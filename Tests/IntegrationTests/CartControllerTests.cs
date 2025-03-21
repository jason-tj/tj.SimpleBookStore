﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using tj.SimpleBookStore.Controllers;
using tj.SimpleBookStore.DbContexts;
using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;
using tj.SimpleBookStore.Services;
using tj.SimpleBookStore.Services.Interface;
using tj.SimpleBookStore.Unit;
using Xunit;

namespace tj.SimpleBookStore.Tests.IntegrationTests
{
    /// <summary>
    /// 
    /// </summary>
    public class CartControllerTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        private readonly HttpClient _client;
        private readonly CartController _controller;
        private readonly Mock<UserContextProxy> _mockUserContext;

        private const string SecretKey = "your-256-bit-secret-1234567890abcdef1234567890abcdef"; // 与主项目一致
        private const string Issuer = "tj-issuer";
        private const string Audience = "tj-audience";


        public CartControllerTests(TestFixture fixture)
        {
            _fixture = fixture;
            var cartRepository = new CartRepository(_fixture.Context);
            var bookRepository = new BookRepository(_fixture.Context);

            _mockUserContext = new Mock<UserContextProxy>();
            var cartService = new CartService(cartRepository, bookRepository, _mockUserContext.Object);
            _controller = new CartController(cartService);
        }

        private void SetUserContext(string userId, string role)
        {
            // 生成 Token
            var token = JwtTokenHelper.GenerateToken(userId, role, SecretKey, Issuer, Audience);

            // 创建 ClaimsPrincipal
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            // 设置 HttpContext
            var httpContext = new DefaultHttpContext();
            httpContext.User = principal;

            // 将 HttpContext 分配给 Controller
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            _mockUserContext.Setup(uc => uc.CurrentUser)
                       .Returns(new UserInfo { Username = userId, Role = userId, UserId = userId });
            //_userContext.CurrentUser = new UserInfo { UserId = userId, Role = userId, Username = userId };
        }

        [Fact]
        public async Task AddToCart_ShouldReturnOk()
        {
            SetUserContext("general", "general");
            // Arrange
            var cartItemDto = new CartItemDto { BookId = 1, Quantity = 2 };

            // Act
            var result = await _controller.AddToCart(cartItemDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetCart_ShouldReturnCartItems()
        {
            // Arrange
            SetUserContext("general", "general");
            var userId = "general";
            _fixture.Context.CartItems.Add(new CartItem { Id = 1, UserId = userId, BookId = 1, Quantity = 2 });
            _fixture.Context.SaveChanges();

            // Act
            var result = await _controller.GetCart();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var cartItems = Assert.IsType<List<CartItem>>(okResult.Value);
            Assert.Single(cartItems);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tj.SimpleBookStore.Controllers;
using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;
using tj.SimpleBookStore.Services;
using tj.SimpleBookStore.Unit;
using Xunit;

namespace tj.SimpleBookStore.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class CartControllerTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        private readonly CartController _controller;

        private const string SecretKey = "your-256-bit-secret-1234567890abcdef1234567890abcdef"; // 与主项目一致
        private const string Issuer = "tj-issuer";
        private const string Audience = "tj-audience";


        public CartControllerTests(TestFixture fixture)
        {
            _fixture = fixture;
            var cartRepository = new CartRepository(_fixture.Context);
            var bookRepository = new BookRepository(_fixture.Context);
            var cartService = new CartService(cartRepository, bookRepository);
            _controller = new CartController(cartService);
        }

        [Fact]
        public async Task AddToCart_ShouldReturnOk()
        {
            // 生成 Token
            var token = JwtTokenHelper.GenerateToken("general", "general", SecretKey, Issuer, Audience);

            // 创建 ClaimsPrincipal
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "general"),
                //new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            // 模拟 HttpContext
            var httpContext = new DefaultHttpContext();
            httpContext.User = principal;

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
            var userId = "user1";
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

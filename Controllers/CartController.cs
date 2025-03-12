using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Services.Interface;

namespace tj.SimpleBookStore.Controllers
{
    /// <summary>
    /// 购物车控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartService"></param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// 获取购物车内容
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var cartItems = await _cartService.GetCartAsync(userId);
            return Ok(cartItems);
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="cartItemDto"></param>
        /// <returns></returns>
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CartItemDto cartItemDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            await _cartService.AddToCartAsync(userId, cartItemDto);
            return Ok();
        }
    }
}

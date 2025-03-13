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
            var cartItems = await _cartService.GetCartAsync();
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
            await _cartService.AddToCartAsync(cartItemDto);
            return Ok();
        }
    }
}

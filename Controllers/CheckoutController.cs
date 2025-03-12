using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tj.SimpleBookStore.Services.Interface;

namespace tj.SimpleBookStore.Controllers
{
    /// <summary>
    /// 结账
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkoutService"></param>
        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        /// <summary>
        /// 结账
        /// </summary>
        /// <returns></returns>
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _checkoutService.CheckoutAsync(userId);
            return Ok(new { TotalPrice = order.TotalAmount });
        }

        /// <summary>
        /// 获取用户订单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetOrderList")]
        public async Task<IActionResult> GetOrderList()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await _checkoutService.GetOrderListAsync(userId);
            return Ok(orders);
        }
    }
}

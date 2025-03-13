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
            var order = await _checkoutService.CheckoutAsync();
            return Ok(new { TotalPrice = order.TotalAmount });
        }

        /// <summary>
        /// 获取用户订单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetOrderList")]
        public async Task<IActionResult> GetOrderList()
        {
            var orders = await _checkoutService.GetOrderListAsync();
            return Ok(orders);
        }
    }
}

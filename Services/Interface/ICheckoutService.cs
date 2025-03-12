using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Services.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICheckoutService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Order> CheckoutAsync(string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> CalculateTotalAsync(string userId);
        /// <summary>
        /// 获取用户订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<IEnumerable<Order>> GetOrderListAsync(string userId);
    }
}

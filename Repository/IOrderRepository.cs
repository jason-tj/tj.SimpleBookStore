using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// 获取用户订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task AddOrderAsync(Order order);
    }
}

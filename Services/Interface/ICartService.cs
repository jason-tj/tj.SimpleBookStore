using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Services.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// 获取购物车内容
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<CartItem>> GetCartAsync();
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartItemDto"></param>
        /// <returns></returns>
        Task AddToCartAsync(CartItemDto cartItemDto);
        /// <summary>
        /// 移除购物车项
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        Task RemoveFromCartAsync(int cartItemId);
        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ClearCartAsync(string userId);
        /// <summary>
        /// 计算购物车总价
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> CalculateTotalAsync(string userId);
    }
}

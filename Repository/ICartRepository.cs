using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task<CartItem> GetCartItemByUserAndBookAsync(string userId, int bookId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(string userId);
    }
}

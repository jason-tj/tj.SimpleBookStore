using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;
using tj.SimpleBookStore.Services.Interface;

namespace tj.SimpleBookStore.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartRepository"></param>
        /// <param name="bookRepository"></param>
        public CartService(ICartRepository cartRepository, IBookRepository bookRepository)
        {
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// 获取购物车内容
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CartItem>> GetCartAsync(string userId)
        {
            return await _cartRepository.GetCartItemsByUserIdAsync(userId);
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartItemDto"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task AddToCartAsync(string userId, CartItemDto cartItemDto)
        {
            // 检查书籍是否存在
            var book = await _bookRepository.GetBookByIdAsync(cartItemDto.BookId);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            // 检查购物车中是否已存在该书籍
            var existingCartItem = await _cartRepository.GetCartItemByUserAndBookAsync(userId, cartItemDto.BookId);
            if (existingCartItem != null)
            {
                // 如果已存在，则更新数量
                existingCartItem.Quantity += cartItemDto.Quantity;
                await _cartRepository.UpdateCartItemAsync(existingCartItem);
            }
            else
            {
                // 如果不存在，则添加新条目
                var cartItem = new CartItem
                {
                    UserId = userId,
                    BookId = cartItemDto.BookId,
                    Quantity = cartItemDto.Quantity
                };
                await _cartRepository.AddCartItemAsync(cartItem);
            }
        }

        /// <summary>
        /// 移除购物项
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task RemoveFromCartAsync(string userId, int cartItemId)
        {
            var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null || cartItem.UserId != userId)
            {
                throw new KeyNotFoundException("Cart item not found");
            }

            await _cartRepository.RemoveCartItemAsync(cartItemId);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }

        /// <summary>
        /// 计算购物车总金额
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> CalculateTotalAsync(string userId)
        {
            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(userId);
            decimal total = 0;

            foreach (var item in cartItems)
            {
                var book = await _bookRepository.GetBookByIdAsync(item.BookId);
                if (book != null)
                {
                    total += book.Price * item.Quantity;
                }
            }

            return total;
        }
    }
}

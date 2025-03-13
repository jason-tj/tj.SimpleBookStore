using tj.SimpleBookStore.DbContexts;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;
using tj.SimpleBookStore.Services.Interface;

namespace tj.SimpleBookStore.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserContext _userContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartRepository"></param>
        /// <param name="orderRepository"></param>
        /// <param name="bookRepository"></param>
        public CheckoutService(ICartRepository cartRepository, IOrderRepository orderRepository, IBookRepository bookRepository, UserContext userContext)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _userContext = userContext;
        }

        /// <summary>
        /// 结算生成订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Order> CheckoutAsync()
        {
            if (_userContext?.CurrentUser == null)
                throw new KeyNotFoundException("user not authentication");
            var userId = _userContext.CurrentUser.UserId;
            // 获取购物车中的商品
            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(userId);
            if (cartItems == null || !cartItems.Any())
            {
                throw new InvalidOperationException("Cart is empty");
            }

            // 计算总价
            decimal total = await CalculateTotalAsync(userId);

            // 创建订单
            var order = new Order
            {
                UserId = userId,
                CreatedTime = DateTime.Now,
                TotalAmount = total,
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    BookId = ci.BookId,
                    Quantity = ci.Quantity,
                    Price = ci.Book.Price
                }).ToList()
            };

            // 保存订单
            await _orderRepository.AddOrderAsync(order);

            // 清空购物车
            await _cartRepository.ClearCartAsync(userId);

            return order;
        }

        /// <summary>
        /// 计算总价
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

        /// <summary>
        /// 获取用户订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Order>> GetOrderListAsync()
        {
            if (_userContext?.CurrentUser == null)
                throw new KeyNotFoundException("user not authentication");
            var userId = _userContext.CurrentUser.UserId;
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }
    }
}

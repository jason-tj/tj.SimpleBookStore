using Moq;
using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;
using tj.SimpleBookStore.Services;
using Xunit;

namespace tj.SimpleBookStore.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _mockCartRepo;
        private readonly Mock<IBookRepository> _mockBookRepo;
        private readonly CartService _cartService;

        /// <summary>
        /// 
        /// </summary>
        public CartServiceTests()
        {
            _mockCartRepo = new Mock<ICartRepository>();
            _mockBookRepo = new Mock<IBookRepository>();
            _cartService = new CartService(_mockCartRepo.Object, _mockBookRepo.Object);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddToCartAsync_ShouldAddNewCartItem()
        {
            // Arrange
            var userId = "user1";
            var cartItemDto = new CartItemDto { BookId = 1, Quantity = 2 };
            var book = new Book { Id = 1, Price = 10.0M };

            _mockBookRepo.Setup(repo => repo.GetBookByIdAsync(1))
                         .ReturnsAsync(book);
            _mockCartRepo.Setup(repo => repo.GetCartItemByUserAndBookAsync(userId, 1))
                         .ReturnsAsync((CartItem)null);

            // Act
            await _cartService.AddToCartAsync(userId, cartItemDto);

            // Assert
            _mockCartRepo.Verify(repo => repo.AddCartItemAsync(It.IsAny<CartItem>()), Times.Once);
        }

        [Fact]
        public async Task CalculateTotalAsync_ShouldReturnCorrectTotal()
        {
            // Arrange
            var userId = "user1";
            var cartItems = new List<CartItem>
        {
            new CartItem { BookId = 1, Quantity = 2, Book = new Book { Id = 1, Price = 10.0M } },
            new CartItem { BookId = 2, Quantity = 1, Book = new Book { Id = 2, Price = 20.0M } }
        };

            _mockCartRepo.Setup(repo => repo.GetCartItemsByUserIdAsync(userId))
                         .ReturnsAsync(cartItems);
            _mockBookRepo.Setup(repo => repo.GetBookByIdAsync(1))
                         .ReturnsAsync(cartItems[0].Book);
            _mockBookRepo.Setup(repo => repo.GetBookByIdAsync(2))
                         .ReturnsAsync(cartItems[1].Book);

            // Act
            var total = await _cartService.CalculateTotalAsync(userId);

            // Assert
            Assert.Equal(40.0M, total);
        }
    }
}

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
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockRepo;
        private readonly BookService _bookService;

        /// <summary>
        /// 
        /// </summary>
        public BookServiceTests()
        {
            _mockRepo = new Mock<IBookRepository>();
            _bookService = new BookService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddBookAsync_ShouldReturnBook()
        {
            // Arrange
            var bookDto = new BookDto { Title = "Test Book", Author = "Test Author", Price = 10.0M, Category = "Fiction" };
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", Price = 10.0M, Category = "Fiction" };

            _mockRepo.Setup(repo => repo.AddBookAsync(It.Is<Book>(b => b == null)))
          .ThrowsAsync(new ArgumentNullException(nameof(Book)));

            // Act
            var result = await _bookService.AddBookAsync(bookDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Book", result.Title);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", Price = 10.0M, Category = "Fiction" };
            _mockRepo.Setup(repo => repo.GetBookByIdAsync(1))
                     .ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBookByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}

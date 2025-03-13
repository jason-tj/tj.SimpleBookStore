using Microsoft.AspNetCore.Mvc;
using Moq;
using tj.SimpleBookStore.Controllers;
using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;
using tj.SimpleBookStore.Services;
using Xunit;

namespace tj.SimpleBookStore.Tests.IntegrationTests
{
    /// <summary>
    /// 
    /// </summary>
    public class BookControllerTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        private readonly BooksController _controller;

        public BookControllerTests(TestFixture fixture)
        {
            _fixture = fixture;
            var bookRepository = new BookRepository(_fixture.Context);
            var bookService = new BookService(bookRepository);
            _controller = new BooksController(bookService);
        }

        [Fact]
        public async Task GetBooksByTitle_ShouldReturnBook()
        {
            // Act
            var result = await _controller.GetBooksByTitle("Book 1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var book = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(1, book.FirstOrDefault().Id);
        }

        [Fact]
        public async Task AddBook_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var bookDto = new BookDto { Title = "New Book", Author = "New Author", Price = 15.0M, Category = "Science" };

            // Act
            var result = await _controller.AddBook(bookDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetAllBooks", createdAtActionResult.ActionName);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Services;

namespace tj.SimpleBookStore.Controllers
{
    /// <summary>
    /// 书籍管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookService"></param>
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// 查找所有书籍
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// 根据标题搜索书籍
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("GetBooksByTitle")]
        public async Task<IActionResult> GetBooksByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title cannot be empty.");
            }

            var books = await _bookService.GetBooksByTitleAsync(title);
            return Ok(books);
        }

        /// <summary>
        /// 添加书籍
        /// 仅支持admin 用户
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpPost("AddBook")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            var book = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetAllBooks), new { id = book.Id }, book);
        }
    }
}

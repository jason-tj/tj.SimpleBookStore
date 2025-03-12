using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// 获取全部书籍
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetAllBooksAsync();
        /// <summary>
        /// 根据ID获取书籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book> GetBookByIdAsync(int id);
        /// <summary>
        /// 根据名称获取书籍
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksByTitleAsync(string title);
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        Task<Book> AddBookAsync(BookDto bookDto);
        /// <summary>
        /// 更新书籍
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        Task UpdateBookAsync(int id, BookDto bookDto);
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteBookAsync(int id);
    }
}

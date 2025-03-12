using tj.SimpleBookStore.DTOs;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;

namespace tj.SimpleBookStore.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }
        public async Task<IEnumerable<Book>> GetBooksByTitleAsync(string title)
        {
            return await _bookRepository.GetBooksByTitleAsync(title);
        }

        public async Task<Book> AddBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Price = bookDto.Price,
                Category = bookDto.Category
            };

            await _bookRepository.AddBookAsync(book);
            return book;
        }

        public async Task UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Price = bookDto.Price;
            book.Category = bookDto.Category;

            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            await _bookRepository.DeleteBookAsync(id);
        }
    }
}

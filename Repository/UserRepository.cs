using Microsoft.EntityFrameworkCore;
using System.Xml;
using tj.DbContexts.SimpleBookStore;
using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                .Where(b => b.UserName == userName).FirstOrDefaultAsync();
        }
    }
}

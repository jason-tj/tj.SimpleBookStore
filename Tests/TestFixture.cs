using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using tj.DbContexts.SimpleBookStore;
using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class TestFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }
        public UserManager<User> UserManager { get; private set; }

        public TestFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new ApplicationDbContext(options);

            // 初始化测试数据
            Context.Books.Add(new Book { Id = 1, Title = "Book 1", Author = "Author 1", Price = 10.0M, Category = "Fiction" });
            Context.Books.Add(new Book { Id = 2, Title = "Book 2", Author = "Author 2", Price = 20.0M, Category = "Non-Fiction" });
            Context.SaveChanges();


            //var store = new UserStore<User>(Context);
            //var passwordHasher = new PasswordHasher<User>();
            //var userValidators = new List<IUserValidator<User>> { new UserValidator<User>() };
            //var passwordValidators = new List<IPasswordValidator<User>> { new PasswordValidator<User>() };
            //var errorDescriber = new IdentityErrorDescriber();
            //var services = new Mock<IServiceProvider>().Object;
            //UserManager = new UserManager<User>(store, null, passwordHasher, userValidators, passwordValidators, null, errorDescriber, services, null);

            //// 预初始化用户数据
            //InitializeUsersAsync().GetAwaiter().GetResult();
        }

        //private async Task InitializeUsersAsync()
        //{
        //    try
        //    {
        //        var general = new User { UserName = "general",Name = "general" };
        //        var resultGeneral = await UserManager.CreateAsync(general, Environment.GetEnvironmentVariable("USER_PASSWORD") ?? "Password123!");
        //        if (!resultGeneral.Succeeded)
        //        {
        //            throw new Exception("Failed to create general user");
        //        }

        //        var admin = new User { UserName = "admin" , Name = "admin", };
        //        var resultAdmin = await UserManager.CreateAsync(admin, Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "Password123!");
        //        if (!resultAdmin.Succeeded)
        //        {
        //            throw new Exception("Failed to create admin user");
        //        }

        //        // 添加日志记录
        //        Console.WriteLine("Users initialized successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // 记录异常信息
        //        Console.WriteLine($"Error initializing users: {ex.Message}");
        //        throw;
        //    }
        //}

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

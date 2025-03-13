using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tj.SimpleBookStore.Models;

namespace tj.DbContexts.SimpleBookStore
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // 定义实体集合
        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置实体关系或约束
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Book)
                .WithMany()
                .HasForeignKey(ci => ci.BookId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany()
                .HasForeignKey(ci => ci.UserId);

            // 配置 Order 实体
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            // 配置 OrderItem 实体
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Book)
                .WithMany()
                .HasForeignKey(oi => oi.BookId);
        }

        /// <summary>
        /// 初始化用户
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public async Task InitializeUsersAsync(UserManager<User> userManager)
        {
            var general = new User { UserName = "general" };
            await userManager.CreateAsync(general, "Password123!");

            var admin = new User { UserName = "admin" };
            await userManager.CreateAsync(admin, "Password123!");
        }
    }
}

using Microsoft.AspNetCore.Identity;
using tj.DbContexts.SimpleBookStore;
using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.Filter
{
    /// <summary>
    /// 初始化数据库数据
    /// </summary>
    public class DataInitializationStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    context.InitializeUsersAsync(userManager).Wait();
                }
                next(app);
            };
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace tj.SimpleBookStore.Models
{
    public class User : IdentityUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
    }
}

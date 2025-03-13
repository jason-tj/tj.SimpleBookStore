using tj.SimpleBookStore.Models;

namespace tj.SimpleBookStore.DbContexts
{
    /// <summary>
    /// 
    /// </summary>
    public class UserContext
    {
        public virtual UserInfo CurrentUser { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    /// <summary>
    /// 仅用于测试
    /// </summary>
    public class UserContextProxy : UserContext
    {
        public override required UserInfo CurrentUser { get; set; }
    }
}

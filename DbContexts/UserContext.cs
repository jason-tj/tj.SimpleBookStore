namespace tj.SimpleBookStore.DbContexts
{
    /// <summary>
    /// 
    /// </summary>
    public class UserContext
    {
        public UserInfo CurrentUser { get; set; }
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
}

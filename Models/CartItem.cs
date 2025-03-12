namespace tj.SimpleBookStore.Models
{
    /// <summary>
    /// 购物车项目
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 书籍ID
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 购买人ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Book Book { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }
    }
}

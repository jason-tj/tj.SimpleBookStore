namespace tj.SimpleBookStore.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class CartItemDto
    {
        /// <summary>
        /// *书籍ID
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// *数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 购买人ID
        /// </summary>
        public string UserId { get; set; }
    }
}

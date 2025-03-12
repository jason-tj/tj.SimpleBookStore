namespace tj.SimpleBookStore.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class BookDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }
    }
}

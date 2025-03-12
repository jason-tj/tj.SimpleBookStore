namespace tj.SimpleBookStore.Models
{
    /// <summary>
    /// 订单项
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Book Book { get; set; }
    }
}

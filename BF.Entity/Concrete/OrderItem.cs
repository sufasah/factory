namespace BF.Entity.Concrete
{
    public class OrderItem:IEntity
    {
        public OrderItem(){}

        public OrderItem(int amount, int orderId, Order order, int productId, Product product)
        {
            Amount = amount;
            OrderId = orderId;
            Order = order;
            ProductId = productId;
            Product = product;
        }

        public int Amount { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}
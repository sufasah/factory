namespace BF.Entity.Concrete
{
    public class SubProduct : IEntity
    {
        public SubProduct(){}

        public SubProduct(int amount, int productId, Product product)
        {
            Amount = amount;
            ProductId = productId;
            Product = product;
        }
        public int Amount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
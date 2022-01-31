namespace BF.WebApi.Models
{
    public class SubProductsDto
    {
        public int SubProductId { get; set; }
        public string SubProductName { get; set; }
        public string SubProductType { get; set; }
        public int SubProductAmount { get; set; }
        public bool SubProductSalable { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int ProductAmount { get; set; }
        public bool ProductSalable { get; set; }
        public int Amount { get; set; }

        public SubProductsDto(int subProductId, string subProductName, string subProductType, int subProductAmount, bool subProductSalable, int productId, string productName, string productType, int productAmount, bool productSalable, int amount)
        {
            SubProductId = subProductId;
            SubProductName = subProductName;
            SubProductType = subProductType;
            SubProductAmount = subProductAmount;
            SubProductSalable = subProductSalable;
            ProductId = productId;
            ProductName = productName;
            ProductType = productType;
            ProductAmount = productAmount;
            ProductSalable = productSalable;
            Amount = amount;
        }

        public SubProductsDto(){}

        
    }
}
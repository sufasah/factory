using System.Collections.Generic;

namespace BF.Entity.Concrete
{
    public class Product:IEntity
    {
        public Product(){}

        public Product(int amount, bool ısSalable, string name, List<OrderItem> orderItems, List<SubProduct> subProducts, string type, List<Schedule> schedules)
        {
            Amount = amount;
            IsSalable = ısSalable;
            Name = name;
            OrderItems = orderItems;
            SubProducts = subProducts;
            Type = type;
            Schedules = schedules;
        }

        public int Amount { get; set; }
        public bool IsSalable { get; set; }
        public string Name { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<SubProduct> SubProducts { get; set; }
        public string Type { get; set; }
    }
}
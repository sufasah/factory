using System.Collections.Generic;

namespace BF.Entity.Concrete
{
    public class Customer:IEntity
    {
        public Customer(){}

        public Customer(string name, string password, List<Order> orders)
        {
            Name = name;
            Password = password;
            Orders = orders;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public List<Order> Orders { get; set; }
    }
}
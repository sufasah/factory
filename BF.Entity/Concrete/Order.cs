using System;
using System.Collections.Generic;

namespace BF.Entity.Concrete
{
    public class Order:IEntity
    {
        public Order(){}

        public Order(int customerId, Customer customer, DateTime deadline, List<OrderItem> orderItems, DateTime orderDate, OrderState state)
        {
            CustomerId = customerId;
            Customer = customer;
            Deadline = deadline;
            OrderItems = orderItems;
            OrderDate = orderDate;
            State = state;
        }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Deadline { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderState State { get; set; }
        public enum OrderState{
            Ordered,
            OrderInQueue,
            OrderDelivered,
            OrderCancelled
        };
    }
}
using System;

namespace BF.Entity.Concrete
{
    public class Schedule : IEntity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int WorkcenterId { get; set; }
        public WorkCenter WorkCenter { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Schedule()
        {
        }

        public Schedule(DateTime start, DateTime end, int workcenterId, WorkCenter workCenter, int productId, Product product)
        {
            Start = start;
            End = end;
            WorkcenterId = workcenterId;
            WorkCenter = workCenter;
            ProductId = productId;
            Product = product;
        }
    }
}
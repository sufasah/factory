using System;

namespace BF.WebApi.Models
{
    public class SchedulesDto
    {
        public int Id { get; set; }
        public int WorkcenterId { get; set; }
        public string WorkcenterName { get; set; }
        public bool WorkcenterActive { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int ProductAmount { get; set; }
        public bool ProductSalable { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public SchedulesDto(){}

        public SchedulesDto(int ıd, int workcenterId, string workcenterName, bool workcenterActive, int productId, string productName, string productType, int productAmount, bool productSalable, DateTime start, DateTime end)
        {
            Id = ıd;
            WorkcenterId = workcenterId;
            WorkcenterName = workcenterName;
            WorkcenterActive = workcenterActive;
            ProductId = productId;
            ProductName = productName;
            ProductType = productType;
            ProductAmount = productAmount;
            ProductSalable = productSalable;
            Start = start;
            End = end;
        }
    }
}
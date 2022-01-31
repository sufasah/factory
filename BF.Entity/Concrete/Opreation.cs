using System.Collections.Generic;

namespace BF.Entity.Concrete
{
    public class Operation:IEntity
    {
        public Operation(){}

        public Operation(string name, string productType, List<WorkCenterOperation> workCenterOperations)
        {
            Name = name;
            ProductType = productType;
            WorkCenterOperations = workCenterOperations;
        }

        public string Name { get; set; }

        public string ProductType { get; set; }
        public List<WorkCenterOperation> WorkCenterOperations { get; set; }
    }
}
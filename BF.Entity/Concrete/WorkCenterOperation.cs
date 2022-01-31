using System.Collections.Generic;

namespace BF.Entity.Concrete
{
    public class WorkCenterOperation : IEntity
    {
        public WorkCenterOperation(){}

        public WorkCenterOperation(int operationId, Operation operation, int speed, int workCenterId, WorkCenter workCenter)
        {
            OperationId = operationId;
            Operation = operation;
            Speed = speed;
            WorkCenterId = workCenterId;
            WorkCenter = workCenter;
        }

        public int OperationId { get; set; }
        public Operation Operation { get; set; }
        public int Speed { get; set; }
        public int WorkCenterId { get; set; }
        public WorkCenter WorkCenter { get; set; }
    }
}
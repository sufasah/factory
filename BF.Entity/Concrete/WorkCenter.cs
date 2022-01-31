using System.Collections.Generic;

namespace BF.Entity.Concrete
{
    public class WorkCenter : IEntity
    {
        public WorkCenter(){}

        public WorkCenter(bool active, string name, List<WorkCenterOperation> workCenterOperations, List<Schedule> schedules)
        {
            Active = active;
            Name = name;
            WorkCenterOperations = workCenterOperations;
            Schedules = schedules;
        }

        public bool Active { get; set; }
        public string Name { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<WorkCenterOperation> WorkCenterOperations { get; set; }
    }
}
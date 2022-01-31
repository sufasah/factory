using System.Collections;
using System.Threading.Tasks;
using BF.Entity.Concrete;

namespace BF.DataAccess.Abstract.Repositories
{
    public interface IWorkCenterRepo : IEntityRepo<WorkCenter>
    {
        Task<IEnumerable> GetListGroupedByActive();
    }
}
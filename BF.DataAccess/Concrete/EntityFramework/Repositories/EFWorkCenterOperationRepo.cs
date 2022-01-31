using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFWorkCenterOperationRepo<TContext> : EFEntityRepoBase<WorkCenterOperation,TContext>, IWorkCenterOperationRepo
        where TContext:DbContext,new()
    {
        
    }
}
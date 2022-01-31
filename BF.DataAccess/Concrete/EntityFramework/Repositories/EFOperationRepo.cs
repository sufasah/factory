using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFOperationRepo<TContext> : EFEntityRepoBase<Operation, TContext>, IOperationRepo
        where TContext : DbContext, new()
    {
    }
}
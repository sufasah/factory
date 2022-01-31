using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFSubProductRepo<TContext> : EFEntityRepoBase<SubProduct,TContext>, ISubProductRepo
        where TContext:DbContext,new()
    {
        
    }
}
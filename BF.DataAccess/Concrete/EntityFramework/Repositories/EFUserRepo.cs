using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFUserRepo<TContext> : EFEntityRepoBase<User,TContext>, IUserRepo
        where TContext:DbContext,new()
    {
        
    }
}
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFOrderItemRepo<TContext> : EFEntityRepoBase<OrderItem,TContext>, IOrderItemRepo
        where TContext:DbContext,new()
    {
        
    }
}
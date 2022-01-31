using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFCustomerRepo<TContext> : EFEntityRepoBase<Customer,TContext>, ICustomerRepo
        where TContext:DbContext,new()
    {
        
    }
}
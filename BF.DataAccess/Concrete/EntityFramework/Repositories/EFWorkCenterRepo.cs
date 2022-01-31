using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFWorkCenterRepo<TContext> : EFEntityRepoBase<WorkCenter, TContext>, IWorkCenterRepo
        where TContext : DbContext, new()
    {
        public async Task<IEnumerable> GetListGroupedByActive()
        {
            using var context = new TContext();
            var db = context.Set<WorkCenter>();
            
            return await db.AsNoTracking().GroupBy(x=>x.Active)
                .Select(x=>new {Active = x.Key, Count = x.Count()})
                .ToListAsync();
        }
    }
}
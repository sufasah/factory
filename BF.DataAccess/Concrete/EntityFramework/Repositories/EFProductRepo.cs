using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFProductRepo<TContext> : EFEntityRepoBase<Product, TContext>, IProductRepo
        where TContext:DbContext,new()
    {
        public async Task<bool> IsProductable(Product entity, int count)
        {
            using var context = new TContext(); 
            entity = await context.Set<Product>().AsNoTracking().Include(x => x.SubProducts).FirstOrDefaultAsync(e => e.Id == entity.Id);
            if (entity == null) throw new System.Exception("Product Not Found");

            foreach (var subProduct in entity.SubProducts)
            {
                var subProductProduct = await context.Set<Product>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == subProduct.Id);
                if (subProduct.Amount * count > subProductProduct.Amount) return false;
            }

            return true;
        }

        public async Task<bool> IsProductionPossible(Product entity, int count)
        {
            using var context = new TContext(); 
            entity = await context.Set<Product>().AsNoTracking().Include(x => x.SubProducts).FirstOrDefaultAsync(e => e.Id == entity.Id);
            if (entity == null) throw new System.Exception("Product Not Found");

            foreach (var subProduct in entity.SubProducts)
            {
                var subProductProduct = await context.Set<Product>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == subProduct.Id);
                if (subProduct.Amount * count > subProductProduct.Amount)
                {
                    var subProductAmountNeeded = subProduct.Amount * count - subProductProduct.Amount;
                    if (!await IsProductionPossible(subProductProduct, subProductAmountNeeded)) return false;
                }
            }

            return true;
        }
    }
}
using System.Threading.Tasks;
using BF.Entity.Concrete;

namespace BF.DataAccess.Abstract.Repositories
{
    public interface IProductRepo : IEntityRepo<Product>
    {
        Task<bool> IsProductable(Product entity, int count);
        Task<bool> IsProductionPossible(Product entity, int count);
    }
}
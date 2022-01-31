using System.Collections;
using System.Threading.Tasks;
using BF.Entity.Concrete;

namespace BF.DataAccess.Abstract.Repositories
{
    public interface IOrderRepo : IEntityRepo<Order>
    {
        Task<Order> InsertOrderWithOrderItems(Order entity);
        Task ConfirmOrder(int id);
        Task<IEnumerable> GetOrdersGroupedByState();
        Task<int> GetOrderCountNotReadyAtDeadline();
    }
}
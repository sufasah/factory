using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFOrderRepo<TContext> : EFEntityRepoBase<Order, TContext>, IOrderRepo
        where TContext : DbContext, new()
    {
        public async Task<Order> InsertOrderWithOrderItems(Order entity)
        {
            using var context = new TContext();
            using var transaction = context.Database.BeginTransaction();
            transaction.CreateSavepoint("BeforeInsertOrdersWithOrderItems");
            try
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Added;
                context.SaveChanges();

                if(entity.OrderItems != null){
                    foreach (var item in entity.OrderItems)
                    {
                        item.Order = entity;
                        var entry2 = context.Entry(item);
                        entry2.State = EntityState.Added;
                    }
                }
                context.SaveChanges();

                transaction.Commit();
                return entity;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeInsertOrdersWithOrderItems");
                return null; 
            }
        }

        public async Task ConfirmOrder(int id)
        {
            using var context = new TContext();
            using var transaction = context.Database.BeginTransaction();
            transaction.CreateSavepoint("BeforeConfirmOrder");
            try
            {
                var entity = context.Set<Order>()
                .Include(x=>x.OrderItems)
                .ThenInclude(x=>x.Product)
                .Where(x=>x.Id==id).First();

                entity.State = Order.OrderState.OrderDelivered;

                foreach (var item in entity.OrderItems)
                {
                    var product = item.Product;
                    product.Amount -= item.Amount;
                    if(product.Amount < 0)
                        throw new Exception("Some of products ordered are not in stock");
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                await transaction.RollbackToSavepointAsync("BeforeConfirmOrder");
                throw new Exception(e.Message,e.InnerException);
            }
        }

        public async Task<IEnumerable> GetOrdersGroupedByState()
        {
            using var context = new TContext();
            var db = context.Set<Order>();
            
            return await db.AsNoTracking().GroupBy(x=>x.State)
                .Select(x=>new {State = x.Key, Count = x.Count()})
                .ToListAsync();
        }

        public async Task<int> GetOrderCountNotReadyAtDeadline()
        {
            using var context = new TContext();
            var orders = await context.Set<Order>()
                .AsNoTracking()
                .Include(x=>x.OrderItems)
                .ThenInclude(x=>x.Product)
                .Where(x=>x.State == Order.OrderState.Ordered || x.State == Order.OrderState.OrderInQueue)
                .ToListAsync();
            int res = 0;
            foreach( var order in orders){
                bool thereOneWontBeReady = false;
                foreach(var item in order.OrderItems){
                    var countOperation = await context.Set<Operation>()
                        .AsNoTracking()
                        .Where(x=>x.ProductType==item.Product.Type)
                        .CountAsync();
                    
                    var scheduledProductCount = await context.Set<Schedule>()
                        .AsNoTracking()
                        .Where(x=>x.ProductId == item.ProductId && x.End <= order.Deadline)
                        .CountAsync();
                    
                    if(countOperation == 0){
                        thereOneWontBeReady = true;
                        break;
                    }

                    var incomingProducts = scheduledProductCount/countOperation;

                    if(item.Amount > item.Product.Amount+incomingProducts)
                    {
                        thereOneWontBeReady = true;
                        break;
                    }
                }

                if(thereOneWontBeReady)
                    res++;
            }

            return res;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BF.WebApi.Controllers
{
    public class OrdersController : EntityControllerBase<Order>
    {
        private readonly IOrderRepo _repo;
        public OrdersController(IOrderRepo repo) : base(repo)
        {
            _repo = repo;
        }
        [HttpGet]
        override public async Task<IActionResult> GetList( ){
            return await base.GetList();
        }
        [HttpGet("{id}")]
        override public async Task<IActionResult> Get(int id){
            return await base.Get(id);
        }
        
        [HttpPost]
        override public async Task<IActionResult> Post(Order entity){
            Order res = await _repo.InsertOrderWithOrderItems(entity);
            foreach (var item in res.OrderItems)
            {
                item.Order = null;
            }
            return Ok(Resp(true,res)); 
        }

        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<Order> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Order entity, [FromQuery] bool confirm){
            if(confirm){
                await _repo.ConfirmOrder(entity.Id);
                return Ok(Resp(true)); 
            }
            else
                return await base.Put(entity);
        }
        [HttpDelete("{id}")]
        override public async Task<IActionResult> Delete(int id){
            return await base.Delete(id);
        }
        [HttpGet("panel")]
        public async Task<IActionResult> GetListPanel(){
            var orders = await _repo.GetListInclude(x=>x.Include(x=>x.OrderItems)
                .ThenInclude(x=>x.Product)
                .Include(x=>x.Customer)
                );

            foreach (var order in orders)
            {
                order.Customer.Orders=null;
                foreach (var item in order.OrderItems)
                {
                    item.Order=null;
                    item.Product.OrderItems=null;
                }
            }
            return Ok(Resp(true,orders));
        }
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetOrdersGroupedByState(){
            var res = await _repo.GetOrdersGroupedByState();
            return Ok(Resp(true,res));
        }
        [HttpGet("count")]
        override public async Task<IActionResult> GetCount(){
            return await base.GetCount();
        }
        [HttpGet("dashboard-deadline")]
        public async Task<IActionResult> GetCountNotReadyAtDeadline(){
            var res = await _repo.GetOrderCountNotReadyAtDeadline();
            return Ok(Resp(true,res));
        }

        

    }
}
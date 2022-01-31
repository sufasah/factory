using System.Collections.Generic;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using BF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BF.WebApi.Controllers
{
    public class SchedulesController : EntityControllerBase<Schedule>
    {
        private readonly IScheduleRepo _repo;

        public SchedulesController(IScheduleRepo repo) : base(repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int workcenterId){
            List<Schedule> resData;
            if(workcenterId>0)
                resData = await _repo.GetListInclude(context=>context
                .Include(x=>x.Product)
                .Include(x=>x.WorkCenter),x=>x.WorkcenterId == workcenterId);
            else
                resData = await _repo.GetListInclude(context=>context
                .Include(x=>x.Product)
                .Include(x=>x.WorkCenter));
            var res = new List<SchedulesDto>();
            foreach (var item in resData)
            {
                res.Add(new SchedulesDto(
                    item.Id,
                    item.WorkcenterId,
                    item.WorkCenter.Name,
                    item.WorkCenter.Active,
                    item.ProductId,
                    item.Product.Name,
                    item.Product.Type,
                    item.Product.Amount,
                    item.Product.IsSalable,
                    item.Start,
                    item.End
                ));
            }

            return Ok(Resp(true,res));
        }
        [HttpGet("{id}")]
        override public async Task<IActionResult> Get(int id){
            return await base.Get(id);
        }
        [HttpPost]
        override public async Task<IActionResult> Post(Schedule entity){
            return await base.Post(entity);
        }
        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<Schedule> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        override public async Task<IActionResult> Put(Schedule entity){
            return await base.Put(entity);
        }
        [HttpDelete("{id}")]
        override public async Task<IActionResult> Delete(int id){
            return await base.Delete(id);
        }
        [HttpGet("count")]
        override public async Task<IActionResult> GetCount(){
            return await base.GetCount();
        }
    }
}
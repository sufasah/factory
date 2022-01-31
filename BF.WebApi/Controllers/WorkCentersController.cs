using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BF.WebApi.Controllers
{
    public class WorkCentersController : EntityControllerBase<WorkCenter>
    {
        private readonly IWorkCenterRepo _repo;
        public WorkCentersController(IWorkCenterRepo repo) : base(repo)
        {
            _repo = repo;
        }
        [HttpGet]
        override public async Task<IActionResult> GetList(){
            return await base.GetList();
        }
        [HttpGet("{id}")]
        override public async Task<IActionResult> Get(int id){
            return await base.Get(id);
        }
        [HttpPost]
        override public async Task<IActionResult> Post(WorkCenter entity){
            return await base.Post(entity);
        }
        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<WorkCenter> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        override public async Task<IActionResult> Put(WorkCenter entity){
            return await base.Put(entity);
        }
        [HttpDelete("{id}")]
        override public async Task<IActionResult> Delete(int id){
            return await base.Delete(id);
        }
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetListGroupedByActive(){
            var res = await _repo.GetListGroupedByActive();
            return Ok(Resp(true,res));
        }
        [HttpGet("count")]
        override public async Task<IActionResult> GetCount(){
            return await base.GetCount();
        }
    }
}
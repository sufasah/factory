using System.Collections.Generic;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BF.WebApi.Controllers
{
    public class OperationsController : EntityControllerBase<Operation>
    {
        private readonly IOperationRepo _repo;

        public OperationsController(IOperationRepo repo) : base(repo)
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
        override public async Task<IActionResult> Post(Operation entity){
            return await base.Post(entity);
        }
        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<Operation> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        override public async Task<IActionResult> Put(Operation entity){
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
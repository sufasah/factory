using System.Collections.Generic;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BF.WebApi.Controllers
{
    public class ProductsController : EntityControllerBase<Product>
    {
        private readonly IProductRepo _repo;

        public ProductsController(IProductRepo repo) : base(repo)
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
        override public async Task<IActionResult> Post(Product entity){
            return await base.Post(entity);
        }
        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<Product> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        override public async Task<IActionResult> Put(Product entity){
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
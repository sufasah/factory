using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using BF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BF.WebApi.Controllers
{
    public class SubProductsController : EntityControllerBase<SubProduct>
    {
        private readonly ISubProductRepo _repo;
        private readonly IProductRepo _productRepo;

        public SubProductsController(ISubProductRepo repo, IProductRepo productRepo) : base(repo)
        {
            _repo = repo;
            _productRepo = productRepo;
        }
        [HttpGet]
        override public async Task<IActionResult> GetList(){
            
            var subProducts = await _repo.GetListInclude(
                x=>x.Include(x=>x.Product));
            
            subProducts = subProducts.Where(x => x.PublicPropertiesEqual(Request.Query)).ToList();            
            var res = new List<SubProductsDto>();
            if(!subProducts.Any()){
                return Ok(Resp(data:res,msg:"No Result"));
            }
            
            foreach (var item in subProducts)
            {
                var subProduct = await _productRepo.Get(x=>x.Id==item.Id);
                if(subProduct != null)
                    res.Add(new SubProductsDto(
                        subProduct.Id,
                        subProduct.Name,
                        subProduct.Type,
                        subProduct.Amount,
                        subProduct.IsSalable,
                        item.Product.Id,
                        item.Product.Name,
                        item.Product.Type,
                        item.Product.Amount,
                        item.Product.IsSalable,
                        item.Amount
                    ));
            }
            return Ok(Resp(true,res));
        }
        [HttpGet("{id}")]
        override public async Task<IActionResult> Get(int id){
            return await base.Get(id);
        }
        [HttpPost]
        override public async Task<IActionResult> Post(SubProduct entity){
            var addRes = await _repo.Add(entity);
            var product = await _productRepo.Get(x=>x.Id == addRes.ProductId);
            var subProduct = await _productRepo.Get(x=>x.Id == addRes.Id);
            var res = new SubProductsDto(
                subProduct.Id,
                subProduct.Name,
                subProduct.Type,
                subProduct.Amount,
                subProduct.IsSalable,
                product.Id,
                product.Name,
                product.Type,
                product.Amount,
                product.IsSalable,
                addRes.Amount
            );
            return Ok(Resp(true,res));
        }
        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<SubProduct> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        override public async Task<IActionResult> Put(SubProduct entity){
            var addRes = await _repo.Add(entity);
            var product = await _productRepo.Get(x=>x.Id == addRes.ProductId);
            var subProduct = await _productRepo.Get(x=>x.Id == addRes.Id);
            var res = new SubProductsDto(
                subProduct.Id,
                subProduct.Name,
                subProduct.Type,
                subProduct.Amount,
                subProduct.IsSalable,
                product.Id,
                product.Name,
                product.Type,
                product.Amount,
                product.IsSalable,
                addRes.Amount
            );
            return Ok(Resp(true,res));
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
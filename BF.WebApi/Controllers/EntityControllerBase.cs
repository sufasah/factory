using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract;
using BF.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BF.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EntityControllerBase<TEntity> : ControllerBase
        where TEntity: IEntity,new()
    {
        private readonly IEntityRepo<TEntity> _repo;
        private readonly Type _entityType;
        public EntityControllerBase(IEntityRepo<TEntity> repo)
        {
            _repo = repo;
            _entityType = typeof(TEntity);
        }

        protected object Resp(bool success=true,object data=null,string msg="Operation is successful."){
            return new {
                Success=success,
                Message=msg,
                Data=data
            };
        }

        public async virtual Task<IActionResult> GetList()
        {
            List<TEntity> res = await _repo.GetList();

            res = res.Where(x => x.PublicPropertiesEqual(Request.Query)).ToList();            
            if(!res.Any()){
                return Ok(Resp(data:new List<TEntity>(),msg:"No Result"));
            }

            return Ok(Resp(data:res,msg:"Records listed successfully."));
        }
        public async virtual Task<IActionResult> Get(int id){
            TEntity res = await _repo.Get(entity => entity.Id==id);
            
            if(res == null){
                return Ok(Resp(data:new {},msg:"No Result"));
            }

            return Ok(Resp(data:res,msg:"The entity fetched successfully."));
        }

        public async virtual Task<IActionResult> Post(TEntity entity){
            TEntity res = await _repo.Add(entity);
            return Created($"{Request.Path.Value}/{res.Id}",Resp(data:res,msg:"The entity recorded successfully."));
        }
        public async virtual Task<IActionResult> PostList(List<TEntity> entities){
            await _repo.AddList(entities);
            return Ok(Resp(data:null,msg:"The entities recorded successfully."));
        }
        public async virtual Task<IActionResult> Put(TEntity entity){
            TEntity res = await _repo.Update(entity);
            return Ok(Resp(data:res,msg:"The entity updated successfully."));
        }
        public async virtual Task<IActionResult> Delete(int id){
            var entity = new TEntity
            {
                Id = id
            };
            await _repo.Delete(entity);
            return Ok(Resp(msg:"The entity deleted successfully"));
        }
        public async virtual Task<IActionResult> GetCount(){
            var res = await _repo.GetCount();
            return Ok(Resp(data:res,msg:"Record count is got successfully."));
        }
    }
}

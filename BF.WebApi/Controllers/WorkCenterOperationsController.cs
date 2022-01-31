using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BF.WebApi.Controllers
{
    public class WorkCenterOperationsController : EntityControllerBase<WorkCenterOperation>
    {
        private readonly IWorkCenterOperationRepo _repo;
        private readonly IOperationRepo _operationRepo;
        private readonly IWorkCenterRepo _workcenterRepo;
        public WorkCenterOperationsController(IWorkCenterOperationRepo repo, IOperationRepo opreationRepo, IWorkCenterRepo workcenterRepo) : base(repo)
        {
            _repo = repo;
            _operationRepo = opreationRepo;
            _workcenterRepo = workcenterRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string type){
            List<WorkCenterOperation> res; 
            if(type==null)
                res = await _repo.GetListInclude(context => context
                .Include(x=>x.Operation)
                .Include(x=>x.WorkCenter));
            else
                res = await _repo.GetListInclude(context => context
                .Include(x=>x.Operation)
                .Include(x=>x.WorkCenter),x=>x.Operation.ProductType == type);

            foreach (var item in res)
            {
                item.Operation.WorkCenterOperations=null;
                item.WorkCenter.WorkCenterOperations=null;
            }
            return Ok(Resp(true,res));
        }
        [HttpGet("{id}")]
        override public async Task<IActionResult> Get(int id){
            return await base.Get(id);
        }
        [HttpPost]
        override public async Task<IActionResult> Post(WorkCenterOperation entity){
            var resData = await _repo.Add(entity);
            var operation = await _operationRepo.Get(x=>x.Id==resData.OperationId);
            var workcenter = await _workcenterRepo.Get(x=>x.Id==resData.WorkCenterId);

            var res=new {
                resData.Id,
                OperationId = operation.Id,
                OperationName = operation.Name,
                OperationType = operation.ProductType,
                WorkcenterId = workcenter.Id,
                WorkcenterName = workcenter.Name,
                WorkcenterActive = workcenter.Active,
                resData.Speed
            };

            return Ok(Resp(true,res));
        }
        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<WorkCenterOperation> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        override public async Task<IActionResult> Put(WorkCenterOperation entity){
            var resData = await _repo.Update(entity);
            var operation = await _operationRepo.Get(x=>x.Id==resData.OperationId);
            var workcenter = await _workcenterRepo.Get(x=>x.Id==resData.WorkCenterId);    
            var res = new {
                resData.Id,
                OperationId = operation.Id,
                OperationName = operation.Name,
                OperationType = operation.ProductType,
                WorkcenterId = workcenter.Id,
                WorkcenterName = workcenter.Name,
                WorkcenterActive = workcenter.Active,
                resData.Speed
            };
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
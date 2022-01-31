using System.Collections.Generic;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BF.WebApi.Controllers
{
    public class UsersController : EntityControllerBase<User>
    {
        private readonly IUserRepo _repo;
        public UsersController(IUserRepo repo) : base(repo)
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
        override public async Task<IActionResult> Post(User entity){
            return await base.Post(entity);
        }
        [HttpPost("list")]
        override public async Task<IActionResult> PostList(List<User> entities){
            return await base.PostList(entities);
        }
        [HttpPut("{id}")]
        override public async Task<IActionResult> Put(User entity){
            return await base.Put(entity);
        }
        [HttpDelete("{id}")]
        override public async Task<IActionResult> Delete(int id){
            return await base.Delete(id);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(Customer entity){
            var res = await _repo.Get(x => x.Name == entity.Name && x.Password == entity.Password);
        
            if(res == null){
                return Ok(Resp(true,new {Logged = false},"User not found."));
            }

            return Ok(Resp(true,new {Logged = true, res.Id},"User successfully logged in."));
        }
        [HttpGet("count")]
        override public async Task<IActionResult> GetCount(){
            return await base.GetCount();
        }
        
    }
}
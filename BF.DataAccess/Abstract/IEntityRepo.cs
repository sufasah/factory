using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BF.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace BF.DataAccess.Abstract
{
    public interface IEntityRepo<T> 
        where T: IEntity,new() 
    {
        Task<List<T>> GetList(Expression<Func<T,bool>> filter=null);
                
        Task<List<T>> GetListInclude(Expression<Func<IQueryable<T>,IIncludableQueryable<T,object>>> includes, Expression<Func<T, bool>> filter = null);

        Task<T> Get(Expression<Func<T,bool>> filter);

        Task<T> Add(T entity);

        Task AddList(List<T> entity);

        Task<T> Update(T entity);

        Task Delete(T entity);

        Task<long> GetCount();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BF.DataAccess.Abstract;
using BF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFEntityRepoBase<TEntity,TContext> : IEntityRepo<TEntity>
        where TEntity: IEntity, new()
        where TContext: DbContext,new()
    {
        public async Task<TEntity> Add(TEntity entity)
        {
            using var context = new TContext();
            var result = context.Entry(entity);
            result.State = EntityState.Added;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task AddList(List<TEntity> entities)
        {
            using var context = new TContext();
            context.AddRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            using var context = new TContext(); 
            var result = context.Entry(entity);
            result.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext(); 
            var result = await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(filter);
            return result;
        }

        public async Task<long> GetCount()
        {
            using var context = new TContext();
            var result = await context.Set<TEntity>().CountAsync();
            return result;
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();

            if (filter == null)
                return await context.Set<TEntity>().AsNoTracking()
                    .ToListAsync();

            var result = await context.Set<TEntity>().AsNoTracking()
                .Where(filter)
                .ToListAsync();

            return result;
        }

        public async Task<List<TEntity>> GetListInclude(Expression<Func<IQueryable<TEntity>,IIncludableQueryable<TEntity,object>>> includes, Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();

            var includeFunc = includes.Compile();
            if (filter == null)
            {
                return await includeFunc(context.Set<TEntity>().AsNoTracking())
                    .ToListAsync();
            }
            var result = await includeFunc(context.Set<TEntity>().AsNoTracking()
                .Where(filter))
                .ToListAsync();
                
            return result;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            using var context = new TContext(); 
            var result = context.Entry(entity);
            result.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
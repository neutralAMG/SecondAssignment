

using Microsoft.EntityFrameworkCore;
using SecondAssignment.Database.Context;

namespace SecondAssignment.Infraestructure.Core
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly SecondAssignmentContext context;
  
        public BaseRepository(SecondAssignmentContext context)
        {
            this.context = context;
           
        }
        public virtual async Task<bool> Exits(Func<TEntity, bool> filter)
        {
            return await Task.FromResult(context.Set<TEntity>().Any(filter));
        }
      public virtual async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task Save(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
           await context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }
        public virtual async Task Delete(TEntity entity)
        {

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

        }
    }
}

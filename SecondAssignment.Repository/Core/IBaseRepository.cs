

namespace SecondAssignment.Infraestructure.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<bool> Exits(Func<TEntity, bool> filter);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Save(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}


namespace SecondAssignment.Application.Core
{
    public interface IBaseService<TModel, TDSave, TDUpdate> 
        where TModel : class
        where TDSave : class
        where TDUpdate : class
    {
        Task<Result<List<TModel>>> GetAll();
        Task<Result<TModel>> Get(Guid id);
        Task<Result<TModel>> Save(TDSave dto);
       Task<Result<TModel>> Update(TDUpdate dto);
        Task<Result<TModel>> Delete(Guid dto);

    }
}

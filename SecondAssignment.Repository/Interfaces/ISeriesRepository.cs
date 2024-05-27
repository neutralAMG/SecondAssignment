
using SecondAssignment.Database.Entities;
using SecondAssignment.Infraestructure.Core;

namespace SecondAssignment.Infraestructure.Interfaces
{
   public interface ISeriesRepository : IBaseRepository<Series>
    {
        Task<Series> GetSeriesByName(string name);
        Task<List<Series>> GetByGenreId(Guid genreId);
        Task<List<Series>> GetByProducerId(Guid producerId);
    }
}

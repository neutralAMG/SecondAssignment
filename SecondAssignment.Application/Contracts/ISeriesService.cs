

using SecondAssignment.Application.Core;
using SecondAssignment.Application.Dtos;
using SecondAssignment.Application.Models;
using SecondAssignment.Database.Entities;

namespace SecondAssignment.Application.Contracts
{
    public interface ISeriesService : IBaseService<SeriesModel, SaveSeriesDto, UpdateSeriesDto>
    {
        Task<Result<SeriesModel>> GetSeriesByName(string name);
        Task<Result<List<SeriesModel>>> GetByGenreId(Guid genreId);
        Task<Result<List<SeriesModel>>> GetByProducerId(Guid producerId);
    }
}

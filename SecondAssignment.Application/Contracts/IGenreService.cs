
using SecondAssignment.Application.Core;
using SecondAssignment.Application.Dtos;
using SecondAssignment.Application.Models;

namespace SecondAssignment.Application.Contracts
{
    public interface IGenreService : IBaseService<GenreModel, SaveGenresDtos, UpdateGenresDtos>
    {
    }
}

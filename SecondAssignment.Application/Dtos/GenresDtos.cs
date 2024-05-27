
using SecondAssignment.Application.Core;

namespace SecondAssignment.Application.Dtos
{
     public abstract record BaseGenresDtos : BaseDto
    {
        
    }
    public record SaveGenresDtos : BaseGenresDtos
    {
       
    }
    public record UpdateGenresDtos : BaseGenresDtos
    {
        public Guid GenreId { get; set; }
    }
}

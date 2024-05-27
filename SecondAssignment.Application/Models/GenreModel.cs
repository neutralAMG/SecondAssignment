

using SecondAssignment.Application.Core;

namespace SecondAssignment.Application.Models
{
    public class GenreModel : BaseModel
    {
        public Guid GenreId { get; set; }
        public IList<SeriesModel>? Series { get; set; }
    }
}

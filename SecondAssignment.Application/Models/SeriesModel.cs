
using SecondAssignment.Application.Core;

namespace SecondAssignment.Application.Models
{
    public class SeriesModel : BaseModel
    {
        public Guid SeriesId { get; set; }
        public string? ImgUrlPath { get; set; }
        public string? VideoUrlPath { get; set; }
        public string Description { get; set; }
        //public Guid PrimaryGenreId { get; set; }
        //public Guid ProducerId { get; set; }
        //public Guid SecundaryGenreId { get; set; }

        public ProducerModel? Producer { get; set; }
        public GenreModel? PrimaryGenre { get; set; }
        public GenreModel? SecundaryGenre { get; set; }
    }
}


using SecondAssignment.Application.Core;
using System.ComponentModel.DataAnnotations;

namespace SecondAssignment.Application.Models
{
    public class SeriesModel : BaseModel
    {
        public Guid SeriesId { get; set; }
        [Required(ErrorMessage = "ImgUrlPath is a required field")]
        public string? ImgUrlPath { get; set; }
        [Required(ErrorMessage = "VideoUrlPath is a required field")]
        public string? VideoUrlPath { get; set; }
        [Required(ErrorMessage = "Description is a required field")]
        public string Description { get; set; }
        //public Guid PrimaryGenreId { get; set; }
        //public Guid ProducerId { get; set; }
        //public Guid SecundaryGenreId { get; set; }
        [Required(ErrorMessage = "Producer is a required field")]
        public ProducerModel? Producer { get; set; }
        [Required(ErrorMessage = "PrimaryGenre is a required field")]
        public GenreModel? PrimaryGenre { get; set; }

        public GenreModel? SecundaryGenre { get; set; }
    }
}

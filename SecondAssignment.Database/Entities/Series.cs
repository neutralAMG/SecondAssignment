
using SecondAssignment.Database.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondAssignment.Database.Entities
{
    public class Series : BasesEntity
    {
        public Series()
        {
            SeriesId = Guid.NewGuid();
        }
        public Guid SeriesId { get; set; }
        public string? ImgUrlPath { get; set; }
        public string? VideoUrlPath { get; set; }
        public string Description { get; set; }

        
        public Guid PrimaryGenreId { get; set; }
        public Guid ProducerId { get; set; }
        public Guid SecundaryGenreId { get; set; }

        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
        [ForeignKey("PrimaryGenreId")]
        public Genre PrimaryGenre { get; set; }
        [ForeignKey("SecundaryGenreId")]
        public Genre SecundaryGenre { get; set; }    
    }
}

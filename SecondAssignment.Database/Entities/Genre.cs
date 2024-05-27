
using SecondAssignment.Database.Core;

namespace SecondAssignment.Database.Entities
{
    public class Genre : BasesEntity
    {
        public Genre()
        {
            GenreId = Guid.NewGuid();
        }
        public Guid GenreId { get; set; } 
        public IList<Series>? Series { get; set; }
    }
}

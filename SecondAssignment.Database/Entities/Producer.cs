

using SecondAssignment.Database.Core;

namespace SecondAssignment.Database.Entities
{
    public class Producer : BasesEntity
    {
        public Producer()
        {
            ProducersId = Guid.NewGuid();
        }
        public Guid ProducersId { get; set; } 
        public IList<Series>? Series { get; set; }
    }
}

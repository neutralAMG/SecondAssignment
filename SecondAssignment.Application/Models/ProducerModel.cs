
using SecondAssignment.Application.Core;


namespace SecondAssignment.Application.Models
{
    public class ProducerModel : BaseModel
    {
        public Guid ProducersId { get; set; }
        public IList<SeriesModel>? Series { get; set; }
    }
}

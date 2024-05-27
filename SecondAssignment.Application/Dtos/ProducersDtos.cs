using SecondAssignment.Application.Core;


namespace SecondAssignment.Application.Dtos
{
    public record BaseProducersDto : BaseDto
    {
       
    }
    public record SaveProducersDto : BaseProducersDto
    {
        
    }
    public record UpdateProducersDto : BaseProducersDto
    {
        public Guid ProducersId { get; set; }
    }
}

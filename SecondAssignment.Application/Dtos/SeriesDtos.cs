using SecondAssignment.Application.Core;


namespace SecondAssignment.Application.Dtos
{
    public abstract record BaseSeriesDto : BaseDto
    {
        public string? ImgUrlPath { get; set; }
        public string? VideoUrlPath { get; set; }
        public string Description { get; set; }
        public Guid PrimaryGenreId { get; set; }
        public Guid ProducerId { get; set; }
        public Guid SecundaryGenreId { get; set; }
    }

    public record SaveSeriesDto : BaseSeriesDto
    {
        
    }
    public record UpdateSeriesDto : BaseSeriesDto
    {
        public Guid SeriesId { get; set; }
    }

}

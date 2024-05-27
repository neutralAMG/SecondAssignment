using Microsoft.AspNetCore.Mvc.Rendering;
using SecondAssignment.Application.Models;

namespace SecondAssignment.WepApp.Models
{
    public class EditSeriesModel
    {
        public SeriesModel? Series { get; set; }
        public Dictionary<int, List<SelectListItem>>? Selectedlists { get; set; }
    }
}

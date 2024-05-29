using Microsoft.AspNetCore.Mvc.Rendering;
using SecondAssignment.Application.Models;

namespace SecondAssignment.WepApp.Models
{
    public class IndexModel
    {
        public List<SeriesModel> Series { get; set; }
        public List<CheckBoxOption>? Selectedlists { get; set; }
    }
}

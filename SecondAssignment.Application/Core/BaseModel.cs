
using System.ComponentModel.DataAnnotations;

namespace SecondAssignment.Application.Core
{
    public class BaseModel
    {
        [Required(ErrorMessage = "name is a required field")]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Sliders
{
    public class SliderEditVM
    {
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public IFormFile NewImage { get; set; }
    }
}

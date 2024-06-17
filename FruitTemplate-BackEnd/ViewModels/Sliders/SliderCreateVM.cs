using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}

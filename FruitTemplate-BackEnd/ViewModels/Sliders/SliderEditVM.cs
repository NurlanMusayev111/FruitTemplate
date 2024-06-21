using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Sliders
{
    public class SliderEditVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}

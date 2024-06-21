using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.SliderInfos
{
    public class SliderInfoCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

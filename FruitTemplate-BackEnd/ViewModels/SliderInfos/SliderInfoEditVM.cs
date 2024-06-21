using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.SliderInfos
{
    public class SliderInfoEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

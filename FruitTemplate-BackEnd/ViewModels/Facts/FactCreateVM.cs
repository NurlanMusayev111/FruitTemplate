using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Facts
{
    public class FactCreateVM
    {
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Count { get; set; }
    }
}

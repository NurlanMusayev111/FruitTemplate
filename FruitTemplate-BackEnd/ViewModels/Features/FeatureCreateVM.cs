using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Features
{
    public class FeatureCreateVM
    {
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

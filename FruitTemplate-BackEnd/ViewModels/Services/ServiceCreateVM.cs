using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Services
{
    public class ServiceCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

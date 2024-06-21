using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Banners
{
    public class BannerCreateVM
    {
        [Required]
        public IFormFile NewImage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
    }
}

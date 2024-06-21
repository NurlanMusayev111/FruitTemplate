using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.BestSellers
{
    public class BestSellerCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Price { get; set; }
    }
}

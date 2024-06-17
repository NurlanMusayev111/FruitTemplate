using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}

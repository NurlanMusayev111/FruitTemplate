using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Category
{
	public class CategoryEditVM
	{
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

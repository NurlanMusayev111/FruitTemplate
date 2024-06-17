using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Facts
{
    public class FactEditVM
    {
        public int Id { get; set; }
        [Required]

        public string Icon { get; set; }
        public string Title { get; set; }
        [Required]
        public int Count { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FruitTemplate_BackEnd.ViewModels.Facts
{
    public class FactEditVM
    {
        public int Id { get; set; }


        public string Icon { get; set; }
        public string Title { get; set; }

        public int Count { get; set; }
    }
}

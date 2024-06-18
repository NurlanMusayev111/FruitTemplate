namespace FruitTemplate_BackEnd.Models
{
    public class Product : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}

namespace FruitTemplate_BackEnd.Models
{
    public class Categories : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

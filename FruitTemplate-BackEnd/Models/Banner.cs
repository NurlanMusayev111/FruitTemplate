namespace FruitTemplate_BackEnd.Models
{
    public class Banner : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}

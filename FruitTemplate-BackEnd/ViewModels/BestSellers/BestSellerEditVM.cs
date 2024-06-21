namespace FruitTemplate_BackEnd.ViewModels.BestSellers
{
    public class BestSellerEditVM
    {
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
    }
}

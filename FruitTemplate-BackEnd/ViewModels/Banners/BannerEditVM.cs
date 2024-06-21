namespace FruitTemplate_BackEnd.ViewModels.Banners
{
    public class BannerEditVM
    {
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}

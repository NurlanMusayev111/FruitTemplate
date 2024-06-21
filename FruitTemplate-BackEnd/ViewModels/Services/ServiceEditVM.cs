namespace FruitTemplate_BackEnd.ViewModels.Services
{
    public class ServiceEditVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}

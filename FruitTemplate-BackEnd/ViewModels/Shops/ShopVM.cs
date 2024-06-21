using FruitTemplate_BackEnd.Models;

namespace FruitTemplate_BackEnd.ViewModels.Shops
{
    public class ShopVM
    {
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<Categories> Categories { get; set; }
        public List<BestSeller> BestSellers { get; set; }
    }
}

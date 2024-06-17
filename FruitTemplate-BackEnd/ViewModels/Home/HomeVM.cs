using FruitTemplate_BackEnd.Models;

namespace FruitTemplate_BackEnd.ViewModels.Home
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderInfo SLiderInfo { get; set; }
        public List<Service> Services { get; set; }
        public Banner Banner { get; set; }
        public List<BestSeller> BestSellers { get; set; }
        public List<Fact> Facts { get; set; }
        public List<Categories> Categories { get; set; }
        public List<Feature> Features { get; set; }
    }
}

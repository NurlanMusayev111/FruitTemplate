using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;       
        }
        public async Task<IActionResult> Index()
        {
            SliderInfo sliderInfo = await _context.SliderInfo.FirstOrDefaultAsync();
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<Service> services = await _context.Services.ToListAsync();
            Banner banner = await _context.Banners.FirstOrDefaultAsync();
            List<BestSeller> bestSeller = await _context.BestSellers.ToListAsync();
            List<Fact> facts = await _context.Facts.ToListAsync();
            List<Categories> categories = await _context.Categories.ToListAsync();
            List<Feature> features = await _context.Features.ToListAsync();
            List<Product> products = await _context.Products.Include(m=>m.ProductImages).ToListAsync();
            

            HomeVM model = new()
            {
                Sliders = sliders,
                SLiderInfo = sliderInfo,
                Services = services,
                Banner = banner,
                BestSellers = bestSeller,
                Facts = facts,
                Categories = categories,
                Features = features,
                Products = products,
            };

            return View(model);
        }
    }
}

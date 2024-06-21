using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels;
using FruitTemplate_BackEnd.ViewModels.Shops;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Categories> categories = await _context.Categories.ToListAsync();
            Product product = await _context.Products.FirstOrDefaultAsync();
            List<BestSeller> bestSellers = await _context.BestSellers.ToListAsync();
            List<Product> products = await _context.Products.Include(m=>m.Category)
                                                            .Include(m=>m.ProductImages)
                                                            .ToListAsync();

            ShopVM model = new()
            {
                Categories = categories,
                Products = products,
                Product = product,
                BestSellers = bestSellers
            };

            return View(model);
        }
    }
}

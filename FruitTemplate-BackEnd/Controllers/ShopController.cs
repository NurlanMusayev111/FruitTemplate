using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels;
using FruitTemplate_BackEnd.ViewModels.Category;
using FruitTemplate_BackEnd.ViewModels.Shops;
using Microsoft.AspNetCore.Identity;
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


        public async Task<IActionResult> ProductDetail(int? id)
        {
            List<Product> products = await _context.Products.Include(m=>m.ProductImages)
                                                            .Include(m=>m.Category)
                                                            .ToListAsync();
            Product product = await _context.Products.Where(m=>m.Id == id).FirstOrDefaultAsync();
            List<Categories> categories = await _context.Categories.ToListAsync();


            ShopVM datas = new()
            {
                Products = products,
                Product = product,
                Categories = categories
            };

            return View(datas);
        }

    }
}

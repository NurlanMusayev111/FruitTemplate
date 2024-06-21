using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.BestSellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    public class BestSellerController : Controller
    {
        private readonly AppDbContext _context;

        public BestSellerController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<BestSeller> bestSellers = await _context.BestSellers.ToListAsync();

            List<BestSellerVM> model = bestSellers.Select(m => new BestSellerVM { Image = m.Image, Title = m.Title, Price = m.Price }).ToList();
            return View(model);
        }
    }
}

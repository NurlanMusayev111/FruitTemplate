using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
	public class BannerController : Controller
	{

		private readonly AppDbContext _context;

        public BannerController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
		{
			Banner banner = await _context.Banners.FirstOrDefaultAsync();

			BannerVM model = new()
			{
				Id = banner.Id,
				Title = banner.Title,
				Description = banner.Description,
				Price = banner.Price,
				Image = banner.Image,
			};

			return View(model);
		}
	}
}

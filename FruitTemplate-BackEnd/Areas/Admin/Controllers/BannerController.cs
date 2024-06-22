using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Helpers;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Banners;
using FruitTemplate_BackEnd.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BannerController : Controller
	{

		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;

        public BannerController(AppDbContext context, 
								IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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


		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			Banner banner = await _context.Banners.Where(m=>m.Id == id)
												  .FirstOrDefaultAsync();

			if (banner is null) return NotFound();

			BannerDetailVM model = new()
			{
				Image = banner.Image,
				Title = banner.Title,
				Description = banner.Description,
				Price = banner.Price,
			};

			return View(model);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int? id)
		{
			if(id is null) return BadRequest();	

			Banner banner = await _context.Banners.Where(m=>m.Id == id).FirstOrDefaultAsync();


            if (banner is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", banner.Title);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);


            _context.Banners.Remove(banner);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File must be only image format");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.NewImage.SaveFileToLocalAsync(path);

            await _context.Banners.AddAsync(new Banner { Image = fileName, Title = request.Title,Description = request.Description,Price = request.Price });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
            if (id is null) return BadRequest();

            Banner banner = await _context.Banners.Where(m => m.Id == id)
                                                  .FirstOrDefaultAsync();

            if (banner is null) return NotFound();

            return View(new BannerEditVM { Title = banner.Title,Description = banner.Description,Price = banner.Price,Image = banner.Image });
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, BannerEditVM request)
		{
            if (id is null) return BadRequest();

            Banner banner = await _context.Banners.Where(m => m.Id == id)
                                                  .FirstOrDefaultAsync();

            if (banner is null) return NotFound();

            if (request.NewImage is not null)
            {

                if (!request.NewImage.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("NewImage", "File must be only image format");
                    request.Image = banner.Image;
                    return View(request);
                }


                string oldPath = Path.Combine(_env.WebRootPath, "img", banner.Image);

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (FileStream stream = new(newPath, FileMode.Create))
                {
                    await request.NewImage.CopyToAsync(stream);
                }

                banner.Image = fileName;
            };



            banner.Title = request.Title;
            banner.Description = request.Description;
            banner.Price = request.Price;



            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

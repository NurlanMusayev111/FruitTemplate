using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Helpers;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.BestSellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BestSellerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BestSellerController(AppDbContext context,
                                    IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<BestSeller> bestSellers = await _context.BestSellers.ToListAsync();

            List<BestSellerVM> model = bestSellers.Select(m => new BestSellerVM {Id = m.Id, Image = m.Image, Title = m.Title, Price = m.Price }).ToList();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BestSeller bestSeller = await _context.BestSellers.Where(m => m.Id == id)
                                                              .FirstOrDefaultAsync();

            if(bestSeller is null) return NotFound();

            BestSellerDetailVM model = new()
            {
                Image = bestSeller.Image,
                Title = bestSeller.Title,
                Price = bestSeller.Price,
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            BestSeller bestseller = await _context.BestSellers.Where(m => m.Id == id)
                                                              .FirstOrDefaultAsync();

            if(bestseller is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", bestseller.Title);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _context.BestSellers.Remove(bestseller);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BestSellerCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File must be only image format");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.BestSellers.AddAsync(new BestSeller { Image = fileName, Title = request.Title,Price = request.Price });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            BestSeller bestSeller = await _context.BestSellers.Where(m => m.Id == id)
                                                              .FirstOrDefaultAsync();

            if (bestSeller is null) return NotFound();

            return View(new BestSellerEditVM { Image = bestSeller.Image, Title = bestSeller.Title, Price = bestSeller.Price });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,BestSellerEditVM request)
        {
            if (id is null) return BadRequest();

            BestSeller bestSeller = await _context.BestSellers.Where(m => m.Id == id)
                                                  .FirstOrDefaultAsync();

            if (bestSeller is null) return NotFound();

            if (request.NewImage is not null)
            {

                if (!request.NewImage.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("NewImage", "File must be only image format");
                    request.Image = bestSeller.Image;
                    return View(request);
                }


                string oldPath = Path.Combine(_env.WebRootPath, "img", bestSeller.Image);

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (FileStream stream = new(newPath, FileMode.Create))
                {
                    await request.NewImage.CopyToAsync(stream);
                }

                bestSeller.Image = fileName;
            };



            bestSeller.Price = request.Price;
            bestSeller.Title = request.Title;



            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Helpers;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, 
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<Slider> sliders = await _context.Sliders.OrderByDescending(m=>m.Id).ToListAsync();

            List<SliderVM> model = sliders.Select(m=> new SliderVM { Id = m.Id,Image = m.Image,Name = m.Name}).ToList();
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if(!ModelState.IsValid)
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

            await _context.Sliders.AddAsync(new Slider { Image = fileName, Name = request.Name });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Slider sliders = await _context.Sliders.Where(m => m.Id == id)
                                                         .FirstOrDefaultAsync();

            if (sliders is null) return NotFound();

            SliderDetailVM model = new()
            {
                Image = sliders.Image,
                Name = sliders.Name,
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Slider sliders = await _context.Sliders.Where(m=>m.Id == id)
                                                   .FirstOrDefaultAsync();

            if(sliders is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", sliders.Name);

            if(System.IO.File.Exists(path))
               System.IO.File.Delete(path); 


            _context.Sliders.Remove(sliders);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null) return BadRequest(); 

            Slider sliders = await _context.Sliders.Where(m=>m.Id == id)
                                                   .FirstOrDefaultAsync();

            if(sliders is null) return NotFound();

            return View(new SliderEditVM { Name = sliders.Name,Image = sliders.Image });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,SliderEditVM request)
        {
            if (id is null) return BadRequest();

            Slider slider = await _context.Sliders.Where(m=>m.Id == id)
                                                  .FirstOrDefaultAsync();

            if(slider is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("NewImage", "File must be only image format");
                    request.Image = slider.Image;
                    return View(request);
                }


                string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (FileStream stream = new(newPath, FileMode.Create))
                {
                    await request.NewImage.CopyToAsync(stream);
                }

                slider.Image = fileName;

            };

            slider.Name = request.Name;
 

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}

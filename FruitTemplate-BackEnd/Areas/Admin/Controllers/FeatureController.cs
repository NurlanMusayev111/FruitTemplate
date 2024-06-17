using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Category;
using FruitTemplate_BackEnd.ViewModels.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;

        public FeatureController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Feature> features = await _context.Features.ToListAsync();

            List<FeatureVM> model = features.Select(m => new FeatureVM { Id = m.Id, Title = m.Title, Description = m.Description }).ToList();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Feature features = await _context.Features.Where(m => m.Id == id)
                                                      .FirstOrDefaultAsync();

            if(features is null) return NotFound();

            FeatureDetailVM model = new()
            {
                Title = features.Title,
                Description = features.Description,
            };


            return View(model);
         }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Feature features = await _context.Features.Where(m => m.Id == id)
                                                      .FirstOrDefaultAsync();

            if (features is null) return NotFound();

            _context.Features.Remove(features);
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
        public async Task<IActionResult> Create(FeatureCreateVM featureVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Feature features = new()
            {
                Icon = featureVM.Icon,
                Title = featureVM.Title,
                Description = featureVM.Description,
            };

            await _context.Features.AddAsync(features);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null) return BadRequest();

            Feature features = await _context.Features.Where(m=>m.Id == id)
                                                      .FirstOrDefaultAsync();

            if (features is null) return NotFound();

            return View(new FeatureEditVM { Icon =  features.Icon, Title = features.Title, Description = features.Description });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FeatureEditVM featureVM,int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            Feature existFeature = await _context.Features.Where(m=>m.Id == id)
                                                          .FirstOrDefaultAsync();

            if (existFeature is null) return NotFound();

            existFeature.Icon = featureVM.Icon;
            existFeature.Title = featureVM.Title;
            existFeature.Description = featureVM.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

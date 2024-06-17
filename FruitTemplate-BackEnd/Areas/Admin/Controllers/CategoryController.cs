using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Categories> categories = await _context.Categories.OrderByDescending(m => m.Id).ToListAsync();
            
            List<CategoryVM> model = categories.Select(m=> new CategoryVM { Id = m.Id,Name = m.Name}).ToList();
            return View(model);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Categories categories = await _context.Categories.Where(m=>m.Id == id)
                                                             .FirstOrDefaultAsync();

            if(categories is null) return NotFound();

            CategoryDetailVM model = new()
            {
                Name = categories.Name,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Categories categories = new()
            {
                Name = categoryVM.Name,
            };

            await _context.Categories.AddAsync(categories);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();

			Categories categories = await _context.Categories.Where(m => m.Id == id)
														 .FirstOrDefaultAsync();

			if (categories is null) return NotFound();

			_context.Categories.Remove(categories);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			Categories categories = await _context.Categories.Where(m => m.Id == id)
														 .FirstOrDefaultAsync();

			if (categories is null) return NotFound();

			return View(new CategoryEditVM { Id = categories.Id, Name = categories.Name });
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }


            if (id is null) return BadRequest();

            Categories existCategory = await _context.Categories.Where(m => m.Id == id)
                                                               .FirstOrDefaultAsync();

            if (existCategory is null) return NotFound();

            existCategory.Name = category.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}

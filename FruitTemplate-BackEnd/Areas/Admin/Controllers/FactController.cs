using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Category;
using FruitTemplate_BackEnd.ViewModels.Facts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FactController : Controller
    {
        private readonly AppDbContext _context;

        public FactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Fact> facts = await _context.Facts.ToListAsync();

            List<FactVM> model = facts.Select(m=> new FactVM {Id = m.Id,Title = m.Title, Count = m.Count }).ToList();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Fact facts = await _context.Facts.Where(m=>m.Id == id)
                                             .FirstOrDefaultAsync();

            if(facts is null) return NotFound();

            FactDetailVM model = new()
            {
                Icon = facts.Icon,
                Title = facts.Title,
                Count = facts.Count
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Fact facts = await _context.Facts.Where(m => m.Id == id)
                                             .FirstOrDefaultAsync();

            if (facts is null) return NotFound();

            return View(new FactEditVM {Id = facts.Id,Icon = facts.Icon, Title = facts.Title,Count = facts.Count});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FactEditVM factVM,int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            Fact existsFacts = await _context.Facts.Where(m => m.Id == id)
                                             .FirstOrDefaultAsync();

            if (existsFacts is null) return NotFound();

            existsFacts.Icon = factVM.Icon;
            existsFacts.Title = factVM.Title;
            existsFacts.Count = factVM.Count;

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
        public async Task<IActionResult> Create(FactCreateVM factVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Fact facts = new()
            {
                Icon = factVM.Icon,
                Title = factVM.Title,
                Count = factVM.Count
            };

            await _context.Facts.AddAsync(facts);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Fact facts = await _context.Facts.Where(m => m.Id == id)
                                             .FirstOrDefaultAsync();

            if (facts is null) return NotFound();

            _context.Facts.Remove(facts);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }
    }
}

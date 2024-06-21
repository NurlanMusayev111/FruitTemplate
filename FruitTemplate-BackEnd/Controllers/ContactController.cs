using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();

            ContactVM model = new()
            {
                Settings = settings
            };

            return View(model);

            
        }
    }
}

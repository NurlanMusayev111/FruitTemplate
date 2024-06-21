using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {

        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();

            List<ServiceVM> model = services.Select(m => new ServiceVM { Icon = m.Icon, Title = m.Title, Description = m.Description }).ToList();

            return View(model);
        }
    }
}

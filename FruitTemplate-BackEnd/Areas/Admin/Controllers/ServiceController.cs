using FruitTemplate_BackEnd.Data;
using FruitTemplate_BackEnd.Helpers;
using FruitTemplate_BackEnd.Models;
using FruitTemplate_BackEnd.ViewModels.Services;
using FruitTemplate_BackEnd.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceController(AppDbContext context,
                                 IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();

            List<ServiceVM> model = services.Select(m => new ServiceVM { Id = m.Id, Icon = m.Icon, Title = m.Title, Description = m.Description }).ToList();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Service service = await _context.Services.FirstOrDefaultAsync();

            if (service is null) return NotFound();

            ServiceDetailVM model = new()
            {
                Icon = service.Icon,
                Title = service.Title,
                Description = service.Description
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Service service = await _context.Services.Where(m => m.Id == id)
                                                     .FirstOrDefaultAsync();
            if (service is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", service.Title);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);


            _context.Services.Remove(service);

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
        public async Task<IActionResult> Create(ServiceCreateVM request)
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

            await _context.Services.AddAsync(new Service { Icon = fileName, Title = request.Title, Description = request.Description });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Service service = await _context.Services.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (service is null) return NotFound();

            return View(new ServiceEditVM { Title = service.Title, Description = service.Description, Image = service.Icon });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceEditVM request)
        {
            if (id is null) return BadRequest();

            Service service = await _context.Services.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (service is null) return NotFound();

            if (request.NewImage is null) return RedirectToAction("Index");


            if (!request.NewImage.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("NewImage", "File must be only image format");
                request.Image = service.Icon;
                return View(request);
            }


            string oldPath = Path.Combine(_env.WebRootPath, "img", service.Icon);

            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);

            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new(newPath, FileMode.Create))
            {
                await request.NewImage.CopyToAsync(stream);
            }

            service.Title = request.Title;
            service.Description = request.Description;
            service.Icon = fileName;


            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

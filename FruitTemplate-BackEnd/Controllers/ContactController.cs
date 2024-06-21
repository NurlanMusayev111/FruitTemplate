using Microsoft.AspNetCore.Mvc;

namespace FruitTemplate_BackEnd.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using FruitTemplate_BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {

        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value)));
        }
    }
}

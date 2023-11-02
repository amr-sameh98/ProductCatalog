using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.Data;

namespace ProductCatalog.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ApplicationDbContext _context;
        public CategoriesServices(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<List<Category>> GetAll()
        //{
        //    return await _context.Categories.ToListAsync();
        //}

        public async Task<IEnumerable<SelectListItem>> GetSelectList()
        {
            return await _context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .ToListAsync();
        }
    }
}

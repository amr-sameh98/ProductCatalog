using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductCatalog.Services
{
    public interface ICategoriesServices
    {
        Task<IEnumerable<SelectListItem>> GetSelectList();
        //Task<List<Category>> GetAll();
    }
}

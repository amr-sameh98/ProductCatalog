namespace ProductCatalog.Controllers
{
    [Authorize]
    public class ProductController : Controller
	{
		IProductsServices productsService;
        ICategoriesServices categoriesServices;
        

        public ProductController(IProductsServices _productsServices , ICategoriesServices _categoriesServices)
		{
			productsService = _productsServices;
            categoriesServices = _categoriesServices;

        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(int id)
		{
             IEnumerable<SelectListItem> categories = await categoriesServices.GetSelectList();
            ViewData["Categories"] = categories;
            return  View(await productsService.GetAll(id));
		}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            AddProductFormViewModel viewModel = new()
            {
                Categories = await categoriesServices.GetSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddProductFormViewModel product)
        {
            if (!ModelState.IsValid)
            {
                product.Categories = await categoriesServices.GetSelectList();
                return View(product);
            }
            await productsService.Add(product);
            return RedirectToAction(nameof(GetAll));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await productsService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }
        public async Task<IActionResult> Details(int id)
        {
            return  View(await productsService.GetById(id));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await productsService.GetById(id);
            
            AddProductFormViewModel viewModel = new()
            {
                Id = product.Id,
                Name = product.Name,
                StartDate = product.StartDate,
                DurationInDays = product.DurationInDays,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Categories = await categoriesServices.GetSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddProductFormViewModel product)
        {
            if (!ModelState.IsValid)
            {
                product.Categories = await categoriesServices.GetSelectList();
                return View(product);
            }
            await productsService.Edit(product);
            return RedirectToAction(nameof(GetAll));
        }
        public async Task<IActionResult> GetAllInDuration(int id)
        {
            IEnumerable<SelectListItem> categories = await categoriesServices.GetSelectList();
            ViewData["Categories"] = categories;
            return View( nameof(GetAll) ,  await productsService.GetAllInDuration(id));
        }
    }
}

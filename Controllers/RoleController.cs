

namespace ProductCatalog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel newRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole() { Name = newRole.RoleName };
                IdentityResult result = await roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return View();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
    }
}

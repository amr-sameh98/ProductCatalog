using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ProductCatalog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;



        public AccountController(UserManager<IdentityUser> _userManager , SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;

        }
        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return  View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterAccountViewModel newAccount)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = newAccount.UserName;
                user.Email = newAccount.Email;

                IdentityResult result = await userManager.CreateAsync(user , newAccount.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("GetAllInDuration", "Product");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(newAccount);
        }
        [HttpGet]
        public async Task<IActionResult> Login(string ReturnUrl = "~/Product/GetAllInDuration")
        {
            ViewData["RedirectUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser , string ReturnUrl = "~/Product/GetAllInDuration")
        {
            IdentityUser user = await userManager.FindByNameAsync(loginUser.UserName);
            if (user != null)
            {
             SignInResult result = await signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.isPersistent , false);
                if (result.Succeeded)
                {
                    var userRole = await userManager.GetRolesAsync(user);
                    if (userRole.Contains("Admin"))
                    {
                        return RedirectToAction("GetAll", "Product");
                    }
                    else
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "invalid username or password");
                }
            }
            else
            {
                ModelState.AddModelError("", "invalid username or password");
            }
            return View(loginUser);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        //------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> AdminRegistration()
        {
            return View("Registration");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRegistration(RegisterAccountViewModel newAccount)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = newAccount.UserName;
                user.Email = newAccount.Email;

                IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("GetAll", "Product");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Registration", newAccount);
        }
    }
}

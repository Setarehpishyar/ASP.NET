using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Business.Models; // اطمینان حاصل کن که LoginForm در این namespace هست

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginForm());
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); // یا هر صفحه‌ای که می‌خوای بعد از لاگین بری
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // برای ثبت‌نام اگر خواستی اضافه کنیم هم می‌تونم برات بنویسم
    }
}

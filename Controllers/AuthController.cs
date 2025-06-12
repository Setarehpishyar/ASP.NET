using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<MemberEntity> _signInManager;
        private readonly UserManager<MemberEntity> _userManager;

        public AuthController(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterForm());
        }

        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterForm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new MemberEntity
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // POST: /Auth/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        // 🔹 Google Login - ExternalLogin
        [HttpGet]
        public IActionResult ExternalLogin(string provider, string? returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        // 🔹 Google Login - Callback
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = "/", string? remoteError = null)
        {
            returnUrl ??= "/";

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            // If user does not exist, create one
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);

            if (email == null)
                return RedirectToAction(nameof(Login));

            var user = new MemberEntity
            {
                UserName = email,
                Email = email,
                FullName = name ?? email
            };

            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                createResult = await _userManager.AddLoginAsync(user, info);
                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }

            foreach (var error in createResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return RedirectToAction(nameof(Login));
        }
    }
}

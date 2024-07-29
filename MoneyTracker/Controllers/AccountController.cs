using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Data.Models;
using MoneyTracker.ViewModels.AccountVM;

namespace MoneyTracker.Controllers
{
    [Route("/account")]
    public class AccountController(UserManager<User> userManager, SignInManager<User> signInManager) : Controller
    {
        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromForm] SignInPageVM signInVM, string? returnUrl)
        {
            if (!ModelState.IsValid) return View();

            var user = await userManager.FindByEmailAsync(signInVM.Email);

            if (user is not null)
            {
                var signInResult = await signInManager.CheckPasswordSignInAsync(user, signInVM.Password, false);
                if (signInResult.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return LocalRedirect(returnUrl ?? "/money-tracker");
                }
            }

            ModelState.AddModelError("Password", "E-posta ya da şifre bilgisi yanlış!");
            return View();
        }

        [HttpGet("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] SignUpPageVM signUpVM)
        {
            if (!ModelState.IsValid) return View();

            var result = await userManager.CreateAsync(new User() { Email = signUpVM.Email, UserName = signUpVM.Email }, signUpVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code.StartsWith("Password"))
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", error.Description);
                    }
                }
                return View();
            }
            return LocalRedirect("/money-tracker");
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOutAsync(string? returnUrl)
        {
            await signInManager.SignOutAsync();

            return LocalRedirect(returnUrl ?? "/money-tracker");
        }
    }
}

using System.Threading.Tasks;
using _07_IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _07_IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl) => View(new LoginViewModel{ReturnUrl = returnUrl});

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl) => View(new RegisterViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser(model.Username);
            var createUserResult = await _userManager.CreateAsync(user, model.Password);

            if (createUserResult.Succeeded)
            {
                await _signInManager.SignInAsync(user,false);
                return Redirect(model.ReturnUrl);
            }

            return View(model);
        }
    }
}

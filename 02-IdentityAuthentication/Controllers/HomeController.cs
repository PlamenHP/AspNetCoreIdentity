using System.Threading.Tasks;
using _02_IdentityAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _02_IdentityAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var signedIn = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signedIn.Succeeded)
                {
                    return RedirectToAction(nameof(Secret));
                }
            }

            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        } 
        
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // Creating the user in DB
            var user = new AppUser {UserName = username};
            var result = await _userManager.CreateAsync(user, password);

            // Sign in the user
            if (result.Succeeded)
            {
                var signedIn = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signedIn.Succeeded)
                {
                    return RedirectToAction(nameof(Secret));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return Redirect(nameof(Index));
        }
    }
}

using System.Threading.Tasks;
using _03_EmailConfirmation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace _03_EmailConfirmation.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IEmailService _emailService;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Index() => View();

        [Authorize]
        public IActionResult Secret() => View();

        public IActionResult Login() => View();

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

            return Redirect(nameof(Register));
        }

        public IActionResult Register() => View();
        
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // Creating the user in DB
            var user = new AppUser {UserName = username};
            var result = await _userManager.CreateAsync(user, password);

            // Sign in the user
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(VerifyEmail),"Home", new { userId = user.Id, token},Request.Scheme,Request.Host.ToString());

                await _emailService.SendAsync("test@test.com", "Validation Email", $"<a href=\"{link}\">Verify Email</a>", true);
                
                return RedirectToAction(nameof(EmailVerification));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EmailVerification() => View();

        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect(nameof(Index));
        }
    }
}

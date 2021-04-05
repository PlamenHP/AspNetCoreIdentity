using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var testClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "Bob@mail.com"),
                new Claim("bob.bob", "is bob")
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "Bob@mail.com"),
                new Claim("bob.bob", "is bob")
            };

            var testIdentity = new ClaimsIdentity(testClaims, "test claim");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "license claim");

            var userPrincipal = new ClaimsPrincipal(new[] 
            { 
                testIdentity, 
                licenseIdentity
            });

            HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _04_AuthorizationPolicyClaims.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();


        [Authorize]
        public IActionResult Secret() => View();

        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy() => View(nameof(Secret));

        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole() => View(nameof(Secret));

        public IActionResult Authenticate()
        {
            var testClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "Bob@mail.com"),
                new Claim("bob.bob", "is bob"),
                new Claim(ClaimTypes.DateOfBirth, "11/11/2000"),
                new Claim(ClaimTypes.Role,"Admin")
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

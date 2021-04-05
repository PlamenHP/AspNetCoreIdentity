using Microsoft.AspNetCore.Identity;

namespace _03_EmailConfirmation.Models
{
    public class AppUser : IdentityUser
    {
        public string StudentNumber { get; set; }
    }
}

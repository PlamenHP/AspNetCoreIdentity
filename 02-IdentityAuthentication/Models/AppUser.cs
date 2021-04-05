using Microsoft.AspNetCore.Identity;

namespace _02_IdentityAuthentication.Models
{
    public class AppUser : IdentityUser
    {
        public string StudentNumber { get; set; }
    }
}

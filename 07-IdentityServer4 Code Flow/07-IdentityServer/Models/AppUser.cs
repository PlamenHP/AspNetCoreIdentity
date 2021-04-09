using Microsoft.AspNetCore.Identity;

namespace _07_IdentityServer.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser(string userName)
            :base(userName)
        {
        }
        public string StudentNumber { get; set; }
    }
}

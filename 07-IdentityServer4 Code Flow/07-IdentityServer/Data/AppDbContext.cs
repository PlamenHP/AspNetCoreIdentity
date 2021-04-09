using _07_IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _07_IdentityServer.Data
{
    // IdentityDbContext contains all the user tables
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }
    }
}

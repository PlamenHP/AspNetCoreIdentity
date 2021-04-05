using _03_EmailConfirmation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _03_EmailConfirmation.Data
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

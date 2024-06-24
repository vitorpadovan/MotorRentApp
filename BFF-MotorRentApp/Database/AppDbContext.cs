using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BFF_MotorRentApp.Database
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
            //TODO better way to configure migrations
            //this.Database.Migrate();
        }
    }
}

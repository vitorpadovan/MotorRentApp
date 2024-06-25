using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorRentApp.Core.Model;

namespace MotorRentApp.Core.Database
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<BackgroundTask> BackgroundTasks { get; private set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
            //TODO better way to configure migrations
            //this.Database.Migrate();
        }
    }
}

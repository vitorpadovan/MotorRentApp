using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorRentApp.Core.Model;
using Microsoft.Extensions.Logging;

namespace MotorRentApp.Core.Database
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly ILogger<AppDbContext> _logger;
        public DbSet<BackgroundTask> BackgroundTasks { get; private set; }

        public AppDbContext(DbContextOptions options, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;

            this.Database.EnsureCreated();

            //TODO better way to configure migrations
            //this.Database.Migrate();
        }
    }
}

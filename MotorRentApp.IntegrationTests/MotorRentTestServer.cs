using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MotorRentApp.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.IntegrationTests
{
    public class MotorRentTestServer : TestServer,IDisposable
    {
        public AppDbContext ProductContext { get; set; }
        public MotorRentTestServer(IWebHostBuilder builder) : base(builder)
        {
            ProductContext = Host.Services.GetRequiredService<AppDbContext>();
        }

        public new void Dispose()
        {
            ProductContext.Database.EnsureDeleted();
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}

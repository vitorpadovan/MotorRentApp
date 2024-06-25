using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MotorRentApp.IntegrationTests
{
    public class MotorRentScenariosBase
    {
        public static MotorRentTestServer CreateServer()
        {
            IWebHostBuilder hostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder
                        .AddJsonFile("appsettings.Testing.json", optional: false)
                        .AddEnvironmentVariables();
                })
                .UseStartup<StartupApiTests>();

            MotorRentTestServer testServer = new(hostBuilder);

            return testServer;
        }
    }
}

using BFF_MotorRentApp;
using MessageService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotorRentApp.Core.Business;
using MotorRentApp.Core.Database;
using MotorRentApp.Core.Repository;
using MotorRentApp.Imp.Business;
using MotorRentApp.Imp.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.IntegrationTests
{
    public class StartupApiTests
    {
        private IConfiguration Configuration { get; }
        public StartupApiTests(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IVehicleBusiness, VehicleBusiness>();
            services.AddScoped<IBackgroundTaskBusiness, BackgroundTaskBusiness>();
            services.AddScoped<IBackgroundTasksRepository, BackgroundTasksRepository>();
            services.AddScoped<IVehicleBusiness, VehicleBusiness>();
            services.AddRabbitMqService();
            services.AddControllers().AddApplicationPart(typeof(Program).Assembly);
            var mySqlConnection = Configuration.GetConnectionString("Mysql");
            services.AddDbContext<AppDbContext>(
                opt => opt.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
            services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

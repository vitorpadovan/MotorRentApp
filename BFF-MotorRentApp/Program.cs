using MessageService.Definition;
using MessageService.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MotorRentApp.Core.Business;
using MotorRentApp.Imp.Business;
using MessageService;
using MotorRentApp.Core.Repository;
using MotorRentApp.Imp.Repository;
using MotorRentApp.Core.Database;
using Microsoft.AspNetCore.Identity;
using MotorRentApp.Core.Services;
using MotorRentApp.Imp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BFF_MotorRentApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            #region Swagger Gen
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("user", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Motor Rent App",
                    Description = "User Api"
                });

                c.SwaggerDoc("vehicle", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Motor Rent App",
                    Description = "Vehicle Api"
                });
            });
            #endregion

            #region MySql Config
            var mySqlConnection = builder.Configuration.GetConnectionString("Mysql");
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseMySql(
                    mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection),
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 20,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null)
                    );
            });
            #endregion

            #region Auth Config
            var authKey = builder.Configuration["authKey"];
            var key = Encoding.ASCII.GetBytes(authKey!);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            builder.Services.AddDefaultIdentity<IdentityUser>(
                opt =>
                {
                    // Password settings.
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireNonAlphanumeric = true;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    opt.Lockout.MaxFailedAccessAttempts = 5;
                    opt.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    opt.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#!";
                    opt.User.RequireUniqueEmail = true;

                    // SignIn settings.
                    opt.SignIn.RequireConfirmedAccount = true;
                    opt.SignIn.RequireConfirmedEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IBackgroundTasksRepository, BackgroundTasksRepository>();
            builder.Services.AddScoped<IBackgroundTaskBusiness, BackgroundTaskBusiness>();
            builder.Services.AddScoped<IVehicleBusiness, VehicleBusiness>();
            builder.Services.AddScoped<IUserBusiness, UserBusiness>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddRabbitMqService();

            var app = builder.Build();

            //app.MapIdentityApi<IdentityUser>().WithGroupName("user").WithTags("User");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/user/swagger.json", "User Api");
                    x.SwaggerEndpoint("/swagger/vehicle/swagger.json", "Vehicle Api");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

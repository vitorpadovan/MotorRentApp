using Microsoft.AspNetCore.Identity;
using MotorRentApp.Core.Business;
using MotorRentApp.Core.Enums;
using MotorRentApp.Core.Extensions;
using MotorRentApp.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Imp.Business
{
    public class UserBusiness : IUserBusiness
    {
        //TODO verificar isso https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserBusiness(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }
        public async Task<bool> CreateUser(string email, string userName, string password)
        {
            var user = new IdentityUser()
            {
                Email = email,
                UserName = userName,
                PasswordHash = password.HashPassword(),
                EmailConfirmed = true,

            };
            var result = await _userManager.CreateAsync(user);
            var role = nameof(UserProfiles.COMMONUSER);
            //var teste = await _roleManager.FindByNameAsync(role);
            //if (teste == null)
            //    await _roleManager.CreateAsync(new() { Name = role, NormalizedName = role });
            await _userManager.AddToRolesAsync(user, [nameof(UserProfiles.COMMONUSER)]);
            return result.Succeeded;
        }

        public async Task<string> GetToken(IdentityUser user)
        {
            //TODO corrigir esse task completed
            await Task.CompletedTask;
            return _tokenService.GenerateToken(user);
        }

        public async Task<bool> Login(string email, string userName, string password)
        {
            var user = new IdentityUser()
            {
                Email = email,
                UserName = userName,
                PasswordHash = password.HashPassword(),
                EmailConfirmed = true
            };

            var r = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            //var token = await _userManager.GenerateUserTokenAsync(user, "teste", "teste");
            if (r.Succeeded)
                return true;
            else
                return false;
        }

    }
}

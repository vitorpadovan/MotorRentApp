using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BFF_MotorRentApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            this.userManager = userManager;
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        //public static bool VerifyHashedPassword(string hashedPassword, string password)
        //{
        //    byte[] buffer4;
        //    if (hashedPassword == null)
        //    {
        //        return false;
        //    }
        //    if (password == null)
        //    {
        //        throw new ArgumentNullException("password");
        //    }
        //    byte[] src = Convert.FromBase64String(hashedPassword);
        //    if ((src.Length != 0x31) || (src[0] != 0))
        //    {
        //        return false;
        //    }
        //    byte[] dst = new byte[0x10];
        //    Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        //    byte[] buffer3 = new byte[0x20];
        //    Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        //    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
        //    {
        //        buffer4 = bytes.GetBytes(0x20);
        //    }
        //    return ByteArraysEqual(buffer3, buffer4);
        //}

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            //var r = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            //var identi = new IdentityUser()
            //{
            //    PasswordHash = HashPassword(login.Password),
            //    Email = login.Email, UserName = login.Email
            //};
            //var werwer = await userManager.CheckPasswordAsync(identi, login.Password);
            var r = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (r.Succeeded) 
                return Ok();
            else
                return Unauthorized();
        }

        [HttpPost]
        [Route("singUp")]
        public async Task<IActionResult> SingUp([FromBody] RegisterRequest login)
        {
            var r = await userManager.CreateAsync(new IdentityUser() { 
               Email = login.Email,
               UserName= login.Email,
               PasswordHash = HashPassword(login.Password),
               EmailConfirmed = true
            });
            return Created();
        }
    }
}

using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MotorRentApp.Core.Business;
using MotorRentApp.Core.Extensions;

namespace BFF_MotorRentApp.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "user")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {

        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var success = await _userBusiness.Login(login.Email, login.Email, login.Password);
            if(!success)
                return Unauthorized();
            var token = await _userBusiness.GetToken(new IdentityUser() { Email = login.Email, UserName = login.Email });
            AccessTokenResponse? response = new()
            {
                AccessToken = token,
                ExpiresIn = 3200,
                RefreshToken = token
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("singUp")]
        public async Task<IActionResult> SingUp([FromBody] RegisterRequest login)
        {
            await _userBusiness.CreateUser(login.Email, login.Email, login.Password);
            return Ok();
        }
    }
}

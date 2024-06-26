using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Core.Business
{
    public interface IUserBusiness
    {
        public Task<bool> CreateUser(string email, string user, string password);
        public Task<bool> Login(string email, string user, string password);
        public Task<String> GetToken(IdentityUser user);
    }
}

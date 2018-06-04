using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MoneyAintFunny.Data.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyAintFunny.Core.Data.Services.Services
{
    public interface IUserSecurityManager
    {
        Task<IdentityUser> GetUserForLogin(string userName, string password);
        IdentityUser GetCurrentAuthenticatedUser(HttpContext context);
        bool UserIsSystemAdmin(IdentityUser user);
        IEnumerable<string> GetRolesForUser(IdentityUser user);
    }
}

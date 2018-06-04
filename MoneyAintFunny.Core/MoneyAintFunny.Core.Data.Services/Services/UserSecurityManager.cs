using System.Security.Claims;
using MoneyAintFunny.Core.Data.Services.Services;
using MoneyAintFunny.Data.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MoneyAintFunny.Core.Data.Context;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MoneyAintFunny.Core.Base.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace MoneyAintFunny.Core.Data.Services.Services
{
    public class UserSecurityManager : IUserSecurityManager
    {
        private UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISettingsService _settingsService;
        private readonly MoneyAintFunnyContext _context;

        public UserSecurityManager(
            MoneyAintFunnyContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ISettingsService settingsService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _settingsService = settingsService;
        }

        public async Task<IdentityUser> GetUserForLogin(string userName, string password)
        {
            //just return a user.
/*            if (userName == "Admin" && password == "Pa55word!")
            {
                return new IdentityUser { Id = "ID-ONE", UserName = "Admin" };
            }
            else if (userName == "bob" && password == "bob")
            {
                return new IdentityUser { Id = "ID-TWO", UserName = "Bob" };
            }
            else
            {
                return null;
            }
            */

            //find user first...
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            //validate password...
            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

            //if password was ok, return this user.
            if (signInResult.Succeeded)
            {
                return user;
            }

            return null;
        }

        public IdentityUser GetCurrentAuthenticatedUser(HttpContext httpContext)
        {
            var userResult = _userManager.GetUserAsync(httpContext.User).Result;

            if (userResult != null)
                return userResult;

            return null;
        }

        public bool UserIsSystemAdmin(IdentityUser user)
        {
            //            var isInAdminRole = _userManager.IsInRoleAsync(user, "SysTem"
            //          _roleManager.Role
            return false;
        }

        public IEnumerable<string> GetRolesForUser(IdentityUser user)
        {
            //TEMP hack 
/*            if (user.UserName == "Admin")
            {
                return new[] { "Administrators", "Users" };
            }
            else if (user.UserName == "Bob")
            {
                return new[] { "Users" };
            }
            else
            {
                return new List<string>();
            }*/

            //End temp hack
            var roles = _userManager.GetRolesAsync(user).Result;
            return roles;
        }
    }
}

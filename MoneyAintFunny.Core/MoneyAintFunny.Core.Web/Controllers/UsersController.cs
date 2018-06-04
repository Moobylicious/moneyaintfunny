using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyAintFunny.Core.Base.Interfaces;
using MoneyAintFunny.Core.Data.Services.Services;
using MoneyAintFunny.Core.Dto.Models.Request;
using MoneyAintFunny.Core.Web.Jwt;
using System.Threading.Tasks;

namespace MoneyAintFunny.Core.Web.Controllers
{
    [Route("api/users")]
    //[Authorize(Policy = "Member")]
    //[Authorize(Policy = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUserSecurityManager _userSecurityMgr;
        private readonly ISettingsService _settingsService;

        public UsersController(
            IUserSecurityManager userSecurity,
            ISettingsService settingsService)
        {
            _userSecurityMgr = userSecurity;
            _settingsService = settingsService;
        }

        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> GetToken([FromBody] LoginRequestModel model)
        {
            //Get user & validate password.
            var user = await _userSecurityMgr.GetUserForLogin(model.Username, model.Password);

            //if that didn't return a user, then either name or password is not valid, so nah.
            if (user == null)
            {
                return Unauthorized();
            }

            var tokenBuilder = new JwtTokenBuilder()
                              .AddSecurityKey(JwtSecurityKey.Create(_settingsService.JwtSecret))
                              .AddSubject(user.UserName)
                              .AddIssuer(_settingsService.JwtIssuer)
                              .AddAudience(_settingsService.JwtAudience)
                              .AddClaim("MembershipId", user.Id);

            //add all known roles to claim.
            foreach (var role in _userSecurityMgr.GetRolesForUser(user)) {
                tokenBuilder = tokenBuilder.AddClaim("roles", role);
            }

            tokenBuilder = tokenBuilder
                              .AddExpiry(_settingsService.TokenLifeTimeInMinutes);

            var token = tokenBuilder.Build();

            return Ok(token.Value);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestModel model)
        {
            //First, check the role exists
            return null;
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyAintFunny.Core.Data.Services.Services;
using System.Collections.Generic;
using System.Linq;

namespace MoneyAintFunny.Core.Web.Controllers
{
    [Route("api/Claims")]
    [Authorize(Policy = "Administrator")]
//    [Authorize(Policy ="Administrator")]
    public class ClaimsController : Controller
    {
        private readonly IUserSecurityManager _securityManager;

        public ClaimsController(
          IUserSecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

        [HttpGet("Show")]
        public IActionResult ShowClaims()
        {

            //Get User
            var dict = new List<KeyValuePair<string, string>>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(new KeyValuePair<string, string>(item.Type, item.Value)));
            return Ok(dict);

            /*            _logger.LogInformation("Getting Collections");
                        var result = _dataService.GetCollections();
                        return Ok(result);*/
        }
    }
}

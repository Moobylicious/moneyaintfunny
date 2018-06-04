using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MoneyAintFunny.Core.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MoneyAintFunny.Core.Web.Services
{
    public class SignInHandler : IAuthenticationSignInHandler
    {
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }

    public class SettingsService : ISettingsService
    {
        private IConfigurationRoot Configuration;

        public SettingsService(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   //How to override settings - if the appSettings.Production.json file exists, then it'll get loaded
                   //IF the environment is set to production.  Last added list wins.
                   .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public string MainDatabaseConnectionString => Configuration["database:connectionstring"];

        public bool IsDesignTime => (Configuration["DesignTime"]?.ToLower() ?? string.Empty) == "true";

        private string _jwtSecret = string.Empty;

        public string JwtSecret
        {
            get
            {
                if (string.IsNullOrEmpty(_jwtSecret))
                {
                    _jwtSecret = "ThisIsAVeryLongSecretKeyThatThe1337Hax0rsWillNeverGuessBwahahaha";
                }
                return _jwtSecret;
            }
        }

        public string SystemAdminRoleName
        {
            get
            {
                return "SystemAdmin";
            }

        }

        public string JwtIssuer => Configuration["security:Issuer"];
        public string JwtAudience => Configuration["security:Audience"];
        public int TokenLifeTimeInMinutes {
            get
            {
                var fromSetting = Configuration["security:TokenLifeTime"];
                //default to 2 hours if no setting...
                int result = 120;

                int.TryParse(fromSetting, out result);
                return result;
            }
        }
    }
}

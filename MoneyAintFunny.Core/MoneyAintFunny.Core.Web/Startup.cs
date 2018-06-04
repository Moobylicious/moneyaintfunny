using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using MoneyAintFunny.Core.Data.Services.Services;
using MoneyAintFunny.Core.Web.Services;
using MoneyAintFunny.Core.Base.Interfaces;
using MoneyAintFunny.Core.Data.Context;
using MoneyAintFunny.Core.Data.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoneyAintFunny.Core.Web.Jwt;
using Microsoft.AspNetCore.Identity;
using MoneyAintFunny.Core.Base.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MoneyAintFunny.Core.Web
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
        }
        
        private void AddIdentityNonsense(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MoneyAintFunnyContext>();

/*            IdentityBuilder builder = services.AddIdentityCore<IdentityUser>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
            }
            );
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder
                .AddEntityFrameworkStores<QuantumCloudContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IRoleValidator<IdentityRole>, RoleValidator<IdentityRole>>();
            services.AddScoped<RoleManager<IdentityRole>, RoleManager<IdentityRole>>();
            // or
            builder.AddRoleValidator<RoleValidator<IdentityRole>>();
            builder.AddRoleManager<RoleManager<IdentityRole>>();*/

            /*            // Services used by identity
                        services.AddScoped<IUserValidator<IdentityUser>, UserValidator<IdentityUser>>();
                        services.AddScoped<IPasswordValidator<IdentityUser>, PasswordValidator<IdentityUser>>();
                        services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
                        services.AddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
                        // No interface for the error describer so we can add errors without rev'ing the interface
                        services.AddScoped<IdentityErrorDescriber>();
                        services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser>>();
                        services.AddScoped<UserManager<IdentityUser>, UserManager<IdentityUser>>();
                        services.AddScoped<RoleManager<IdentityRole>>();
                        services.AddScoped<SignInManager<IdentityUser>>();
                        services.Configure<IdentityOptions>(opt =>
                        {
                            opt.Password.RequireDigit = true;
                            opt.Password.RequiredLength = 8;
                            opt.Password.RequireNonAlphanumeric = false;
                            opt.Password.RequireUppercase = true;
                            opt.Password.RequireLowercase = true;
                        });

                        var identityBuilder = new IdentityBuilder(typeof(IdentityUser), typeof(IdentityRole), services);
                        identityBuilder.AddEntityFrameworkStores<QuantumCloudContext>();*/
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISettingsService, SettingsService>();

            AddIdentityNonsense(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters =
                            new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,

                                ValidIssuer = "QuantumCloudCore.Security.Bearer",
                                ValidAudience = "QuantumCloudCore.Security.Bearer",
                                IssuerSigningKey =
                                JwtSecurityKey.Create("ThisIsAVeryLongSecretKeyThatThe1337Hax0rsWillNeverGuessBwahahaha")

                            };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                    policy => policy.RequireClaim("MembershipId"));

                options.AddPolicy("Administrator",
                    policy => policy.RequireRole(GlobalConst.AdministratorRoleName));
            });

            services.AddMvc(opt=>
            {
/*                if (_hostingEnvironment.IsDevelopment())
                {
                    opt.SslPort = 44388;
                }
                opt.Filters.Add(new RequireHttpsAttribute());*/
            })
            .AddMvcOptions(o=>o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            //.AddJsonOptions(o =>
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var resolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        resolver.NamingStrategy = null;
            //    }
            //});

            //ToDo:  move DI stuff somehow to the projects that require it...

            var connectionString = new SettingsService(_hostingEnvironment).MainDatabaseConnectionString;
            //Context must be added via AddDbContext...
            services.AddDbContext<MoneyAintFunnyContext>(o=>o.UseSqlServer(connectionString));
            //To enable injection of the context via an interface, we need to do this instead.
            //This is resolving the interface to the context already configured above to ensure that we
            //resolve the correct one, rather than a separate registration that news one up incorrectly.
            services.AddScoped<IMoneyAintFunnyContext>(provider => provider.GetService<MoneyAintFunnyContext>());

            services.AddScoped<IMoneyAintFunnyDataService, MoneyAintFunnyDataService>();
            services.AddScoped<IMoneyAintFunnyQueryService, MoneyAintFunnyQueryService>();

            services.AddAutoMapper();

            services.AddScoped<IUserSecurityManager, UserSecurityManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            MoneyAintFunnyContext context,
            ISettingsService settings,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseAuthentication();

            context.EnsureSeedDataForContext(userManager, roleManager);

            app.UseStatusCodePages();            

            app.UseMvc();
        }
    }
}

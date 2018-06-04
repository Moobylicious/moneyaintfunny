using Microsoft.AspNetCore.Identity;
using MoneyAintFunny.Core.Base.Constants;
using MoneyAintFunny.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyAintFunny.Core.Data.Context
{
    public static class MoneyAintFunnyContextExtensions
    {
        public static void EnsureSeedDataForContext(
            this MoneyAintFunnyContext context, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            IdentityRole systemAdminRole = new IdentityRole(GlobalConst.AdministratorRoleName);
            if (!roleManager.RoleExistsAsync(systemAdminRole.Name).Result)
            {
                var result = roleManager.CreateAsync(systemAdminRole).Result;
                if (!result.Succeeded)
                {
                    throw new Exception($"could not create {GlobalConst.AdministratorRoleName} role!");
                }
            }

            var adminUser = new IdentityUser() { UserName = "Admin", Email = "cloudadmin@eurocoin.co.uk" };

            IdentityUser existingUser = userManager.FindByNameAsync(adminUser.UserName).Result;
            if (existingUser == null)
            {                
                var result = userManager.CreateAsync(adminUser, "Pa55word!").Result;

                if (!result.Succeeded)
                {
                    throw new Exception("could not create admin user!");
                }
                var addToRoleResult = userManager.AddToRoleAsync(adminUser, systemAdminRole.Name).Result;
                if (!result.Succeeded)
                {
                    throw new Exception("could not add admin user to administrators role");
                }            

                context.SaveChanges();
            }
        }

    }
}
using BikeStore.Persistence.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Data.SeedData
{
   public static class RolesSeed
   {

        public static async Task SeedRoles(this IServiceProvider provider) 
        {
            var scope = provider.CreateScope();
            var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole{ Name="SUPERADMIN",NormalizedName="SUPERADMIN"},
                new ApplicationRole{ Name="STOREADMIN",NormalizedName="STOREADMIN"},
                new ApplicationRole{ Name="EMPLOYEE",NormalizedName="EMPLOYEE"},
                new ApplicationRole{ Name="TECHNICIAN",NormalizedName="TECHNICIAN"},
            };

            foreach (var x in roles)
            {

                if (!await RoleManager.RoleExistsAsync(x.Name))
                {

                    await RoleManager.CreateAsync(x);
                }
            }


        }

        public static async Task SeedUsers(this IServiceProvider provider) {
            var scope = provider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<BikeStoresContext>();
            await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users ON");
            var userList = dbContext.Staffs.ToList();
   
            foreach (var x in userList) 
            {
               
                var newUser = new ApplicationUser()
                {
                    Id=x.StaffId,
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                    Email=x.Email,
                    PhoneNumber=x.Phone,
                    UserName= x.Email
                };
                // var hasher = new PasswordHasher<ApplicationUser>();
                //newUser.PasswordHash = hasher.HashPassword(newUser, "Admin@123");
                var result = await userManager.CreateAsync(newUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "EMPLOYEE");
                }
            }
            await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users OFF");
        }


    }
}

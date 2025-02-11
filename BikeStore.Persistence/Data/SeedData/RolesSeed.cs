using BikeStore.Persistence.User;
using Microsoft.AspNetCore.Identity;
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

   }
}

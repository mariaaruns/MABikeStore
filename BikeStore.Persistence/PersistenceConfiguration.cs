using Azure.Core;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Persistence.Data;
using BikeStore.Persistence.Repository;

using BikeStore.Persistence.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) 
        {

            services.AddDbContext<BikeStoresContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthenticationService,AuthenticationService>();
            /*services.AddScoped(typeof(IPaginationService<,>), typeof(PaginationService<,>));*/
            return services;
        }
    }
}

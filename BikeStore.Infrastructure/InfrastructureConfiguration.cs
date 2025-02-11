using BikeStore.Domain.Contracts.IService;
using BikeStore.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure
{
    public static class InfrastructureConfiguration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) 
        {
            services.AddScoped(typeof(IPaginationService<,>), typeof(PaginationService<,>));
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}

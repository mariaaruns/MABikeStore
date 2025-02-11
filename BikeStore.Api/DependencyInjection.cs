

using BikeStore.Api.Exceptions;
using BikeStore.Application;
using BikeStore.Application.CQRS.Behaviours;
using BikeStore.Infrastructure;
using BikeStore.Persistence;
using MediatR;
using System.Reflection;

namespace BikeStore.Api
{
    public static class DependencyInjection
    {

        public static IServiceCollection RegisterDI(this IServiceCollection services,IConfiguration configuration) 
        {

            services.AddPersistenceServices(configuration).AppDependencyInjection().AddInfrastructureServices();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }
    }
}

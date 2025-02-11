using BikeStore.Application.CQRS.Behaviours;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BikeStore.Application
{
    public static  class AppConfiguraion
    {
        public static IServiceCollection AppDependencyInjection(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
            { 
                cfg.RegisterServicesFromAssembly(typeof(AppConfiguraion).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(AppConfiguraion).Assembly);

            //services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}

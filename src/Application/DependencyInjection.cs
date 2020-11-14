using MediatR;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Mappings;
using Application.Common.Behaviours;
using Application.Common.Interfaces;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var pathProvider = serviceProvider.GetService<IPathProvider>();
            var mappingProfile = new HelperMappingProfile2(pathProvider).MappingProfile;

            services.AddAutoMapper(x => x.AddProfile(mappingProfile), Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}

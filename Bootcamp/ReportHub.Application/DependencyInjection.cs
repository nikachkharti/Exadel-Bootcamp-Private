using Microsoft.Extensions.DependencyInjection;
using ReportHub.Application.Behaviors;
using ReportHub.Application.Exceptions.Handler;
using System.Reflection;

namespace ReportHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Assembly assembly)
        {
            //Add MediatR, CQRS with behaviors
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(assembly);
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
                options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });


            //Add custom exception handler service
            services.AddExceptionHandler<CustomExceptionHandler>();


            return services;
        }
    }
}

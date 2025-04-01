using Microsoft.Extensions.DependencyInjection;
using ReportHub.Application.Behaviors;
using ReportHub.Application.Exceptions.Handler;

namespace ReportHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            //Add MediatR, CQRS with behaviors
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
                options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });


            //Add custom exception handler service
            //services.AddExceptionHandler<CustomExceptionHandler>();


            return services;
        }
    }
}

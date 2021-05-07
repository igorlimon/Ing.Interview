using Ing.Interview.Application.Common.Interfaces;
using Ing.Interview.Infrastructure.Persistence;
using Ing.Interview.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ing.Interview.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddDbContext<ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();

            services.AddPersistenceHealthChecks();

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure.Repositories;

namespace SimpleCQRS.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}

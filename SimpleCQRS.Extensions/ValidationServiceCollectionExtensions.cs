using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimpleCQRS.Application.Commands;
using SimpleCQRS.Application.Commands.Validators;

namespace SimpleCQRS.Extensions
{
    public static class ValidationServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreatePostCommand>, CreatePostCommandValidator>();
            services.AddTransient<IValidator<UpdatePostCommand>, UpdatePostCommandValidator>();
            return services;
        }
    }
}

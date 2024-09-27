using Microsoft.Extensions.DependencyInjection;
using SimpleCQRS.Application.Commands.Handlers;
using SimpleCQRS.Application.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Extensions
{
    public static class CommandHandlerServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            //services.AddScoped<CreatePostCommandHandler>();
            services.AddScoped<DelletePostCommandHandler>();
            services.AddScoped<UpdatePostCommandHandler>();
            services.AddScoped<GetPostByIdQuerieHandler>();
            services.AddScoped<GetPostsQuerieHandler>();
            return services;
        }
    }
}

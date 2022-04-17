using Chattoo.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Services;

namespace Chattoo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddScoped<DateIntervalService>();
            services.AddTransient<EventSuggestionService>();
            services.AddScoped<ChannelManager>();
            services.AddScoped<MessageManager>();
            services.AddScoped<GroupManager>();
            services.AddScoped<UserManager>();
            services.AddScoped<CalendarEventManager>();
            services.AddScoped<CalendarEventWishManager>();

            return services;
        }

    }
}

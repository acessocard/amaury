using System;
using Amaury.MediatR.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Amaury.MediatR
{
    public static class NotifiableCelebrityEventsBusExtensions
    {
        public static void AddCelebrityEventsBus(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddTransient<INotifiableCelebrityEventsBus, NotifiableCelebrityEventsBus>();
        }

        public static void AddCelebrityEventsBus(this IServiceCollection services, params Type[] types)
        {
            services.AddMediatR(types);
            services.AddTransient<INotifiableCelebrityEventsBus, NotifiableCelebrityEventsBus>();
        }
    }
}
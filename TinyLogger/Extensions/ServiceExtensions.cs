using System;
using System.Collections.Generic;
using System.Text;
using TinyLogger.Interfaces;
using TinyLogger.LogChannels;
using Microsoft.Extensions.DependencyInjection;

namespace TinyLogger.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterTinyLogger(this IServiceCollection services, params Type[] channels)
        {
            if(channels.Length == 0)
            {
                services.AddScoped<ITinyLoggerChannel, ColoredConsoleLogChannel>();
            }
            else
            {
                foreach(var channel in channels)
                {
                    services.AddScoped(typeof(ITinyLoggerChannel), channel);
                }
            }
            services.AddScoped<ITinyLogger, TinyLogger>();

            return services;
        }
    }
}

using Dashboard.Application.Interfaces;
using Dashboard.Application.Services;
using Dashboard.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;


namespace Dashboard.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // automapper
            services.AddAutoMapper(typeof(DependencyInjectionExtensions));

            // Services
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddScoped<IStatsService, StatsService>();


        }
    }
}

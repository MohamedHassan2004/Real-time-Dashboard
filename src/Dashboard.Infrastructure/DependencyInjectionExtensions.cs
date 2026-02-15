using Dashboard.Domain.Interfaces;
using Dashboard.Infrastructure.Data;
using Dashboard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Models;
using Sieve.Services;

namespace Dashboard.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Sieve
            services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();

            // Repositories
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IQueueRepository, QueueRepository>();
            services.AddScoped<ICallRepository, CallRepository>();

        }
    }
}


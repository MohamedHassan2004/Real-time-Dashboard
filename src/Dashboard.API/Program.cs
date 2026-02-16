using AutoMapper;
using Dashboard.API.Hubs;
using Dashboard.API.Middlewares;
using Dashboard.API.Services;
using Dashboard.Application;
using Dashboard.Application.Interfaces;
using Dashboard.Infrastructure;
using Dashboard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;


namespace Dashboard.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            // Sieve
            builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("Sieve"));

            builder.Services.AddControllers();

            // SignalR
            builder.Services.AddSignalR();
            builder.Services.AddScoped<IDashboardNotifier, Dashboard.API.SignalR.SignalRDashboardNotifier>();

            // CORS (for frontend clients)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDashboard", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Background broadcast service
            builder.Services.AddHostedService<DashboardBroadcastService>();

            // Swagger
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Auto-apply migrations + seed data
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseCors("AllowDashboard");

            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<DashboardHub>("/hubs/dashboard");

            app.Run();
        }
    }
}


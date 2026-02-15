using AutoMapper;
using Dashboard.API.Middlewares;
using Dashboard.Application;
using Dashboard.Infrastructure;
using Dashboard.Infrastructure.Data;
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
            builder.Services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();

            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobelExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

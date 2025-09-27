using LibraryProject.Api.Services;
using LibraryProject.DataAccess.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api;

public partial class Program
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        // Read Neon connection string (can be overridden in tests)
        var connStr = Environment.GetEnvironmentVariable("CONN_STR")
                      ?? config.GetConnectionString("Default");
        if (string.IsNullOrEmpty(connStr))
        {
            throw new InvalidOperationException("Database connection string not found. Did you set CONN_STR?");
        }

        // Add Cors
        services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                if (env is not null && env.IsDevelopment())
                {
                    // Dev: allow everything
                    policy.AllowAnyOrigin();
                }
                else
                {
                    // Prod: restrict to your client Fly.io app
                    policy.WithOrigins(
                        "https://library-client.fly.dev",
                        "http://localhost:5173"
                    );
                }
            });
        });

        // DbContext
        services.AddDbContext<LibraryDbContext>(options =>
            options.UseNpgsql(connStr));

        // Controllers + Swagger
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Services
        services.AddScoped<GenreService>();
        services.AddScoped<AuthorService>();
        services.AddScoped<BookService>();
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        ConfigureServices(services, config);
    }
}



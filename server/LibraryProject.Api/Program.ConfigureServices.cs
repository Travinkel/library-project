using LibraryProject.Api.Services;
using LibraryProject.DataAccess.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api;

public partial class Program
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration config, IWebHostEnvironment? env = null)
    {
        var connStr = Environment.GetEnvironmentVariable("CONN_STR")
                      ?? config.GetConnectionString("Default");
        if (string.IsNullOrEmpty(connStr))
        {
            throw new InvalidOperationException("Database connection string not found. Did you set CONN_STR?");
        }

        services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader();

                if (env is not null && env.IsDevelopment())
                {
                    policy.AllowAnyOrigin();
                }
                else
                {
                    policy.WithOrigins(
                        "https://library-client.fly.dev",
                        "http://localhost:5173"
                    );
                }
            });
        });

        services.AddDbContext<LibraryDbContext>(options => options.UseNpgsql(connStr));
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<GenreService>();
        services.AddScoped<AuthorService>();
        services.AddScoped<BookService>();
    }
}



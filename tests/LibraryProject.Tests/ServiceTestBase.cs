namespace LibraryProject.Tests;

using System.Collections.Generic;
using System.IO;
using LibraryProject.Api;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using LibraryProject.DataAccess.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Npgsql;
using Testcontainers.PostgreSql;

public abstract class ServiceTestBase<TService> : IAsyncLifetime where TService : class
{
    protected ServiceProvider? Provider;
    protected LibraryDbContext Db = null!;
    protected TService Svc = null!;
    private PostgreSqlContainer? _pg;

    public async Task InitializeAsync()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        try
        {
            _pg = new PostgreSqlBuilder()
                .WithImage("postgres:16-alpine")
                .WithDatabase("library")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithCleanUp(true)
                .Build();

            await _pg.StartAsync();
            await SeedSchemaAsync(_pg);

            var cfg = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:Default"] = _pg.GetConnectionString(),
                })
                .Build();

            var env = new TestHostEnvironment("Testing");
            ServiceConfiguration.ConfigureServices(services, cfg, env);
        }
        catch
        {
            services.AddDbContext<LibraryDbContext>(o => o.UseInMemoryDatabase("library-tests"));
            services.AddScoped<AuthorService>();
            services.AddScoped<BookService>();
            services.AddScoped<GenreService>();
        }

        Provider = services.BuildServiceProvider();
        Db = Provider.GetRequiredService<LibraryDbContext>();
        Svc = Provider.GetRequiredService<TService>();

        await Db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        if (Provider is not null) await Provider.DisposeAsync();
        if (_pg is not null) await _pg.DisposeAsync();
    }

    private static async Task SeedSchemaAsync(PostgreSqlContainer container)
    {
        var sql = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "sql", "schema.sql"));
        await using var conn = new NpgsqlConnection(container.GetConnectionString());
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync();
    }

    private sealed class TestHostEnvironment : IWebHostEnvironment
    {
        public TestHostEnvironment(string environmentName)
        {
            EnvironmentName = environmentName;
            ApplicationName = typeof(ServiceConfiguration).Assembly.GetName().Name!;
            ContentRootPath = AppContext.BaseDirectory;
            WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot");
            ContentRootFileProvider = new NullFileProvider();
            WebRootFileProvider = new NullFileProvider();
        }

        public string ApplicationName { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string WebRootPath { get; set; }
    }
}

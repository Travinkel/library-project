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

public class AuthorServiceTests : IAsyncLifetime
{
    private ServiceProvider? _provider;
    private LibraryDbContext _db = null!;
    private IAuthorService _svc = null!;
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
                    ["App:SomeRequiredSetting"] = "ok-in-tests"
                })
                .Build();

            var env = new TestHostEnvironment("Testing");
            ServiceConfiguration.ConfigureServices(services, cfg, env);
        }
        catch
        {
            services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("library-tests"));
            services.AddScoped<GenreService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<AuthorService>();
            services.AddScoped<BookService>();
        }

        _provider = services.BuildServiceProvider();
        _db = _provider.GetRequiredService<LibraryDbContext>();
        _svc = _provider.GetRequiredService<IAuthorService>();

        await _db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        if (_provider is not null)
        {
            await _provider.DisposeAsync();
        }

        if (_pg is not null)
        {
            await _pg.DisposeAsync();
        }
    }

    [Fact]
    public async Task CreateAuthor_WritesRow()
    {
        var id = await _svc.CreateAsync(new CreateAuthorDto("Ursula K. Le Guin"));

        var found = await _db.Authors.AsNoTracking().SingleAsync(a => a.Id == id);
        Assert.Equal("Ursula K. Le Guin", found.Name);
        Assert.True(found.CreatedAt.HasValue);
    }

    private static async Task SeedSchemaAsync(PostgreSqlContainer container)
    {
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "sql", "schema.sql");
        var sql = await File.ReadAllTextAsync(scriptPath);

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

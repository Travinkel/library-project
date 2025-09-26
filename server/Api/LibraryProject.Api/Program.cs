using LibraryProject.Api;
using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json and environment variables
builder.Configuration.AddEnvironmentVariables();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Bind AppOptions (from configuration if needed later)
builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection(AppOptions.SectionName));

// Read Neon connection string from environment (.env)
var connStr = Environment.GetEnvironmentVariable("CONN_STR");

if (string.IsNullOrEmpty(connStr))
{
    throw new InvalidOperationException("Database connection string not found. Did you set CONN_STR?");
}

// Register DbContext
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseNpgsql(connStr));

// Add controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();

var app = builder.Build();

// Must use CORS before endpoints/Swagger - indeed - After builder.Build(); truly also..
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
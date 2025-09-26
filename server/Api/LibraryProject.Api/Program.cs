using LibraryProject.Api;
using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;;

var builder = WebApplication.CreateBuilder(args);

// Bind AppOptions (from configuration if needed later)
builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection(AppOptions.SectionName));

// Read Neon connection string from environment (.env)
var connStr = Environment.GetEnvironmentVariable("CONN_STR");

// Register DbContext
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseNpgsql(connStr));

// Add controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

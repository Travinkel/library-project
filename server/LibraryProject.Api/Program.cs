using LibraryProject.Api;
using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json and environment variables
builder.Configuration.AddEnvironmentVariables();

// Call the extracted method from partial class for testability
LibraryProject.Api.ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Do NOT use HTTPS redirection on Fly.
    // Fly handles TLS termination at the edge.
    // When your code tries to redirect, the browser
    // is sent to an HTTPS endpoint that doesn’t exist
    // → ERR_CONNECTION_CLOSED.
    // app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

// Redirect root to Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
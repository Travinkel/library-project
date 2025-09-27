using LibraryProject.Api;
using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json and environment variables
builder.Configuration.AddEnvironmentVariables();

// Call the extracted method from partial class for testability
ServiceConfiguration.ConfigureServices(
    builder.Services,
    builder.Configuration,
    builder.Environment
);
var app = builder.Build();

// Allow CORS Globally
app.UseCors("Default");

// Always enable Swagger, regardless of environment
// Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryProject.Api v1");
    // Default RoutePrefix = "swagger" ensures UI is at /swagger
    // Do NOT set RoutePrefix = string.Empty
});


// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
// else
// {
//     // Do NOT use HTTPS redirection on Fly.
//     // Fly handles TLS termination at the edge.
//     // When your code tries to redirect, the browser
//     // is sent to an HTTPS endpoint that doesn’t exist
//     // → ERR_CONNECTION_CLOSED.
//     // app.UseHttpsRedirection();
// }



app.UseAuthorization();
app.MapControllers();

// Healthcheck
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// Redirect root → /swagger
app.MapGet("/", () => Results.Redirect("/swagger"));


app.Run();
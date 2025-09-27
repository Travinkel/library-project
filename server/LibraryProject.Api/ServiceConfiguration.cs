using Microsoft.AspNetCore.Hosting;

namespace LibraryProject.Api;

public static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        => Program.ConfigureServices(services, config);

    public static void ConfigureServices(IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        => Program.ConfigureServices(services, config, env);
}

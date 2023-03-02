using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Training.MultiTenancy.Data.Tools;

public class ToolsApplication
{
    internal ToolsApplication(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        ServiceProvider = serviceProvider;
        Configuration = configuration;
    }

    public IServiceProvider ServiceProvider { get; }
    public IConfiguration Configuration { get; }

    public static ToolsApplicationBuilder CreateBuilder() => new();
    public static ToolsApplicationBuilder CreateDefaultBuilder()
    {
        var builder = CreateBuilder();
        builder.Configuration.AddJsonFile("appsettingsTools.json");
        builder.Services.AddLogging();
        return builder;
    }
}

public class ToolsApplicationBuilder
{
    public IServiceCollection Services { get; } = new ServiceCollection();
    public ConfigurationManager Configuration { get; } = new();
    public ILoggerFactory LoggerFactory { get; }

    public ToolsApplication Build() => new(Services.BuildServiceProvider(), Configuration);
}

using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Test.Configuration;

[ExcludeFromCodeCoverage]
public static class ConfigurationManager
{
    public static IConfiguration Get(string environmentName)
    {
        var basePath = Directory.GetCurrentDirectory();

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}

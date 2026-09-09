using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Ttu.FiberImgApp.Core.Abstractions;
using Ttu.FiberImgApp.Core.Configuration;
using Ttu.FiberImgApp.Infrastructure.Data;

namespace Ttu.FiberImgApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ZenOptions>(configuration.GetSection(ZenOptions.SectionName));
        services.Configure<ThorlabsOptions>(configuration.GetSection(ThorlabsOptions.SectionName));
        services.Configure<UpdatesOptions>(configuration.GetSection(UpdatesOptions.SectionName));
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));
        services.Configure<LoggingOptions>(configuration.GetSection(LoggingOptions.SectionName));

        var databaseOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>()
            ?? new DatabaseOptions();
        var dbPath = ResolveDatabasePath(databaseOptions.Path);

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }

    public static void ConfigureSerilog(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var logDir = Path.Combine(GetAppDataDirectory(), "logs");
        Directory.CreateDirectory(logDir);

        var minimumLevel = configuration.GetSection(LoggingOptions.SectionName)["MinimumLevel"]
            ?? "Information";

        loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Is(ParseLevel(minimumLevel))
            .Enrich.FromLogContext()
            .WriteTo.File(
                Path.Combine(logDir, "fiberimg-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14);
    }

    public static string GetAppDataDirectory()
    {
        var dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TTU",
            "FiberImgApp");
        Directory.CreateDirectory(dir);
        return dir;
    }

    private static string ResolveDatabasePath(string configuredPath)
    {
        if (!string.IsNullOrWhiteSpace(configuredPath))
        {
            var directory = Path.GetDirectoryName(configuredPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return configuredPath;
        }

        return Path.Combine(GetAppDataDirectory(), "fiberimg.db");
    }

    private static LogEventLevel ParseLevel(string level) =>
        Enum.TryParse<LogEventLevel>(level, ignoreCase: true, out var parsed)
            ? parsed
            : LogEventLevel.Information;
}

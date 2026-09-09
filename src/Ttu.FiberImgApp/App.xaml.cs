using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Serilog;
using Ttu.FiberImgApp.Core.Abstractions;
using Ttu.FiberImgApp.Infrastructure;
using Ttu.FiberImgApp.Integrations.Thorlabs;
using Ttu.FiberImgApp.Integrations.Zen;
using Ttu.FiberImgApp.ViewModels;

namespace Ttu.FiberImgApp;

public partial class App : Application
{
    private Window? _window;
    private IHost? _host;

    public App()
    {
        InitializeComponent();
        UnhandledException += OnUnhandledException;
    }

    public static IServiceProvider Services { get; private set; } = null!;

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        _host = BuildHost();
        Services = _host.Services;

        await using (var scope = Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
            await db.EnsureCreatedAsync();
        }

        _window = new MainWindow();
        _window.Activate();
    }

    private static IHost BuildHost()
    {
        var appData = DependencyInjection.GetAppDataDirectory();
        var localOverride = Path.Combine(appData, "appsettings.Local.json");

        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
                config.AddJsonFile(localOverride, optional: true, reloadOnChange: true);
            })
            .UseSerilog((context, _, loggerConfiguration) =>
                DependencyInjection.ConfigureSerilog(loggerConfiguration, context.Configuration))
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructure(context.Configuration);
                services.AddZenIntegration();
                services.AddThorlabsIntegration();
                services.AddTransient<MainViewModel>();
            })
            .Build();
    }

    private static void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        Log.Fatal(e.Exception, "Unhandled exception");
        Log.CloseAndFlush();
    }
}

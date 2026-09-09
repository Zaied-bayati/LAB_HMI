using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ttu.FiberImgApp.Core.Abstractions;
using Ttu.FiberImgApp.Core.Configuration;

namespace Ttu.FiberImgApp.Integrations.Thorlabs;

public sealed class NotConfiguredThorlabsClient : IThorlabsClient
{
    private readonly ThorlabsOptions _options;

    public NotConfiguredThorlabsClient(IOptions<ThorlabsOptions> options)
    {
        _options = options.Value;
    }

    public string Status =>
        string.IsNullOrWhiteSpace(_options.DeviceId)
            ? "NotConfigured — set Thorlabs:DeviceId and replace stub with XA/.NET SDK"
            : $"NotConfigured — Thorlabs stub for device '{_options.DeviceId}' (SDK not wired yet)";

    public Task<bool> ConnectAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(false);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}

public static class ThorlabsServiceCollectionExtensions
{
    public static IServiceCollection AddThorlabsIntegration(this IServiceCollection services)
    {
        services.AddSingleton<IThorlabsClient, NotConfiguredThorlabsClient>();
        return services;
    }
}

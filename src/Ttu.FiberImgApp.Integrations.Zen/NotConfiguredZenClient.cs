using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ttu.FiberImgApp.Core.Abstractions;
using Ttu.FiberImgApp.Core.Configuration;

namespace Ttu.FiberImgApp.Integrations.Zen;

public sealed class NotConfiguredZenClient : IZenClient
{
    private readonly ZenOptions _options;

    public NotConfiguredZenClient(IOptions<ZenOptions> options)
    {
        _options = options.Value;
    }

    public string Status =>
        string.IsNullOrWhiteSpace(_options.ApiToken) && string.IsNullOrWhiteSpace(_options.ApiTokenFilePath)
            ? "NotConfigured — provide Zen:ApiToken (or ApiTokenFilePath) and implement ZEN API gRPC client"
            : $"NotConfigured — ZEN stub ready for {_options.Host}:{_options.Port} (gRPC client not implemented yet)";

    public Task<bool> ConnectAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(false);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}

public static class ZenServiceCollectionExtensions
{
    public static IServiceCollection AddZenIntegration(this IServiceCollection services)
    {
        services.AddSingleton<IZenClient, NotConfiguredZenClient>();
        return services;
    }
}

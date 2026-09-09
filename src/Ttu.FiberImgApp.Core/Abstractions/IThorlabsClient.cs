namespace Ttu.FiberImgApp.Core.Abstractions;

public interface IThorlabsClient
{
    string Status { get; }
    Task<bool> ConnectAsync(CancellationToken cancellationToken = default);
    Task DisconnectAsync(CancellationToken cancellationToken = default);
}

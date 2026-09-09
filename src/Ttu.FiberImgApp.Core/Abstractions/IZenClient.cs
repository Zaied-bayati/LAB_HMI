namespace Ttu.FiberImgApp.Core.Abstractions;

public interface IZenClient
{
    string Status { get; }
    Task<bool> ConnectAsync(CancellationToken cancellationToken = default);
    Task DisconnectAsync(CancellationToken cancellationToken = default);
}

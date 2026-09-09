namespace Ttu.FiberImgApp.Core.Entities;

public class LogEntry
{
    public long Id { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    public string Level { get; set; } = "Information";
    public string Category { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? DetailsJson { get; set; }
    public Guid? SessionId { get; set; }
}

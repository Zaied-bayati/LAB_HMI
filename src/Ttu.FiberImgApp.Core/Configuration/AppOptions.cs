namespace Ttu.FiberImgApp.Core.Configuration;

public sealed class ZenOptions
{
    public const string SectionName = "Zen";

    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 50051;
    public string ApiToken { get; set; } = string.Empty;
    public string? ApiTokenFilePath { get; set; }
}

public sealed class ThorlabsOptions
{
    public const string SectionName = "Thorlabs";

    public string DeviceId { get; set; } = string.Empty;
}

public sealed class UpdatesOptions
{
    public const string SectionName = "Updates";

    public string AppInstallerUri { get; set; } =
        "https://github.com/Zaied-bayati/ttu-fiber-img-app/releases/latest/download/";
}

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    /// <summary>
    /// Absolute path or empty to use %LocalAppData%\TTU\FiberImgApp\fiberimg.db
    /// </summary>
    public string Path { get; set; } = string.Empty;
}

public sealed class LoggingOptions
{
    public const string SectionName = "Logging";

    public string MinimumLevel { get; set; } = "Information";
}

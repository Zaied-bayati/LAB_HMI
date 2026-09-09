namespace Ttu.FiberImgApp.Core.Entities;

public class Calibration
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Instrument { get; set; } = string.Empty;
    public string ParametersJson { get; set; } = "{}";
    public DateTimeOffset CalibratedAt { get; set; } = DateTimeOffset.UtcNow;
    public bool IsActive { get; set; }
}

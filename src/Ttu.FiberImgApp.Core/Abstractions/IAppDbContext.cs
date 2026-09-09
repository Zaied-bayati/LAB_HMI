using Ttu.FiberImgApp.Core.Entities;

namespace Ttu.FiberImgApp.Core.Abstractions;

public interface IAppDbContext
{
    IQueryable<Session> Sessions { get; }
    IQueryable<Recipe> Recipes { get; }
    IQueryable<LogEntry> LogEntries { get; }
    IQueryable<Calibration> Calibrations { get; }
    IQueryable<UserPreference> UserPreferences { get; }

    Task EnsureCreatedAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

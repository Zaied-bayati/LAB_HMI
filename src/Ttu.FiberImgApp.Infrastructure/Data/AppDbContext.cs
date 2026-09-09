using Microsoft.EntityFrameworkCore;
using Ttu.FiberImgApp.Core.Abstractions;
using Ttu.FiberImgApp.Core.Entities;

namespace Ttu.FiberImgApp.Infrastructure.Data;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<LogEntry> LogEntries => Set<LogEntry>();
    public DbSet<Calibration> Calibrations => Set<Calibration>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();

    IQueryable<Session> IAppDbContext.Sessions => Sessions;
    IQueryable<Recipe> IAppDbContext.Recipes => Recipes;
    IQueryable<LogEntry> IAppDbContext.LogEntries => LogEntries;
    IQueryable<Calibration> IAppDbContext.Calibrations => Calibrations;
    IQueryable<UserPreference> IAppDbContext.UserPreferences => UserPreferences;

    public Task EnsureCreatedAsync(CancellationToken cancellationToken = default) =>
        Database.EnsureCreatedAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(256).IsRequired();
        });

        modelBuilder.Entity<Recipe>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(256).IsRequired();
            e.HasIndex(x => x.Name);
        });

        modelBuilder.Entity<LogEntry>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Level).HasMaxLength(32);
            e.Property(x => x.Category).HasMaxLength(128);
            e.HasIndex(x => x.Timestamp);
        });

        modelBuilder.Entity<Calibration>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(256).IsRequired();
            e.Property(x => x.Instrument).HasMaxLength(128);
        });

        modelBuilder.Entity<UserPreference>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Key).HasMaxLength(128).IsRequired();
            e.HasIndex(x => x.Key).IsUnique();
        });
    }
}

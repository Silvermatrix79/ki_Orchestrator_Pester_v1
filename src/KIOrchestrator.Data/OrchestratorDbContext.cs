using KIOrchestrator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KIOrchestrator.Data;

public class OrchestratorDbContext : DbContext
{
    public OrchestratorDbContext(DbContextOptions<OrchestratorDbContext> options) : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<PlanVersion> PlanVersions => Set<PlanVersion>();
    public DbSet<PlanItem> PlanItems => Set<PlanItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(200).IsRequired();
            e.Property(p => p.WorkingDirectory).HasMaxLength(500).IsRequired();
            e.HasIndex(p => p.Name).IsUnique();
        });

        modelBuilder.Entity<ChatMessage>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.Role).HasMaxLength(20).IsRequired();
            e.Property(m => m.Content).IsRequired();
            e.HasOne(m => m.Project).WithMany(p => p.ChatMessages).HasForeignKey(m => m.ProjectId);
            e.HasIndex(m => m.ProjectId);
        });

        modelBuilder.Entity<PlanVersion>(e =>
        {
            e.HasKey(v => v.Id);
            e.HasOne(v => v.Project).WithMany(p => p.PlanVersions).HasForeignKey(v => v.ProjectId);
            e.HasIndex(v => new { v.ProjectId, v.VersionNumber }).IsUnique();
        });

        modelBuilder.Entity<PlanItem>(e =>
        {
            e.HasKey(i => i.Id);
            e.Property(i => i.Title).HasMaxLength(500).IsRequired();
            e.HasOne(i => i.PlanVersion).WithMany(v => v.Items).HasForeignKey(i => i.PlanVersionId);
            e.HasOne(i => i.Parent).WithMany(i => i.Children).HasForeignKey(i => i.ParentId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(i => i.PlanVersionId);
        });
    }
}

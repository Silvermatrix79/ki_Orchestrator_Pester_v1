namespace KIOrchestrator.Core.Models;

public class PlanVersion
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int VersionNumber { get; set; }
    public string? ChangeDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;
    public List<PlanItem> Items { get; set; } = [];
}

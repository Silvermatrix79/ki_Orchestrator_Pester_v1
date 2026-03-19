using KIOrchestrator.Core.Enums;

namespace KIOrchestrator.Core.Models;

public class PlanItem
{
    public int Id { get; set; }
    public int PlanVersionId { get; set; }
    public int? ParentId { get; set; }
    public PlanItemType ItemType { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public string DetailSteps { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public KanbanStatus Status { get; set; } = KanbanStatus.Backlog;
    public WorkTag WorkTag { get; set; } = WorkTag.None;
    public string? WorkTagDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public PlanVersion PlanVersion { get; set; } = null!;
    public PlanItem? Parent { get; set; }
    public List<PlanItem> Children { get; set; } = [];
}

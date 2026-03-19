namespace KIOrchestrator.Core.Models;

public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public required string WorkingDirectory { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<ChatMessage> ChatMessages { get; set; } = [];
    public List<PlanVersion> PlanVersions { get; set; } = [];
}

namespace KIOrchestrator.Core.Models;

public class ChatMessage
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public required string Role { get; set; } // "user" or "assistant"
    public required string Content { get; set; }
    public string? ConversationId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;
}

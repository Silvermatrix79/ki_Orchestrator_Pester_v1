namespace KIOrchestrator.Core.Enums;

public enum KanbanStatus
{
    Backlog,
    Work,
    Review,
    Done
}

public enum WorkTag
{
    None,
    Changes,
    Patching,
    Bug
}

public enum PlanItemType
{
    Phase,
    Point,
    SubPoint
}

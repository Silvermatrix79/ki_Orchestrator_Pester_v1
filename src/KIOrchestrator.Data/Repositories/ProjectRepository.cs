using KIOrchestrator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KIOrchestrator.Data.Repositories;

public class ProjectRepository
{
    private readonly OrchestratorDbContext _db;

    public ProjectRepository(OrchestratorDbContext db)
    {
        _db = db;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _db.Projects
            .OrderByDescending(p => p.UpdatedAt)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _db.Projects.FindAsync(id);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        project.CreatedAt = DateTime.UtcNow;
        project.UpdatedAt = DateTime.UtcNow;
        _db.Projects.Add(project);
        await _db.SaveChangesAsync();
        return project;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        project.UpdatedAt = DateTime.UtcNow;
        _db.Projects.Update(project);
        await _db.SaveChangesAsync();
        return project;
    }

    public async Task DeleteAsync(int id)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project != null)
        {
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _db.Projects.AnyAsync(p => p.Name == name);
    }
}

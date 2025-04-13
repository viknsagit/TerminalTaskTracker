using Microsoft.EntityFrameworkCore;
using TerminalTaskTracker.Models;
using Task = System.Threading.Tasks.Task;

namespace TerminalTaskTracker.Repository;

public class TaskService (TaskRepository repository)
{
    public async Task AddNewTaskAsync(Models.Task task)
    {
        await repository.Tasks.AddAsync(task);
        await repository.SaveChangesAsync();
    }

    public async Task<bool> ProjectWithNameExistAsync(string projectName)
    {
        var project = await repository.Projects
            .Where(p => p.ProjectName == projectName)
            .FirstOrDefaultAsync();
        return project != null;
    }

    public async Task AddNewProjectAsync(Project project)
    {
        await repository.Projects.AddAsync(project);
        await repository.SaveChangesAsync();
    }

    public async Task<Models.Task?> GetTaskByIdAsync(int id)
    {
        return await repository.Tasks.FindAsync(id);
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await repository.Projects.FindAsync(id);
    }

    public async Task<int?> GetProjectIdAsync(string projectName)
    {
        var project = await repository.Projects.
            Where(p => p.ProjectName == projectName)
            .FirstOrDefaultAsync();
        return project?.ProjectId;
    }
}
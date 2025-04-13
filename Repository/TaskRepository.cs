using Microsoft.EntityFrameworkCore;
using TerminalTaskTracker.Models;
using Task = TerminalTaskTracker.Models.Task;

namespace TerminalTaskTracker.Repository;

public class TaskRepository : DbContext
{
    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<Project> Projects => Set<Project>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./tasktracker.db");
    }
}
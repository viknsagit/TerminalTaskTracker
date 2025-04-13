using Spectre.Console;
using TerminalTaskTracker.Models;
using TerminalTaskTracker.Repository;
using Task = TerminalTaskTracker.Models.Task;

TaskService taskService = new(new TaskRepository());

AnsiConsole.Write(
    new FigletText("Terminal Task Tracker")
        .Centered()
        .Color(Color.Red));
AnsiConsole.MarkupLine("[bold green]Welcome to the Terminal Task Tracker![/]");

var command = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What would you like to do?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more commands)[/]")
        .AddChoices(new[] {
            "Add Task", "Add Project", "View Projects", "Delete Project",
            "Settings", "Exit"
        }));

switch (command)
{
    case "Add Task":
    {
        var taskName = AnsiConsole.Ask<string>("Enter the task name:");
        var taskDescription = AnsiConsole.Ask<string>("Enter the task description:",string.Empty);
        var projectId = AnsiConsole.Prompt(new TextPrompt<int>("Enter the project id:").Validate(id =>
        {
            var project = taskService.GetProjectByIdAsync(id).Result;
            return project is null ? ValidationResult.Error("[bold red]Project not found![/]") : ValidationResult.Success();
        }));
        var project = await taskService.GetProjectByIdAsync(projectId);
        await taskService.AddNewTaskAsync(new Task()
        {
            Project = project,
            Title = taskName,
            Description = taskDescription,
        });
        AnsiConsole.MarkupLine("[bold green]Task added to project list![/]");
    }
        break;

    case "Add Project":
    {
        var projectName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the project name:")
            .Validate(name =>
            {
                var nameExits = taskService.ProjectWithNameExistAsync(name).Result;
                return nameExits ? ValidationResult.Error("[bold red]Project with this name already exists![/]") : ValidationResult.Success();
            }));
        
        var projectDescription = AnsiConsole.Ask("Enter the project description:",string.Empty);
        await taskService.AddNewProjectAsync(new Project()
        {
            ProjectName = projectName,
            ProjectDescription = projectDescription
        });
        var projectId = await taskService.GetProjectIdAsync(projectName);
        AnsiConsole.MarkupLine($"[bold green]Project added successfully with ID: {projectId}[/]");
    }
        break;

    case "View Projects":
    {
        var projects = await taskService.GetAllProjectsAsync();
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Expand();
        table.Title("[bold yellow]Projects list[/]");
        
        table.AddColumn(new TableColumn("[blue]ID[/]").Centered());
        table.AddColumn(new TableColumn("[green]Project name[/]"));
        table.AddColumn(new TableColumn("[purple]Description[/]"));
        table.AddColumn(new TableColumn("[red]Tasks count[/]").Centered());
        
        foreach (var project in projects)
        {
            var tasksCount = await taskService.GetTasksCountByProjectIdAsync(project.ProjectId);
            table.AddRow(
                $"[blue]{project.ProjectId}[/]", 
                $"[green]{project.ProjectName}[/]", 
                project.ProjectDescription?.Length > 0 ? project.ProjectDescription : "[grey](empty)[/]", 
                tasksCount > 0 ? $"[red]{tasksCount}[/]" : "[grey]0[/]"
            );
        }
        AnsiConsole.Write(table);
    }
        break;
    
    case "Exit": 
        Environment.Exit(0);
        break;
    
}





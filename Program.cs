using Spectre.Console;
using TerminalTaskTracker.Repository;
TaskService taskService = new(new TaskRepository());

AnsiConsole.Write(
    new FigletText("Terminal Task Tracker")
        .LeftJustified()
        .Color(Color.Red));
AnsiConsole.MarkupLine("[bold green]Welcome to the Terminal Task Tracker![/]");

var command = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What would you like to do?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more commands)[/]")
        .AddChoices(new[] {
            "Add Task", "View Task", "Delete Task","Move Task",
            "Add Project", "View Project", "Delete Project",
            "Settings", "Exit"
        }));

switch (command)
{
    case "Add Task":
    {
        var taskName = AnsiConsole.Ask<string>("Enter the task name:");
        var taskDescription = AnsiConsole.Ask<string>("Enter the task description (Can be null):");
        var projectId = AnsiConsole.Prompt(new TextPrompt<int>("Enter the project id:").Validate(id =>
        {
            var project = taskService.GetProjectByIdAsync(id).Result;
            return project is null ? ValidationResult.Error("Project not found") : ValidationResult.Success();
        }));
        var project = await taskService.GetProjectByIdAsync(projectId);
    }
    break;
}





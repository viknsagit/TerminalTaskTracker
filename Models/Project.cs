using System.ComponentModel.DataAnnotations.Schema;

namespace TerminalTaskTracker.Models;

public class Project
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
}
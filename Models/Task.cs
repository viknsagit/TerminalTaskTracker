using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerminalTaskTracker.Models;

public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskId { get; set; }
    
    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool IsCompleted { get; set; }
    
    [Required]
    public long TaskCreateTime { get; private set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    
    public List<TaskTimePart> TaskTimeParts { get; set; } = [];
}
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerminalTaskTracker.Models;

public class TaskTimePart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public long StartTime { get; private set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    public long EndTime { get; set; }

    public bool IsRunning => EndTime is 0;
}
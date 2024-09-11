using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.DAL.Entities;

public class TaskEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    public string Description { get; set; }
    public DateTime? DueDate { get; set; }

    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public UserEntity User { get; set; }
}

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed
}

public enum TaskPriority
{
    Low,
    Medium,
    High
}

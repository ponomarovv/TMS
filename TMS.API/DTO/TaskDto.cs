using System;
using System.ComponentModel.DataAnnotations;
using TMS.BLL.Models;
using TaskStatus = TMS.BLL.Models.TaskStatus;

namespace TMS.API.DTO;

public class CreateTaskDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
}

public class UpdateTaskDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
}

public class TaskFilterDto
{
    public TaskStatus? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskPriority? Priority { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
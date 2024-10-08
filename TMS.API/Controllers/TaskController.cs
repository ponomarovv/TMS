﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMS.API.DTO;
using TMS.BLL.Contracts;
using TMS.BLL.Models;

namespace TMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;

    public TaskController(ITaskService taskService, ILogger<TaskController> logger)
    {
        _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto model)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for task creation.");
            return BadRequest(ModelState);
        }

        // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = "f2ef75c6-d373-4a81-bc71-5c0a58d1e99a";
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in claims.");
            return Unauthorized();
        }

        var task = new TaskModel
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description,
            DueDate = model.DueDate,
            Status = model.Status,
            Priority = model.Priority,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };

        try
        {
            await _taskService.CreateTaskAsync(task);
            _logger.LogInformation("Task created successfully for user {UserId}.", userId);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating task for user {UserId}.", userId);
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] TaskFilterDto filter)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var tasks = await _taskService.GetTasksAsync(userId, new TaskFilterModel
        {
            Status = filter.Status,
            DueDate = filter.DueDate,
            Priority = filter.Priority,
            Page = filter.Page,
            PageSize = filter.PageSize
        });
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var task = await _taskService.GetTaskAsync(id, userId);

        if (task == null)
        {
            return NotFound("Task not found.");
        }

        return Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var task = await _taskService.GetTaskAsync(id, userId);

        if (task == null)
        {
            return NotFound("Task not found.");
        }

        task.Title = model.Title;
        task.Description = model.Description;
        task.DueDate = model.DueDate;
        task.Status = model.Status;
        task.Priority = model.Priority;
        task.UpdatedAt = DateTime.UtcNow;

        await _taskService.UpdateTaskAsync(task);
        return Ok("Task updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        await _taskService.DeleteTaskAsync(id, userId);
        return Ok("Task deleted successfully.");
    }
}

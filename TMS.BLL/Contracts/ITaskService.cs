using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.BLL.Models;

namespace TMS.BLL.Contracts;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> GetTasksAsync(string userId, TaskFilterModel filter);
    Task<TaskModel> GetTaskAsync(Guid id, string userId);
    Task CreateTaskAsync(TaskModel task);
    Task UpdateTaskAsync(TaskModel task);
    Task DeleteTaskAsync(Guid id, string userId);
}
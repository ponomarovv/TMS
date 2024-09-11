using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Contracts;
using TMS.BLL.Models;
using TMS.DAL.Contracts;
using TMS.DAL.Entities;

namespace TMS.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync(string userId, TaskFilterModel filter)
        {
            var query = _unitOfWork.TaskRepository.GetAll().Where(t => t.UserId == userId).AsQueryable();

            // if (filter.Status.HasValue)
            // {
            //     query = query.Where(t => t.Status == filter.Status.Value);
            // }
            //
            // if (filter.DueDate.HasValue)
            // {
            //     query = query.Where(t => t.DueDate == filter.DueDate.Value);
            // }
            //
            // if (filter.Priority.HasValue)
            // {
            //     query = query.Where(t => t.Priority == filter.Priority.Value);
            // }

            query = query.OrderBy(t => t.DueDate).ThenBy(t => t.Priority);

            var tasks = await query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            return _mapper.Map<IEnumerable<TaskModel>>(tasks);
        }

        public async Task<TaskModel> GetTaskAsync(Guid id, string userId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (task != null && task.UserId == userId)
            {
                return _mapper.Map<TaskModel>(task);
            }
            return null;
        }

        public async Task CreateTaskAsync(TaskModel taskModel)
        {
            var task = _mapper.Map<TaskEntity>(taskModel);
            await _unitOfWork.TaskRepository.AddAsync(task);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTaskAsync(TaskModel taskModel)
        {
            var task = _mapper.Map<TaskEntity>(taskModel);
            _unitOfWork.TaskRepository.Update(task);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTaskAsync(Guid id, string userId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (task != null && task.UserId == userId)
            {
                _unitOfWork.TaskRepository.Delete(task);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}

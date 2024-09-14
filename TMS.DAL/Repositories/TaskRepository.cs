using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMS.DAL.Contracts;
using TMS.DAL.Data;
using TMS.DAL.Entities;

namespace TMS.DAL.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
            _context = context;
        }

    public IQueryable<TaskEntity> GetAll()
    {
            return _context.Tasks.AsQueryable();
        }

    public async Task<TaskEntity> GetByIdAsync(Guid id)
    {
            return await _context.Tasks.FindAsync(id);
        }

    public async Task AddAsync(TaskEntity task)
    {
            await _context.Tasks.AddAsync(task);
        }

    public void Update(TaskEntity task)
    {
            _context.Tasks.Update(task);
        }

    public void Delete(TaskEntity task)
    {
            _context.Tasks.Remove(task);
        }
}
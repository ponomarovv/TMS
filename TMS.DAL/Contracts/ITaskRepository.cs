using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DAL.Entities;

namespace TMS.DAL.Contracts
{
    public interface ITaskRepository
    {
        IQueryable<TaskEntity> GetAll();
        Task<TaskEntity> GetByIdAsync(Guid id);
        Task AddAsync(TaskEntity task);
        void Update(TaskEntity task);
        void Delete(TaskEntity task);
    }
}

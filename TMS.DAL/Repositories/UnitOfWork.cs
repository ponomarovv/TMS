using System.Threading.Tasks;
using TMS.DAL.Contracts;
using TMS.DAL.Data;

namespace TMS.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ITaskRepository TaskRepository { get; set; }

    public UnitOfWork(ApplicationDbContext context, ITaskRepository taskRepository)
    {
        _context = context;
        TaskRepository = taskRepository;
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}

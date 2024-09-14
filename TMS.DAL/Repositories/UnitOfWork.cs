using System.Threading.Tasks;
using TMS.DAL.Contracts;
using TMS.DAL.Data;

namespace TMS.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ITaskRepository _taskRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    // todo is it bad? taskRepository is not injected!!!
    public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}

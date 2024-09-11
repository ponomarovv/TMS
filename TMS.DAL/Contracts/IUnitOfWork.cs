using System.Threading.Tasks;

namespace TMS.DAL.Contracts
{
    public interface IUnitOfWork
    {
        ITaskRepository TaskRepository { get; }
        Task SaveAsync();
    }
}

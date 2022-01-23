using System.Threading;
using System.Threading.Tasks;

namespace CqrsMediatorExamp.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        Task CompleteAsync(CancellationToken token);
    }
}
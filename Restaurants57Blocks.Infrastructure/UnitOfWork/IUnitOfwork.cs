using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Infrastructure.Repository;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Infrastructure.UnitOfWork
{
    public interface IUnitOfwork
    {
        Repository<User> User { get; }
        void Dispose();
        int Save();
        Task<int> SaveAsync();
    }
}

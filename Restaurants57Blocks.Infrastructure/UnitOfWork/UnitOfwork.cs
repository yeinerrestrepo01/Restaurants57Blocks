using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Infrastructure.DBContext;
using Restaurants57Blocks.Infrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Infrastructure.UnitOfWork
{

    /// <summary>
    /// Clase para definiciones de unidades de trabajo
    /// </summary>
    public class UnitOfwork : IUnitOfwork
    {

        private readonly Restaurants57BlocksDBContext _restaurants57BlocksDBContext;

        private Repository<User> RepositoryUser;
        public UnitOfwork(Restaurants57BlocksDBContext restaurants57BlocksDBContext)
        {
            _restaurants57BlocksDBContext = restaurants57BlocksDBContext;
        }

        /// <summary>
        /// Unidad de trabajo Repository<Owner> 
        /// </summary>
        public Repository<User> User
        {
            get
            {
                if (RepositoryUser == null)
                    RepositoryUser = new Repository<User>(_restaurants57BlocksDBContext);

                return RepositoryUser;
            }
        }

        public int Save() => _restaurants57BlocksDBContext.SaveChanges();
        public async Task<int> SaveAsync() => await _restaurants57BlocksDBContext.SaveChangesAsync();

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _restaurants57BlocksDBContext.Dispose();
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

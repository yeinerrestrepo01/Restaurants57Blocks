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
        
        private Repository<Restaurant> RepositoryRestaurant;

        private Repository<Employee> RepositoryEmployee;
        public UnitOfwork(Restaurants57BlocksDBContext restaurants57BlocksDBContext)
        {
            _restaurants57BlocksDBContext = restaurants57BlocksDBContext;
        }

        /// <summary>
        /// Unidad de trabajo Repository<User> 
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

        /// <summary>
        /// Unidad de trabajo Repository<Restaurant> 
        /// </summary>
        public Repository<Restaurant> Restaurant
        {
            get
            {
                if (RepositoryRestaurant == null)
                    RepositoryRestaurant = new Repository<Restaurant>(_restaurants57BlocksDBContext);

                return RepositoryRestaurant;
            }
        }

        /// <summary>
        /// Unidad de trabajo Repository<Employee> 
        /// </summary>
        public Repository<Employee> Employee
        {
            get
            {
                if (RepositoryEmployee == null)
                    RepositoryEmployee = new Repository<Employee>(_restaurants57BlocksDBContext);

                return RepositoryEmployee;
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

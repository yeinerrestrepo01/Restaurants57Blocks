using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Infrastructure.DBContext;
using Restaurants57Blocks.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Infrastructure.GenericRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Interfaz de unidad de trabajo
        /// </summary>
        private readonly IUnitOfwork _unitWork;

        /// <summary>
        /// Inicializador de repositorio de User
        /// </summary>
        /// <param name="restaurants57BlocksDBContext"></param>
        public UserRepository(Restaurants57BlocksDBContext restaurants57BlocksDBContext)
        {
            _unitWork = new UnitOfwork(restaurants57BlocksDBContext);
        }

        public async Task<int> AddAsync(User user)
        {
            await _unitWork.User.InsertAsync(user);
            return await _unitWork.SaveAsync();
        }
        public List<User> GetAll()
        {
            return _unitWork.User.AsQueryable().ToList();
        }

        public User GetById(int idUser)
        {
            return _unitWork.User.FirstOrDefault(o => o.Id == idUser);
        }

        public User ExistsEmail(string email)
        {
            return _unitWork.User.FirstOrDefault(o => o.Email == email);
        }
    }
}

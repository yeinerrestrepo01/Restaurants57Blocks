using Restaurants57Blocks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Infrastructure.GenericRepository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int idUser);
        User ExistsEmail(string email);
        Task<int> AddAsync(User user);
    }
}

using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Domain.Request;
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
        UserLoginDto LoginUsuario(LoginRequest login);
    }
}

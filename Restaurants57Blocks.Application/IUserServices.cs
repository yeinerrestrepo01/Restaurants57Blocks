using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Application
{
    public interface IUserServices
    {
        List<UserDto> GetAll();
        ResponseDto<UserDto> GetById(int idUser);
        Task<ResponseDto<bool>> AddAsync(UserRequest user);
    }
}

using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Application
{
    public interface IEmployeeServices
    {
        ResponseDto<List<EmployeeDto>> GetAll(string tokenAcceso);
        ResponseDto<EmployeeDto> GetById(int idEmployee, string tokenAcceso);
        Task<ResponseDto<bool>> AddAsync(EmployeeRequest employee);
    }
}

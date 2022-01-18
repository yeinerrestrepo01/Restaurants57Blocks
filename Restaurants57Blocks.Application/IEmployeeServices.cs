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
        List<EmployeeDto> GetAll();
        ResponseDto<EmployeeDto> GetById(int idEmployee);
        Task<ResponseDto<bool>> AddAsync(EmployeeRequest employee);
    }
}

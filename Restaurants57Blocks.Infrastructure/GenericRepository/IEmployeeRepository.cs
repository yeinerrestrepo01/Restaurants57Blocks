using Restaurants57Blocks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Infrastructure.GenericRepository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(string idEmployee);
        Task<int> AddAsync(Employee employee);
    }
}

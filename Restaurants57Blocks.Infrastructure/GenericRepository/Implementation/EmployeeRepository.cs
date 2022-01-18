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
    /// <summary>
    /// EmployeeRepository
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Interfaz de unidad de trabajo
        /// </summary>
        private readonly IUnitOfwork _unitWork;

        /// <summary>
        /// Inicializador de repositorio de User
        /// </summary>
        /// <param name="restaurants57BlocksDBContext"></param>
        public EmployeeRepository(Restaurants57BlocksDBContext restaurants57BlocksDBContext)
        {
            _unitWork = new UnitOfwork(restaurants57BlocksDBContext);
        }

        /// <summary>
        /// Metodo para crear un restaurante
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(Employee employee)
        {
            await _unitWork.Employee.InsertAsync(employee);
            return await _unitWork.SaveAsync();
        }

        /// <summary>
        /// Metodo para listar todos los restaurantes creados
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAll()
        {
            return _unitWork.Employee.AsQueryable().ToList();
        }

        /// <summary>
        /// Metodo para retornar la informacion de un restaurante consultado
        /// </summary>
        /// <param name="idRestaurant"></param>
        /// <returns></returns>
        public Employee GetById(int idEmployee)
        {
            return _unitWork.Employee.FirstOrDefault(o => o.Identifcation == idEmployee);
        }
    }
}

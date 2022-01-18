using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
namespace Restaurants57Blocks.Application.Validations
{
    public class EmployeeValitaions
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeValitaions(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Validacion para creacion de restaurantes
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ExistsEegisterEmployee(EmployeeRequest employee)
        {
            var resultValitaion = true;
            var queryResult = _employeeRepository.GetById(employee.Identification);
            if (queryResult != null)
            {
                resultValitaion = false;
            }
            return resultValitaion;
        }
    }
}

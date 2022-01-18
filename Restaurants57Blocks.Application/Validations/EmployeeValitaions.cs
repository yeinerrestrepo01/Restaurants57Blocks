using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
namespace Restaurants57Blocks.Application.Validations
{
    public class EmployeeValitaions
    {

        protected string _messageValidation;
        private readonly IRestaurantRepository _restaurantRepository;
        public EmployeeValitaions(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        /// <summary>
        /// Validacion para creacion de restaurantes
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool ExistsEmployee(EmployeeRequest employee, ref string message)
        {
            var resultValitaion = true;
            var queryResult = _restaurantRepository.GetById(employee.RestaurantId);
            if (queryResult != null)
            {
                resultValitaion = false;
                message = Message.Not_Information_Restaurant;
            }
            return resultValitaion;
        }
    }
}

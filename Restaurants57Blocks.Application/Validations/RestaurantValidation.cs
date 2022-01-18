using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Infrastructure.GenericRepository;

namespace Restaurants57Blocks.Application.Validations
{
    public class RestaurantValidation
    {
        public string _messageValidation;
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantValidation(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        /// <summary>
        /// Validacion para creacion de restaurantes
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ExistsRestaurant(string idRestaurant, ref string message)
        {
            var resultValitaion = true;
            var queryResult = _restaurantRepository.GetById(idRestaurant);
            if (queryResult == null)
            {
                resultValitaion = false;
                message = Message.Not_Information_Restaurant;
            }
            return resultValitaion;
        }
    }
}

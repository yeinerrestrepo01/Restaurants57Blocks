using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Application
{
    public interface IRestaurantServices
    {
        List<RestaurantDto> GetAll();
        ResponseDto<RestaurantDto> GetById(int idRestaurant);
        Task<ResponseDto<bool>> AddAsync(RestaurantRequest restaurant);
    }
}

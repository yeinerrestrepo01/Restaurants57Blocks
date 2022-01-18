using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Application
{
    public interface IRestaurantServices
    {
        ResponseDto<List<RestaurantDto>> GetAll(string TokenTransacction);
        ResponseDto<RestaurantDto> GetById(string idRestaurant, string tokenTransacction);
        Task<ResponseDto<bool>> AddAsync(RestaurantRequest restaurant);
    }
}

using Restaurants57Blocks.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Infrastructure.GenericRepository
{
    public interface IRestaurantRepository
    {
        List<Restaurant> GetAll();
        Restaurant GetById(string idRestaurant);
        Task<int> AddAsync(Restaurant restaurant);
    }
}

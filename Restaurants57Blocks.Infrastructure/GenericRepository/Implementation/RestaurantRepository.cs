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
    /// Repostoty para  mandejo de mutaciones de restaurantes
    /// </summary>
    public class RestaurantRepository : IRestaurantRepository
    {

        /// <summary>
        /// Interfaz de unidad de trabajo
        /// </summary>
        private readonly IUnitOfwork _unitWork;

        /// <summary>
        /// Inicializador de repositorio de User
        /// </summary>
        /// <param name="restaurants57BlocksDBContext"></param>
        public RestaurantRepository(Restaurants57BlocksDBContext restaurants57BlocksDBContext)
        {
            _unitWork = new UnitOfwork(restaurants57BlocksDBContext);
        }

        /// <summary>
        /// Metodo para crear un restaurante
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(Restaurant restaurant)
        {
            await _unitWork.Restaurant.InsertAsync(restaurant);
            return await _unitWork.SaveAsync();
        }

        /// <summary>
        /// Metodo para listar todos los restaurantes creados
        /// </summary>
        /// <returns></returns>
        public List<Restaurant> GetAll()
        {
            return _unitWork.Restaurant.AsQueryable().ToList();
        }

        /// <summary>
        /// Metodo para retornar la informacion de un restaurante consultado
        /// </summary>
        /// <param name="idRestaurant"></param>
        /// <returns></returns>
        public Restaurant GetById(string idRestaurant)
        {
            return _unitWork.Restaurant.FirstOrDefault(o => o.Identifcation == idRestaurant);
        }
    }
}

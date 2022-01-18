using AutoMapper;
using Restaurants57Blocks.Application.Validations;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Application.Implementation
{
    /// <summary>
    /// clase de logica de servicios para restaurantes
    /// </summary>
    public class RestaurantServices: IRestaurantServices
    {

        private string _messageValidationsRestaurant;

        private readonly IMapper _mapper;

        private readonly IRestaurantRepository _restaurantRepository;

        private  RestaurantValidation _restaurantValitaions;

        /// <summary>
        /// Inincializador de clase <class>RestaurantServices</class>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="restaurantRepository"></param>
        public RestaurantServices(IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RestaurantRequest, Restaurant>();
                _ = cfg.CreateMap<Restaurant, RestaurantDto>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Encargado de realizar la insercion de los restauranres
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> AddAsync(RestaurantRequest restaurant)
        {
            var Response = new ResponseDto<bool>();
            if (!ValidationRegisterRestaurant(restaurant)) 
            {
                var restaurantEntity = _mapper.Map<RestaurantRequest, Restaurant>(restaurant);
                var ResultRestaurant = await _restaurantRepository.AddAsync(restaurantEntity);
                
                if (ResultRestaurant.Equals(0))
                {
                    Response.StatusCode = 202;
                    Response.Message = Message.Error_Proccess;
                }
                else
                {
                    Response.Message = Message.Successful_Register;
                    Response.Data = true;
                    Response.IsSuccess = true;
                    Response.StatusCode = 200;
                }
            }
            else
            {
                Response.StatusCode = 202;
                Response.Message = _messageValidationsRestaurant;
                Response.Data = true;
                Response.IsSuccess = true;
            }
            
            return Response;
        }

        /// <summary>
        /// Realiza la busqueda de todos los restaurantes creados
        /// </summary>
        /// <returns></returns>
        public List<RestaurantDto> GetAll()
        {
            var ResulQuery = _restaurantRepository.GetAll();
            return _mapper.Map<List<RestaurantDto>>(ResulQuery);
        }

        /// <summary>
        /// Realiza la busqueda de un restaurante especifico
        /// </summary>
        /// <param name="idRestaurant"></param>
        /// <returns></returns>
        public ResponseDto<RestaurantDto> GetById(string idRestaurant)
        {
            var Response = new ResponseDto<RestaurantDto>();
            var GetEntity = _restaurantRepository.GetById(idRestaurant);
            if (GetEntity == null)
            {
                Response.Message = Message.Not_Information;
                Response.StatusCode = 204;
            }
            else
            {
                Response.Message = Message.Successful_Query;
                Response.IsSuccess = true;
                Response.Data = _mapper.Map<RestaurantDto>(GetEntity);
            }
            return Response;
        }

        private bool ValidationRegisterRestaurant(RestaurantRequest restaurant)
        {
            var validationResultRestaurant = true;
            _restaurantValitaions = new RestaurantValidation(_restaurantRepository);
            if (!_restaurantValitaions.ExistsRestaurant(restaurant.Identifcation))
            {
                validationResultRestaurant = false;
                _messageValidationsRestaurant = Message.Not_Information_Restaurant;

            }
            else
            {
                _messageValidationsRestaurant = Message.Exists_Information_Restaurant;
            }
            return validationResultRestaurant;
        }
    }
}

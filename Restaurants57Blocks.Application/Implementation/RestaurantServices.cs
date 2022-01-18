using AutoMapper;
using Restaurants57Blocks.Application.Validations;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using Restaurants57Blocks.Infrastructure.ProviderCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Application.Implementation
{
    /// <summary>
    /// clase de logica de servicios para restaurantes
    /// </summary>
    public class RestaurantServices : IRestaurantServices
    {

        private string _messageValidationsRestaurant;

        private readonly IMapper _mapper;

        private readonly IRestaurantRepository _restaurantRepository;

        private readonly IEmployeeRepository _employeeRepository;

        private object _informactiosesion;

        private string _messageValidationAuthorization;
        private RestaurantValidation _restaurantValitaions { get; set; }

        private readonly MemoryCacheProvider _provedieCache;

        /// <summary>
        /// Inincializador de clase <class>RestaurantServices</class>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="restaurantRepository"></param>
        public RestaurantServices(IMapper mapper,
            IRestaurantRepository restaurantRepository,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantRequest, Restaurant>();
                _ = cfg.CreateMap<Restaurant, RestaurantDto>();
            });
            _mapper = config.CreateMapper();
            _provedieCache = MemoryCacheProvider.Instance();
            _restaurantValitaions = new RestaurantValidation(restaurantRepository);
            _employeeRepository = employeeRepository;
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
        public ResponseDto<List<RestaurantDto>> GetAll(string TokenTransacction)
        {
            var resulutQueryAll = new ResponseDto<List<RestaurantDto>>();

            if (!Validationauthorization(TokenTransacction,string.Empty))
            {
                resulutQueryAll.Message = _messageValidationAuthorization;
                resulutQueryAll.StatusCode = 401;
            }
            else
            {
                var ojectUsersesion = (UserLoginDto)_informactiosesion;
                var ResulQuery = _restaurantRepository.GetAll().Where(t => t.Identifcation == ojectUsersesion.RestaurantId);
                resulutQueryAll.Data = _mapper.Map<List<RestaurantDto>>(ResulQuery);
                resulutQueryAll.IsSuccess = true;
                resulutQueryAll.StatusCode = 200;
                resulutQueryAll.Message = Message.Successful_Query;
            }

            return resulutQueryAll;
        }

        /// <summary>
        /// Realiza la busqueda de un restaurante especifico
        /// </summary>
        /// <param name="idRestaurant"></param>
        /// <returns></returns>
        public ResponseDto<RestaurantDto> GetById(string idRestaurant, string tokenTransacction)
        {
            var Response = new ResponseDto<RestaurantDto>();
            
            if (!Validationauthorization(tokenTransacction, idRestaurant))
            {
                Response.Message = _messageValidationAuthorization;
                Response.StatusCode = 401;
            }
            else
            {
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
            }
            return Response;
        }

        /// <summary>
        /// Validaciones para registro
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        private bool ValidationRegisterRestaurant(RestaurantRequest restaurant)
        {
            var validationResultRestaurant = true;
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

        /// <summary>
        /// Valiacion de atorizacion de consulta y token activo
        /// </summary>
        /// <returns></returns>
        private bool Validationauthorization(string tokenTransacction, string restaurantId)
        {
            _informactiosesion = _provedieCache.GetCache(tokenTransacction);
            var resultValidationauthorization = true;
            if (_informactiosesion != null && !string.IsNullOrEmpty(restaurantId))
            {
                var ojectUsersesionValidation = (UserLoginDto)_informactiosesion;
                var validationRestaurant = _employeeRepository.GetAll().Where(e => e.Identification == ojectUsersesionValidation.EmployeeId
                && e.RestaurantId == restaurantId).FirstOrDefault();
                if (validationRestaurant == null)
                {
                    _messageValidationAuthorization = Message.Not_Access;
                    resultValidationauthorization = false;
                }
            }
            else
            {
                resultValidationauthorization = false;
                _messageValidationAuthorization = Message.Error_Token;
            }
            return resultValidationauthorization;
        }
    }
}

using AutoMapper;
using Restaurants57Blocks.Application.Validations;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using Restaurants57Blocks.Infrastructure.ProviderCache;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Application.Implementation
{
    /// <summary>
    ///   EmployeeServices
    /// </summary>
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IMapper _mapper;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly RestaurantValidation _restaurantValitaions;

        private  EmployeeValitaions _employeeValitaions;

        private string _messageValidations;

        private string _messageValidationAuthorization;

        private readonly MemoryCacheProvider _provedieCache;

        private object _informatiosesion;

        /// <summary>
        /// Inincializador de clase <class>RestaurantServices</class>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="restaurantRepository"></param>
        public EmployeeServices(IMapper mapper,
            IEmployeeRepository employeeRepository,
            IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _restaurantValitaions = new RestaurantValidation(restaurantRepository);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeRequest, Employee>();
                _ = cfg.CreateMap<Employee, EmployeeDto>();
            });
            _mapper = config.CreateMapper();
            _provedieCache = MemoryCacheProvider.Instance();
        }

        /// <summary>
        /// Encargado de realizar la insercion de los Employee
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> AddAsync(EmployeeRequest employee)
        {
            var Response = new ResponseDto<bool>();
            if (ValidationRegister(employee.RestaurantId,employee.Identification))
            {
                var employeeEntity = _mapper.Map<EmployeeRequest, Employee>(employee);
                var ResultAddemployee = await _employeeRepository.AddAsync(employeeEntity);
                
                if (ResultAddemployee.Equals(0))
                {
                    Response.StatusCode = 202;
                    Response.Message = Message.Error_Proccess;
                }
                else
                {
                    Response.Message = Message.Successful_Register;
                    Response.Data = true;
                    Response.IsSuccess = true;
                }
            }
            else
            {
                Response.StatusCode = 202;
                Response.Message = _messageValidations;
                Response.Data = false;
                Response.IsSuccess = true;
            }
            
            return Response;
        }

        /// <summary>
        /// Realiza la busqueda de todos los Employee creados
        /// </summary>
        /// <returns></returns>
        public ResponseDto<List<EmployeeDto>> GetAll(string tokenAcceso)
        {
            var resulutQueryAll = new ResponseDto<List<EmployeeDto>>();
            if (!ValidationAuthorization(tokenAcceso,0))
            {
                resulutQueryAll.Message = _messageValidationAuthorization;
                resulutQueryAll.StatusCode = 401;
            }
            else
            {
                var ojectUsersesion = (UserLoginDto)_informatiosesion;
                var ResulQuery = _employeeRepository.GetAll().Where(e=> e.RestaurantId == ojectUsersesion.RestaurantId);
                resulutQueryAll.IsSuccess = true;
                resulutQueryAll.StatusCode = 200;
                resulutQueryAll.Message = Message.Successful_Query;
                resulutQueryAll.Data =_mapper.Map<List<EmployeeDto>>(ResulQuery);
            }

            return resulutQueryAll;
        }

        /// <summary>
        /// Realiza la busqueda de un Employee especifico
        /// </summary>
        /// <param name="idEmployee"></param>
        /// <returns></returns>
        public ResponseDto<EmployeeDto> GetById(int idEmployee, string tokenAcceso)
        {
            var Response = new ResponseDto<EmployeeDto>();
            
            if (!ValidationAuthorization(tokenAcceso, idEmployee))
            {
                Response.Message = _messageValidationAuthorization;
                Response.StatusCode = 401;
            }
            else
            {
                var GetEntity = _employeeRepository.GetById(idEmployee);
                Response.Message = Message.Successful_Query;
                Response.IsSuccess = true;
                Response.Data = _mapper.Map<EmployeeDto>(GetEntity);

            }
            return Response;
        }

        /// <summary>
        /// procesa las Validacion para poder realziar un registro de empleado
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool ValidationRegister(string restaurantId, int identification) 
        {
            _employeeValitaions = new EmployeeValitaions(_employeeRepository);
            var validationResult = true;
            if (!_restaurantValitaions.ExistsRestaurant(restaurantId))
            {
                validationResult = false;
                _messageValidations = Message.Not_Information_Restaurant;

            }
            else if(!_employeeValitaions.ExistsEegisterEmployee(identification))
            {
                validationResult = false;
                _messageValidations += Message.Exists_Information_Employee;
            }
            return validationResult;
        }

        private bool ValidationAuthorization(string tokenAcceso, int idEmployee)
        {
            var resultAuthorization = true;
            _informatiosesion = tokenAcceso !=null?_provedieCache.GetCache(tokenAcceso):null;
            if (_informatiosesion != null)
            {
                var ojectUserAuthorization = (UserLoginDto)_informatiosesion;
                var userAuthorization =_employeeRepository.GetAll().Where(e => e.Identification == idEmployee && 
                            e.RestaurantId == ojectUserAuthorization.RestaurantId).FirstOrDefault();

                if (userAuthorization == null && idEmployee > 0)
                {
                    _messageValidationAuthorization = Message.Not_Access;
                    resultAuthorization = false;
                }
            }
            else
            {
                resultAuthorization = false;
                _messageValidationAuthorization = Message.Error_Token;
            }

            return resultAuthorization;
        }
    }
}

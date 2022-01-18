using AutoMapper;
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
    ///   EmployeeServices
    /// </summary>
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IMapper _mapper;

        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Inincializador de clase <class>RestaurantServices</class>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="restaurantRepository"></param>
        public EmployeeServices(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeRequest, Employee>();
                _ = cfg.CreateMap<Employee, EmployeeDto>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Encargado de realizar la insercion de los Employee
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> AddAsync(EmployeeRequest employee)
        {
            var employeeEntity = _mapper.Map<EmployeeRequest, Employee>(employee);
            var ResultAddemployee = await _employeeRepository.AddAsync(employeeEntity);
            var Response = new ResponseDto<bool>();
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
            return Response;
        }

        /// <summary>
        /// Realiza la busqueda de todos los Employee creados
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDto> GetAll()
        {
            var ResulQuery = _employeeRepository.GetAll();
            return _mapper.Map<List<EmployeeDto>>(ResulQuery);
        }

        /// <summary>
        /// Realiza la busqueda de un Employee especifico
        /// </summary>
        /// <param name="idEmployee"></param>
        /// <returns></returns>
        public ResponseDto<EmployeeDto> GetById(int idEmployee)
        {
            var Response = new ResponseDto<EmployeeDto>();
            var GetEntity = _employeeRepository.GetById(idEmployee);
            if (GetEntity == null)
            {
                Response.Message = Message.Not_Information;
                Response.StatusCode = 204;
            }
            else
            {
                Response.Message = Message.Successful_Query;
                Response.IsSuccess = true;
                Response.Data = _mapper.Map<EmployeeDto>(GetEntity);
            }
            return Response;
        }
    }
}

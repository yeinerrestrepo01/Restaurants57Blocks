using AutoMapper;
using Restaurants57Blocks.Application.Validations;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Common.Security;
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
    public class UserServices : IUserServices
    {

        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;

        private EmployeeValitaions _validationEmployee;

        private string  _messageValidationUser;

        /// <summary>
        /// Inincializador de clase <class>UserServices</class>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="employeeRepository"></param>
        public UserServices(IMapper mapper,
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _validationEmployee = new EmployeeValitaions( employeeRepository);
            _userRepository = userRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequest, User>();
                _ = cfg.CreateMap<User, UserDto>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> AddAsync(UserRequest user)
        {
            var Response = new ResponseDto<bool>();
            if (ValidationRegisterUser(user.EmployeeId, user.Email))
            {
                user.Password = SecurityManager.EncryptSHA512(user.Password);
                var userEntity = _mapper.Map<UserRequest, User>(user);
                userEntity.DateRegister = DateTime.Now;
                var resultUser = await _userRepository.AddAsync(userEntity);

                if (resultUser.Equals(0))
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
                Response.Message = _messageValidationUser;
                Response.Data = true;
                Response.IsSuccess = true;
            }
            return Response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<UserDto> GetAll()
        {
            var ResulQuery = _userRepository.GetAll();
            return _mapper.Map<List<UserDto>>(ResulQuery);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public ResponseDto<UserDto> GetById(int idUser)
        {
            var Response = new ResponseDto<UserDto>();
            var GetEntity = _userRepository.GetById(idUser);
            if (GetEntity == null)
            {
                Response.Message = Message.Not_Information;
                Response.StatusCode = 204;
            }
            else
            {
                Response.Message = Message.Successful_Query;
                Response.IsSuccess = true;
                Response.Data = _mapper.Map<UserDto>(GetEntity);
            }
            return Response;
        }
        private bool ValidationRegisterUser(int employeeId, string email)
        {
            var resultValidationUser = true;
            var _emailValidation = _userRepository.ExistsEmail(email);
            if (_emailValidation != null)
            {
                _messageValidationUser = Message.Exists_Email;
                resultValidationUser = false;

            }else if (_validationEmployee.ExistsEegisterEmployee(employeeId))
            {
                _messageValidationUser =Message.Not_Information_Employee;
                resultValidationUser = false;
            }
            return resultValidationUser;
        }
    }
}

using AutoMapper;
using Restaurants57Blocks.Application.Validations;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Common.Security;
using Restaurants57Blocks.Domain.Dto;
using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using Restaurants57Blocks.Infrastructure.ProviderCache;
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

        private readonly EmployeeValitaions _validationEmployee;

        private string  _messageValidationUser;

        private readonly MemoryCacheProvider _provedieCache;

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
            _provedieCache = MemoryCacheProvider.Instance();
        }

        /// <summary>
        ///  inserta el registro de usuario
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
        ///  trae el lisatado de usuarios creados
        /// </summary>
        /// <returns></returns>
        public List<UserDto> GetAll()
        {
            var ResulQuery = _userRepository.GetAll();
            return _mapper.Map<List<UserDto>>(ResulQuery);
        }

        /// <summary>
        /// busca un usuario en especifico
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

        /// <summary>
        /// Valida la informacion para el proceso de login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        public ResponseDto<UserLoginDto> LoginUsuario(LoginRequest login)
        {
            var Response = new ResponseDto<UserLoginDto>();
            login.Password = SecurityManager.EncryptSHA512(login.Password);
            var GetEntity = _userRepository.LoginUsuario(login);
            if (GetEntity == null)
            {
                Response.Message = Message.Login_Error;
                Response.StatusCode = 202;
            }
            else
            {
                var KeyCache = Guid.NewGuid();
                _provedieCache.SetCahe(KeyCache.ToString(), GetEntity);
                GetEntity.Token = KeyCache.ToString();
                Response.Message = Message.Login_Successful;
                Response.IsSuccess = true;
                Response.Data = GetEntity;
            }
            return Response;
        }

        /// <summary>
        /// Validaciones para procesar registros
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
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

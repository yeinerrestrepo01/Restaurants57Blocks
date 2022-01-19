using AutoMapper;
using Moq;
using NUnit.Framework;
using Restaurants57Blocks.Application.Implementation;
using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Test
{
    [TestFixture]
    public class UserServicesTest
    {
        private MockRepository mockRepository;

        private Mock<IEmployeeRepository> mockEmployeeRepository;

        private Mock<IRestaurantRepository> mockRestaurantRepository;

        private Mock<IUserRepository> mockUserRepository;

        private Mock<IMapper> mockIMapperRepository;

        [SetUp]
        public void Setup()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            mockEmployeeRepository = this.mockRepository.Create<IEmployeeRepository>();
            mockRestaurantRepository = this.mockRepository.Create<IRestaurantRepository>();
            mockUserRepository = this.mockRepository.Create<IUserRepository>();
            mockIMapperRepository = this.mockRepository.Create<IMapper>();
        }

        public UserServices CreateService()
        {
            return new UserServices(mockIMapperRepository.Object, mockUserRepository.Object, mockEmployeeRepository.Object);
        }

        [Test]
        [Author("Yeiner Merino")]
        public void GetAll()
        {
            // Arrange
            var service = this.CreateService();
            var dbListUser = new List<User>() 
            { 
                new User {
                    Id = 1,
                    Email = "Yeinermeri@gmail.com",
                    EmployeeId =1065635832,
                    DateRegister = DateTime.Now,
                    Password = "12w3e3r4tt",
                }
            };

            this.mockUserRepository.Setup(s => s.GetAll()).Returns(dbListUser);

            var result = service.GetAll();
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        [Author("Yeiner Merino")]
        public async Task Add_User_Nuevo()
        {
            var emmployee = new Employee()
            {
                Identification = 1065635832,
                Email = "yeinermeri@gmail.com",
                FullName = "Yeiner Merino Lopez",
                Phone = "1234567890",
                ResidenceAdress = "Calle 46",
                RestaurantId = "4757588",
                Type = 1
            };

            var dbRestaurant = new User()
            {
                Id = 1,
                Email = "Yeinermeri@gmail.com",
                EmployeeId = 1065635832,
                DateRegister = DateTime.Now,
                Password = "12w3e3r4tt",
            };

            var UserRequest = new UserRequest()
            {
                Email = "Yeinermeri@gmail.com",
                Password = "Complemento*1234",
                EmployeeId = 1065635832
            };


            this.mockEmployeeRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(emmployee);

            this.mockUserRepository.Setup(s => s.ExistsEmail(It.IsAny<string>())).Returns<User>(null);

            this.mockUserRepository.Setup(s => s.AddAsync(It.IsAny<User>())).ReturnsAsync(1);
            // Arrange
            var service = this.CreateService();
            var result = await service.AddAsync(UserRequest);
            Assert.AreEqual(result.IsSuccess, true);
            this.mockRepository.VerifyAll();
        }

        [Test]
        [Author("Yeiner Merino")]
        public async Task Add_User_Email_Existe()
        {
            var dbRestaurant = new Restaurant()
            {
                Identifcation = "4757588",
                Name = "Sierra Nevada",
                Phone = "123456",
                Address = "Calle 45 # 4 Bogota",
                Status = true
            };

            var dbEmployee = new Employee()
            {
                Identification = 1065635832,
                Email = "yeinermeri@gmail.com",
                FullName = "Yeiner Merino Lopez",
                Phone = "1234567890",
                ResidenceAdress = "Calle 46",
                RestaurantId = "4757588",
                Type = 1
            };

            var UserRequest = new UserRequest()
            {
                Email = "Yeinermeri@gmail.com",
                Password = "Complemento*1234",
                EmployeeId = 1065635832
            };


            this.mockEmployeeRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(dbEmployee);

            this.mockUserRepository.Setup(s => s.ExistsEmail(It.IsAny<string>())).Returns(new User() { Email = "Yeinermeri@gmail.com" });

            this.mockUserRepository.Setup(s => s.AddAsync(It.IsAny<User>())).ReturnsAsync(1);
            // Arrange
            var service = this.CreateService();
            var result = await service.AddAsync(UserRequest);
            Assert.AreEqual(result.IsSuccess, true);
        }

    }
}
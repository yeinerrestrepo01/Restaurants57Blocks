using AutoMapper;
using Moq;
using NUnit.Framework;
using Restaurants57Blocks.Application.Implementation;
using Restaurants57Blocks.Domain.Entities;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Test
{
    [TestFixture]
    public class EmployeeServicesTest
    {
        private MockRepository mockRepository;

        private Mock<IEmployeeRepository> mockEmployeeRepository;

        private Mock<IRestaurantRepository> mockRestaurantRepository;

        private Mock<IMapper> mockIMapperRepository;

        [SetUp]
        public void Setup()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            mockEmployeeRepository = this.mockRepository.Create<IEmployeeRepository>();
            mockRestaurantRepository = this.mockRepository.Create<IRestaurantRepository>();
            mockIMapperRepository = this.mockRepository.Create<IMapper>();
        }

        public EmployeeServices CreateService()
        {
            return new EmployeeServices(mockIMapperRepository.Object, mockEmployeeRepository.Object, mockRestaurantRepository.Object);
        }

        [Test]
        [Author("Yeiner Merino")]
        public void GetAll_Token_Invalido()
        {
            // Arrange
            var service = this.CreateService();
            var result = service.GetAll("aaaaaaaaaaaaaaa");
            Assert.AreEqual(result.StatusCode, 401);
        }

        [Test]
        [Author("Yeiner Merino")]
        public async Task Add_User_Nuevo()
        {
            var employeeRequest = new EmployeeRequest()
            {
                Identification = 1065635832,
                Email = "yeinermeri@gmail.com",
                FullName = "Yeiner Merino Lopez",
                Phone = "1234567890",
                ResidenceAdress = "Calle 46",
                RestaurantId = "4757588",
                Type = 1
            };

            var dbRestaurant = new Restaurant()
            {
                Identifcation = "4757588",
                Name = "Sierra Nevada",
                Phone = "123456",
                Address = "Calle 45 # 4 Bogota",
                Status = true
            };

            this.mockRestaurantRepository.Setup(s => s.GetById("4757588")).Returns(dbRestaurant);

            this.mockEmployeeRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns<Employee>(null);

            this.mockEmployeeRepository.Setup(s => s.AddAsync(It.IsAny<Employee>())).ReturnsAsync(1);
            // Arrange
            var service = this.CreateService();
            var result = await service.AddAsync(employeeRequest);
            Assert.AreEqual(result.IsSuccess, true);
            this.mockRepository.VerifyAll();
        }

        [Test]
        [Author("Yeiner Merino")]
        public async Task Add_User_Existente()
        {
            var employeeRequest = new EmployeeRequest()
            {
                Identification = 1065635832,
                Email = "yeinermeri@gmail.com",
                FullName = "Yeiner Merino Lopez",
                Phone = "1234567890",
                ResidenceAdress = "Calle 46",
                RestaurantId = "4757588",
                Type = 1
            };

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


            this.mockRestaurantRepository.Setup(s => s.GetById("4757588")).Returns(dbRestaurant);

            this.mockEmployeeRepository.Setup(s => s.GetById(1065635832)).Returns(dbEmployee);

            this.mockEmployeeRepository.Setup(s => s.AddAsync(It.IsAny<Employee>())).ReturnsAsync(1);
            // Arrange
            var service = this.CreateService();
            var result = await service.AddAsync(employeeRequest);
            Assert.AreEqual(result.StatusCode, 202);
            this.mockRepository.VerifyAll();
        }


    }
}
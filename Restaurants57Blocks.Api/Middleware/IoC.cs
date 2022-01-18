using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurants57Blocks.Application;
using Restaurants57Blocks.Application.Implementation;
using Restaurants57Blocks.Domain.FluentValidation;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using Restaurants57Blocks.Infrastructure.GenericRepository.Implementation;
using Restaurants57Blocks.Infrastructure.Repository;
using Restaurants57Blocks.Infrastructure.UnitOfWork;

namespace Restaurants57Blocks.Api.Middleware
{
    public static class IoC
    {
        /// <summary>
        ///  Adds the dependency.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            // Infrastructure
            #region Infrastructure
            services.AddTransient<IUnitOfwork, UnitOfwork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            #endregion

            //Services
            #region Services
            services.AddTransient<IRestaurantServices, RestaurantServices>();
            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<IUserServices, UserServices>();
            #endregion

            #region Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            #endregion

            #region FluentValidator
            services.AddSingleton<IValidator<UserRequest>, UserValidator>();
            services.AddSingleton<IValidator<RestaurantRequest>, RestaurantValidator>();
            services.AddSingleton<IValidator<EmployeeRequest>, EmployeeValidator>();

            #endregion

            return services;
        }
    }
}

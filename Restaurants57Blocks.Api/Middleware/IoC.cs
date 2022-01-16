using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurants57Blocks.Domain.FluentValidation;
using Restaurants57Blocks.Domain.Request;
using Restaurants57Blocks.Infrastructure.GenericRepository;
using Restaurants57Blocks.Infrastructure.Repository;
using Restaurants57Blocks.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            #endregion

            #region Repository
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion

            #region FluentValidator
            services.AddSingleton<IValidator<UserRequest>, UserValidator>();

            #endregion

            return services;
        }
    }
}

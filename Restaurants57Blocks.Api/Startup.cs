using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Restaurants57Blocks.Api.Middleware;
using Restaurants57Blocks.Infrastructure.DBContext;

namespace Restaurants57Blocks.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "57Blocks Restaurants API",
                    Version = "v1",
                    Description = "API encaragda de gestionar la información sobre restaurantes " +
                  "asociados a 57Blocks"
                });
            });

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Restaurants57BlocksDBContext>(options => options.UseSqlServer(connection));
            #region Register (dependency injection)
            IoC.AddDependency(services);
            #endregion
            services.AddControllers();
            // Configuración FluentValidator
            services.AddMvc().AddFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "57Blocks Restaurants API v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

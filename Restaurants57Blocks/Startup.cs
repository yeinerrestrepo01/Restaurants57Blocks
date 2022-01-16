using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

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
            #region Register (dependency injection)
            #endregion
            services.AddControllers();
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

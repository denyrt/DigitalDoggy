using DigitalDoggy.BusinessLogic.Extensions;
using DigitalDoggy.BusinessLogic.Pipelines;
using DigitalDoggy.DataAccess;
using DigitalDoggy.Domain.Constants;
using DigitalDoggy.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DigitalDoggy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DigitalDoggy", Version = "v1" });
            });

            services.AddDbContext<DoggyDbContext>(options => 
            {
                options.UseSqlServer(EnvironmentConstants.DbConnectionString);
            });

            services.AddSingleton(new ConcurrentRequestsMiddleware(EnvironmentConstants.MaxConcurrentRequests));
            services.AddMediatR(BusinessAssembly.GetAssembly());
            services.AddValidatorsFromAssembly(BusinessAssembly.GetAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DoggyValidationBehavior<,>));
            services.AddTransient<FluentValidationMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalDoggy v1"));
            }

            app.UseMiddleware<ConcurrentRequestsMiddleware>();
            app.UseMiddleware<FluentValidationMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

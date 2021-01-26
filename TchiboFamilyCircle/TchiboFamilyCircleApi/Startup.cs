using Autofac;
using AutofacSerilogIntegration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using TchiboFamilyCircle.DomainService;
using TchiboFamilyCircle.DomainService.Contracts;
using TchiboFamilyCircle.Mapping;

namespace TchiboFamilyCircle
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tchibo Family Circle Api", Version = "v1" });
            });
                     

            services.AddSingleton
                (
                    provider => new MapperConfiguration(config => 
                    {
                        config.AddProfile(new FamilyMemberMapping());
                    }).CreateMapper()
                );

         
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()                
                .ReadFrom.Configuration(Configuration)              
                .CreateLogger();

            builder.RegisterLogger();

            builder.RegisterType<FamilyMemberService>()
              .As<IFamilyMemberService>();                       

            Log.Information("Hello, world!");          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tchibo Family Circle Api v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSerilogRequestLogging();
        }        
    }
}

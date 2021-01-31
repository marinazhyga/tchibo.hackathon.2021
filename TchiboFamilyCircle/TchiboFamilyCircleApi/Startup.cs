using Autofac;
using AutofacSerilogIntegration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using TchiboFamilyCircle.DomainService;
using TchiboFamilyCircle.Mapping;
using TchiboFamilyCircle.Settings;

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
            services.AddControllers()
                .AddNewtonsoftJson(options =>
            options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Tchibo Family Circle Api",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<AppSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoDb:DatabaseName").Value;
                options.CollectionName = Configuration.GetSection("MongoDb:CollectionName").Value;
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

            builder.RegisterType<SizeService>()
             .As<ISizeService>();
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

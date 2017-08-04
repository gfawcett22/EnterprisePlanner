using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shipments.Contexts;
using Shipments.Entities;
using Shipments.Repositories;
using ShipmentsDtoTypes.Models;
using WebApiHelpers.Formatters;

namespace Shipments
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(cfg =>
            {
                cfg.InputFormatters.Add(new ProtobufInputFormatter());
                cfg.OutputFormatters.Add(new ProtobufOutputFormatter());
            });
            
            services.AddDbContext<ShipmentsDbContext>(o => o.UseInMemoryDatabase());

            services.AddScoped<IShipmentRepository, ShipmentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("A server error occured.");
                });
            });
            
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Shipment, ShipmentDto>();
                cfg.CreateMap<ShipmentToCreateDto, Shipment>();
            });

            app.UseMvc();
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orders.Contexts;
using Orders.Entities;
using Orders.Repositories;
using OrdersDtoTypes.Models;
using WebApiHelpers.Formatters;

namespace Orders
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
            // Add framework services.
            services.AddMvc(cfg =>
            {
                cfg.InputFormatters.Add(new ProtobufInputFormatter());
                cfg.OutputFormatters.Add(new ProtobufOutputFormatter());
            });
            var connectionString = Configuration["connectionStrings:DefaultConnection"];
            services.AddDbContext<OrdersDbContext>(o => o.UseInMemoryDatabase());

            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if it is production, just return general server error message
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("A server error occured.");
                });
            });


            var ordersContext = app.ApplicationServices.GetService<OrdersDbContext>();
            OrdersDbContextSeeder.SeedInMemoryDatabase(ordersContext);

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderToCreateDto, Order>();
                cfg.CreateMap<OrderToUpdateDto, Order>();
            });

            app.UseMvc();
        }
    }
}

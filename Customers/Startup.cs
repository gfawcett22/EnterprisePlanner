using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Customers.Contexts;
using Customers.Repositories;
using Customers.Entities;
using CustomersDtoTypes.Models;
using WebApiHelpers.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Customers
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
            // Add framework services.
            services.AddMvc(cfg =>
            {
                cfg.InputFormatters.Add(new ProtobufInputFormatter());
                cfg.OutputFormatters.Add(new ProtobufOutputFormatter());
            });
            var connectionString = Configuration["connectionStrings:DefaultConnection"];
            services.AddDbContext<CustomersDbContext>(o => o.UseInMemoryDatabase("Customers"));

            services.AddTransient<ICustomerRepository, CustomerRepository>();
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

            app.UseMvc();

            var custContext = app.ApplicationServices.GetService<CustomersDbContext>();
            CustomersDbSeeder.SeedInMemoryDatabase(custContext);

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CustomerToCreateDto, Customer>();
                cfg.CreateMap<CustomerToUpdateDto, Customer>();
            });            
        }
    }
}

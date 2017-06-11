using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Customers.Contexts;
using Customers.Repositories;
using Customers.Entities;
using CustomersDtoTypes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Customers
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
            services.AddMvc();
            var connectionString = Configuration["connectionStrings:DefaultConnection"];
            services.AddDbContext<CustomersDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ICustomerRepository, CustomerRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //if it is production, just return general server error message
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("A server error occured.");
                    });
                });
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CustomerToCreateDto, Customer>();
                cfg.CreateMap<CustomerToUpdateDto, Customer>();
            });

            app.UseMvc();
        }
    }
}

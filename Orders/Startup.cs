using Autofac;
using Autofac.Extensions.DependencyInjection;
using EnterprisePlanner.Messaging.Models;
using EnterprisePlanner.Messaging.Models.Abstractions;
using EnterprisePlanner.Messaging.RabbitMQ;
using EnterprisePlanner.Messaging.RabbitMQ.Abstractions;
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
using RabbitMQ.Client;
using System;
using WebApiHelpers.Formatters;

namespace Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(cfg =>
            {
                cfg.InputFormatters.Add(new ProtobufInputFormatter());
                cfg.OutputFormatters.Add(new ProtobufOutputFormatter());
            });
            
            services.AddDbContext<OrdersDbContext>(o => o.UseInMemoryDatabase("Orders"));

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };

                return new RabbitMQConnection(factory, logger);
            });

            RegisterEventBus(services);

            services.AddScoped<IOrderRepository, OrderRepository>();

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
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


            var ordersContext = app.ApplicationServices.GetService<OrdersDbContext>();
            OrdersDbContextSeeder.SeedInMemoryDatabase(ordersContext);

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderToCreateDto, Order>();
                cfg.CreateMap<OrderToUpdateDto, Order>();
            });

            app.UseMvc();
            ConfigureEventBus(app);
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, RabbitMQEventBus>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var logger = sp.GetRequiredService<ILogger<RabbitMQEventBus>>();

                return new RabbitMQEventBus(rabbitMQPersistentConnection, eventBusSubcriptionsManager, iLifetimeScope, logger, "orders");
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            // Add Event Handlers Here

        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        }
    }
}

using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderManagement.Domain.Consumers;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services;
using OrderManagement.Repository.Mongo;
using SimpleInjector;

namespace OrderManagement.Client.Web
{
    public class Startup
    {
        private readonly Container container = new Container();
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<MongoConfiguration>(Configuration.GetSection(MongoConfiguration.Name));
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection(RabbitMqConfiguration.Name));
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });

            InitializeContainer();
        }

        public void InitializeContainer()
        {
            container.RegisterSingleton<ISystemBus, Producer>();
            container.RegisterSingleton<IOrderRepository, MongoOrderRepository>();
            container.RegisterSingleton<IOrderService, OrderService>();
            container.RegisterSingleton<IConsumer<OrderConfirmed>, OrderConfirmedConsumer>();
            container.RegisterSingleton<IConsumer<OrderRefused>, OrderRefusedConsumer>();
            container.RegisterSingleton<ConsumerWrapper<OrderConfirmed>>();
            container.RegisterSingleton<ConsumerWrapper<OrderRefused>>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            container.GetInstance<ConsumerWrapper<OrderConfirmed>>().Consume();
            container.GetInstance<ConsumerWrapper<OrderRefused>>().Consume();
            
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
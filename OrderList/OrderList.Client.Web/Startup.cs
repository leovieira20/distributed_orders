using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderList.Domain.Repositories;
using OrderList.Domain.Services;
using OrderList.Domain.Consumers;
using OrderList.Domain.Events.Inbound;
using OrderList.Repository.Redis;
using SimpleInjector;

namespace OrderList.Client.Web
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
            services.Configure<RedisConfiguration>(Configuration.GetSection(RedisConfiguration.Name));
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });

            InitializeContainer();
        }
        
        private void InitializeContainer()
        {
            container.RegisterSingleton<IOrderRepository, RedisOrderRepository>();
            container.RegisterSingleton<IConsumer<OrderCreated>, OrderCreatedConsumer>();
            container.RegisterSingleton<IConsumer<OrderConfirmed>, OrderConfirmedConsumer>();
            container.RegisterSingleton<IConsumer<OrderRefused>, OrderRefusedConsumer>();
            container.RegisterSingleton<IConsumer<OrderCancelled>, OrderCancelledConsumer>();
            container.RegisterSingleton<IConsumer<DeliveryAddressUpdated>, DeliverAddressUpdatedConsumer>();
            container.RegisterSingleton<IOrderService, OrderService>();
            container.RegisterSingleton(() =>
            {
                var service = container.GetInstance<IConsumer<OrderCreated>>();
                return new Consumer<OrderCreated>(service.Consume);
            });
            container.RegisterSingleton(() =>
            {
                var service = container.GetInstance<IConsumer<DeliveryAddressUpdated>>();
                return new Consumer<DeliveryAddressUpdated>(service.Consume);
            });
            container.RegisterSingleton(() =>
            {
                var service = container.GetInstance<IConsumer<OrderConfirmed>>();
                return new Consumer<OrderConfirmed>(service.Consume);
            });
            container.RegisterSingleton(() =>
            {
                var service = container.GetInstance<IConsumer<OrderRefused>>();
                return new Consumer<OrderRefused>(service.Consume);
            });
            container.RegisterSingleton(() =>
            {
                var service = container.GetInstance<IConsumer<OrderCancelled>>();
                return new Consumer<OrderCancelled>(service.Consume);
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(container);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            container.GetInstance<Consumer<OrderCreated>>().Consume();
            container.GetInstance<Consumer<OrderConfirmed>>().Consume();
            container.GetInstance<Consumer<OrderRefused>>().Consume();
            container.GetInstance<Consumer<OrderCancelled>>().Consume();
            container.GetInstance<Consumer<DeliveryAddressUpdated>>().Consume();
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
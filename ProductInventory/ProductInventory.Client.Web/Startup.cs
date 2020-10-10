using System;
using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductInventory.Domain.Consumers;
using ProductInventory.Domain.Events;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;
using ProductInventory.Domain.Services;
using ProductInventory.Repository.Mongo;
using SimpleInjector;

namespace ProductInventory.Client.Web
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
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });

            InitializeContainer();
        }
        
        private void InitializeContainer()
        {
            container.RegisterSingleton<ISystemBus, Producer>();
            container.RegisterSingleton<IProductRepository, MongoProductRepository>();
            container.RegisterSingleton<IStockChecker, StockChecker>();
            container.RegisterSingleton<IConsumer<OrderCreated>, OrderCreatedConsumer>();
            container.RegisterSingleton<IConsumer<OrderCancelled>, OrderCancelledConsumer>();
            container.RegisterSingleton(() =>
            {
                var service = container.GetInstance<IConsumer<OrderCreated>>();
                return new Consumer<OrderCreated>(service.Consume);
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

            var repository = container.GetInstance<IProductRepository>();
            repository.Create(Product.CreateWithIdAndQuantity("2b2dab36-f6de-4677-a4b2-abbf57731fa4", 2));
            repository.Create(Product.CreateWithIdAndQuantity("adc366bc-43c6-4420-867d-e1bb96ada786", 3));
            repository.Create(Product.CreateWithIdAndQuantity("bf75aa45-6e94-4655-98f8-6885bf3d1393", 5));
            
            container.GetInstance<Consumer<OrderCreated>>().Consume();
            container.GetInstance<Consumer<OrderCancelled>>().Consume();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
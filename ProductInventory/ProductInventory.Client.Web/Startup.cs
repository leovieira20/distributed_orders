using System;
using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductInventory.Client.Web.Infrastructure;
using ProductInventory.Domain.Consumers;
using ProductInventory.Domain.Events.Inbound;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;
using ProductInventory.Domain.Services;
using ProductInventory.Repository.Mongo;
using ProductInventory.Repository.Mongo.Mapping;
using SimpleInjector;
using Steeltoe.Management.Tracing;

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
            services.AddCors(options =>
                options.AddPolicy("default", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddDistributedTracing(Configuration, builder => builder.UseZipkinWithTraceOptions(services));
            services.AddMvcCore()
                .AddApiExplorer();
            services.Configure<MongoConfiguration>(Configuration.GetSection(MongoConfiguration.Name));
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection(RabbitMqConfiguration.Name));
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });

            InitializeContainer();
        }
        
        private void InitializeContainer()
        {
            container.RegisterSingleton(() => new MapperProvider(container).GetMapper());
            container.RegisterSingleton<ISystemBus, Producer>();
            container.RegisterSingleton<IProductRepository, MongoProductRepository>();
            container.RegisterSingleton<IStockChecker, StockChecker>();
            container.RegisterSingleton<IConsumer<OrderCreated>, OrderCreatedConsumer>();
            container.RegisterSingleton<IConsumer<OrderCancelled>, OrderCancelledConsumer>();
            container.RegisterSingleton<IConsumer<OrderItemsUpdated>, OrderItemsUpdatedConsumer>();
            container.RegisterSingleton<ConsumerWrapper<OrderCreated>>();
            container.RegisterSingleton<ConsumerWrapper<OrderCancelled>>();
            container.RegisterSingleton<ConsumerWrapper<OrderItemsUpdated>>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(container);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var repository = container.GetInstance<IProductRepository>();
            repository.CreateAsync(Product.CreateWithIdAndQuantity(Guid.NewGuid().ToString(), new Random().Next(10)));
            repository.CreateAsync(Product.CreateWithIdAndQuantity(Guid.NewGuid().ToString(), new Random().Next(10)));
            repository.CreateAsync(Product.CreateWithIdAndQuantity(Guid.NewGuid().ToString(), new Random().Next(10)));
            
            container.GetInstance<ConsumerWrapper<OrderCreated>>().Consume();
            container.GetInstance<ConsumerWrapper<OrderCancelled>>().Consume();
            container.GetInstance<ConsumerWrapper<OrderItemsUpdated>>().Consume();

            app.UseRouting();

            app.UseCors("default");
            
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderList.Client.Web.Infrastructure;
using OrderList.Domain.Repositories;
using OrderList.Domain.Services;
using OrderList.Domain.Consumers;
using OrderList.Domain.Events.Inbound;
using OrderList.Repository.Mongo;
using SimpleInjector;
using Steeltoe.Management.Tracing;

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
            services.AddCors(options =>
                options.AddPolicy("default", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddControllers();
            services.AddDistributedTracing(Configuration, builder => builder.UseZipkinWithTraceOptions(services));
            services.AddSwaggerGen();
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
            container.RegisterSingleton<IOrderRepository, MongoOrderRepository>();
            container.RegisterSingleton<IConsumer<OrderCreated>, OrderCreatedConsumer>();
            container.RegisterSingleton<IConsumer<OrderConfirmed>, OrderConfirmedConsumer>();
            container.RegisterSingleton<IConsumer<OrderRefused>, OrderRefusedConsumer>();
            container.RegisterSingleton<IConsumer<OrderCancelled>, OrderCancelledConsumer>();
            container.RegisterSingleton<IConsumer<DeliveryAddressUpdated>, DeliverAddressUpdatedConsumer>();
            container.RegisterSingleton<IOrderService, OrderService>();
            container.RegisterSingleton<ConsumerWrapper<OrderCreated>>();
            container.RegisterSingleton<ConsumerWrapper<OrderConfirmed>>();
            container.RegisterSingleton<ConsumerWrapper<OrderRefused>>();
            container.RegisterSingleton<ConsumerWrapper<OrderCancelled>>();
            container.RegisterSingleton<ConsumerWrapper<DeliveryAddressUpdated>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(container);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            container.GetInstance<ConsumerWrapper<OrderCreated>>().Consume();
            container.GetInstance<ConsumerWrapper<OrderConfirmed>>().Consume();
            container.GetInstance<ConsumerWrapper<OrderRefused>>().Consume();
            container.GetInstance<ConsumerWrapper<OrderCancelled>>().Consume();
            container.GetInstance<ConsumerWrapper<DeliveryAddressUpdated>>().Consume();

            app.UseRouting();
            app.UseCors("default");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderManagement.Client.Web.Infrastructure;
using OrderManagement.Domain.Consumers;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services;
using OrderManagement.Repository.Mongo;
using SimpleInjector;
using Steeltoe.Management.Tracing;

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

        public void InitializeContainer()
        {
            container.RegisterSingleton(() => new MapperProvider(container).GetMapper());
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
            
            container.GetInstance<ConsumerWrapper<OrderConfirmed>>().Consume();
            container.GetInstance<ConsumerWrapper<OrderRefused>>().Consume();
            
            app.UseRouting();
            app.UseCors("default");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using OrderList.Domain.Repositories;
using OrderList.Domain.Services;
using OrderList.Domain.Consumers;
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
            container.RegisterSingleton<IOrderCreatedConsumer, OrderCreatedConsumer>();
            container.RegisterSingleton<IOrderService, OrderService>();
            container.RegisterSingleton(() =>
            {
                var service = container.GetInstance<IOrderCreatedConsumer>();
                return new Consumer<JObject>(service.Create);
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(container);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            container.GetInstance<Consumer<JObject>>().Consume();
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
using AutoMapper;
using AutoMapper.Configuration;
using OrderManagement.Repository.Mongo.Mapping;
using SimpleInjector;

namespace OrderManagement.Client.Web.Infrastructure
{
    public class MapperProvider
    {
        private readonly Container _container;

        public MapperProvider(Container container)
        {
            _container = container;
        }
        
        public IMapper GetMapper()
        {
            var mce = new MapperConfigurationExpression();
            mce.ConstructServicesUsing(_container.GetInstance);

            mce.AddMaps(typeof(MappingProfile).Assembly);

            var mc = new MapperConfiguration(mce);
            mc.AssertConfigurationIsValid();

            IMapper m = new Mapper(mc, t => _container.GetInstance(t));

            return m;
        }
    }
}
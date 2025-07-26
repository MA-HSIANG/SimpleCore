using AutoMapper;

namespace SimpleCore.Extensions.ServiceExtensions
{
    public class ServiceMappingConfig
    {
        public static MapperConfiguration ServiceMappingRegistr()
        {
            var config = new MapperConfiguration(ctg =>
            {
                ctg.AddProfile(new ServiceMapping());
            });
            return config;
        }
    }
}

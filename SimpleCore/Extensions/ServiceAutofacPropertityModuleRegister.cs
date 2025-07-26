using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCore.Extensions
{
    public class ServiceAutofacPropertityModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var controllerBaseType = typeof(ControllerBase);
            //controller使用依賴注入
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }
    }
}

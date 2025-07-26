using Autofac;
using Autofac.Extras.DynamicProxy;
using SimpleCore.Extensions.Aop;
using SimpleCore.Repository.Base;
using SimpleCore.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Extensions.ServiceExtensions
{
    public class ServiceAutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
            var serviceDll = Path.Combine(basePath, "SimpleCore.Service.dll");
            var repositoryDll = Path.Combine(basePath, "SimpleCore.Repository.dll");

            //Aop
            var aopTypes = new List<Type>() { typeof(ExceptionAop) };
            aopTypes.ForEach(type =>
            {
                builder.RegisterType(type);
            });

            //泛型倉儲和服務註冊
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>))
                .InstancePerDependency();
            builder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBaseService<,>))
                .EnableInterfaceInterceptors()
                .InterceptedBy(aopTypes.ToArray())
                .InstancePerDependency();
            //註冊倉儲和服務的所有程序集
            var assemblyServices = Assembly.LoadFrom(serviceDll);
            builder.RegisterAssemblyTypes(assemblyServices)
                .AsImplementedInterfaces()//註冊類型公共接口
                .InstancePerDependency()// Transient
                .PropertiesAutowired()//屬性注入
                .EnableInterfaceInterceptors()//啟用攔截器
                .InterceptedBy(aopTypes.ToArray());//加入攔截器
            var assemblyRepository = Assembly.LoadFrom(repositoryDll);
            builder.RegisterAssemblyTypes(assemblyRepository)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();
        }
    }
}

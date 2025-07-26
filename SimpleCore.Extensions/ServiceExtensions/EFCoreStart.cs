using Autofac;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleCore.Common.Helpers;
using System.Reflection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace SimpleCore.Extensions.ServiceExtensions
{
    public static class EFCoreStart
    {
        public static void AddDbContextsDll(this ContainerBuilder builder, IConfiguration configuration)
        {
            var basePath = AppContext.BaseDirectory;
            var dbContextDll = Path.Combine(basePath, "SimpleCore.Common.dll");
            var assembly = Assembly.LoadFrom(dbContextDll);
            var dbSpace = "SimpleCore.Common.DB.DbContexts";



            var dbContextTypes = assembly.GetTypes()
                  .Where(t => t.IsClass &&
                  !t.IsAbstract && typeof(DbContext).IsAssignableFrom(t)
                  && t.Namespace != null &&
                  t.Namespace.Equals(dbSpace, StringComparison.OrdinalIgnoreCase))
                  .ToList();
            foreach (var context in dbContextTypes)
            {
                var connectionKey = context.Name.Replace("DbContext", "") + "Connection";
                var connectionString = configuration.GetConnectionString(connectionKey);

                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("缺少連線字符串:" + connectionKey);

                var dbContextType = context;

                //autofac註冊dbcontext
                builder.Register(c =>
                {
                    var loggerFactory = c.Resolve<ILoggerFactory>();//容器中找到Serilog 整合後的ILoggerFactory

                    var optionsBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(dbContextType);
                    var optionsBuilder = Activator.CreateInstance(optionsBuilderType) as DbContextOptionsBuilder;

                    var configureOptions = DbProviderHelper.GetDbContextOptions(connectionString);
                    configureOptions(optionsBuilder);

              

                    return Activator.CreateInstance(context,optionsBuilder.Options)!;
                })
                .As(dbContextType) //註冊為自己的型別
                .As<DbContext>() //註冊DBcontext讓BaseRepository才能注入成功
                .InstancePerLifetimeScope();
                //var method = typeof(EntityFrameworkServiceCollectionExtensions)
                //        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                //        .FirstOrDefault(m =>
                //            m.Name == "AddDbContext" &&
                //            m.IsGenericMethod &&
                //            m.GetGenericArguments().Length == 1 &&
                //            m.GetParameters().Length >= 2);

                //var genericMenthod = method.MakeGenericMethod(context);
                //var configureOptions = DbProviderHelper.GetDbContextOptions(connectionString);

                //genericMenthod.Invoke(null, new object[]
                //{
                //    services,
                //    configureOptions,
                //    ServiceLifetime.Scoped,
                //    ServiceLifetime.Scoped
                //});
                Console.WriteLine($"✅ 已註冊 DbContext：{context.Name} → 連線字串 {connectionKey}");
            }

        }
    }

}

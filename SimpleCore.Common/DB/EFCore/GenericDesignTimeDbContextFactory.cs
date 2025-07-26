using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using SimpleCore.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.DB.EFCore
{
    public class GenericDesignTimeDbContextFactory<TDbContext> : IDesignTimeDbContextFactory<TDbContext> where TDbContext : DbContext
    {
        public GenericDesignTimeDbContextFactory() { }

        public TDbContext CreateDbContext(string[] args)
        {
            string baseDir = AppContext.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\SimpleCore")); // 回到 SimpleCore 根目錄
           
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(projectRoot)  
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            var connectionKey = typeof(TDbContext).Name.Replace("DbContext", "") + "Connection";
            var connectionString = configuration.GetConnectionString(connectionKey);

            if (string.IsNullOrEmpty(connectionString))
            {
                Log.Error("連線字符串Key找不到 value: {ConnectionKey}", connectionKey);
                throw new InvalidOperationException($"連線字符串Key找不到value:{connectionKey}");
            }
               

            var optionBuilder = new DbContextOptionsBuilder<TDbContext>();

            var configureOptions = DbProviderHelper.GetDbContextOptions(connectionString);

            configureOptions(optionBuilder);//委託執行配置

            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionBuilder.Options)
              ?? throw new InvalidOperationException("無法建立 DbContext 實體");
        }

        
    }
}

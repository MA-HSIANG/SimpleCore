using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.Helpers
{
    public class AppSettingsHelper
    {
        private static IConfiguration _configuration;

        static AppSettingsHelper()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var basePath = Directory.GetCurrentDirectory();
            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json",optional:false,reloadOnChange:true)
                .AddJsonFile($"appsettings.{environment}.json",optional:false, reloadOnChange: true) 
                .AddEnvironmentVariables()
                .Build();
        } 

        public static string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name) 
                ?? throw new InvalidOperationException($"找不到連線字符串:{name}");
        }

        public static T GetSection<T>(string sectionName) where T : new()
        {
            var section = new T();
            _configuration.GetSection(sectionName).Bind(section);
            return section;
        }

        public static IConfiguration Configuration => _configuration;
    }
}

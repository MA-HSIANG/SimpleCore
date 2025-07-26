using Microsoft.Extensions.DependencyInjection;
using SimpleCore.Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Extensions.ServiceExtensions
{
    public static class ServiceCacheExtension
    {
        public static void AddCacheService(this IServiceCollection Service)
        {
            // 註冊內存緩存
            Service.AddMemoryCache(options =>
            {
                options.SizeLimit = 1024 * 1024 * 100; // 設置緩存大小限制
            });

            // 註冊分佈式內存緩存
            Service.AddDistributedMemoryCache(options =>
            {
                options.SizeLimit = 1024 * 1024 * 200; // 設置最大內存使用
            });

            Service.AddSingleton<ICaching, Caching>();
        }
    }
}

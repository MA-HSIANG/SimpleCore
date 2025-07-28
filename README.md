# SimpleCore

> 該項目參考 Blog.core 專案的架構與設計理念進行學習與擴展，該項目主旨為簡單的快速展開一個後端框架方便快速接入前端項目(MAUI、Vue3、WPF...)。 


## 套件版本
1. AutoMapper - 13.0.1
2. Microsoft.AspNetCore.Mvc.NewtonsoftJson - 8.0.0
3. Microsoft.EntityFrameworkCore 9.0.6
4. Microsoft.EntityFrameworkCore.Design - 9.0.6
5. Microsoft.EntityFrameworkCore.SqlServer - 9.0.6
6. Microsoft.EntityFrameworkCore.Tools - 9.0.6
7. Microsoft.Extensions.Configuration - 9.0.6
8. Microsoft.Extensions.Configuration.Binder - 9.0.6
9. Microsoft.Extensions.Configuration.Json - 9.0.6
10. Microsoft.Extensions.Options.ConfigurationExtensions - 9.0.6
11. Serilog.AspNetCore - 9.0.0
12. Serilog.Sinks.Async - 2.1.0
13. Autofac.Extensions.DependencyInjection - 10.0.0
14. Autofac.Extras.DynamicProxy - 7.1.0
15. Microsoft.AspNetCore.Authentication.JwtBearer - 8.0.18
16. Microsoft.Extensions.Caching.Abstractions - 9.0.6
17. Microsoft.Extensions.Caching.Memory - 9.0.6
18. Microsoft.Extensions.Caching.StackExchangeRedis - 9.0.6

## 模組
1. 封裝 IAuthService 接口實作登入服務和授權
2. 僅封裝內存緩存，可自行增加Redis
3. IDesignTimeDbContextFactory 接口配置
4. 封裝ExceptionAop、GlobalExceptionFilter 錯誤攔截詳細可自行增強邏輯
5. 內封裝RBAC和自訂義授權政策，可直接使用。

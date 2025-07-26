
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SimpleCore.Common.Filters;
using SimpleCore.Common.Helpers;
using SimpleCore.Common.HttpContext;
using SimpleCore.Common.Settings;
using SimpleCore.Extensions;
using SimpleCore.Extensions.Permission;
using SimpleCore.Extensions.ServiceExtensions;
using System.Text;

namespace SimpleCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // log
            var logPath = Path.Combine(AppContext.BaseDirectory, "Logs");
            Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Async(s=>s.File(Path.Combine("Logs", @"Log.txt"), 
                rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31))
                .CreateLogger();
            

            var builder = WebApplication.CreateBuilder(args);
            builder.Host
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder.AddDbContextsDll(builder.Configuration);

                    // 其他模組註冊
                    containerBuilder.RegisterModule<ServiceAutofacModuleRegister>();
                    containerBuilder.RegisterModule<ServiceAutofacPropertityModuleRegister>();
                });

            // Add services to the container.
            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NetCoreApi", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    //選擇類型，type選擇http時，透過swagger畫面做認證時可以省略Bearer前綴詞
                    Type = SecuritySchemeType.Http,
                    //採用Bearer token
                    Scheme = "Bearer",
                    //bearer格式使用jwt 
                    BearerFormat = "JWT",
                    //認證放在http request的header上
                    In = ParameterLocation.Header,
                    //描述
                    Description = "JWT驗證描述"
                });
                // 設定需要 Bearer Token 的 API
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            //sysUser
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ISysUser, SysUser>();
            //JWT
            var salt = AppSettingsHelper.GetSection<SaltSetting>("Salt");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = salt.Issuer,
                                    ValidAudience = salt.Audience,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(salt.Secret))
                                };
                            });

            builder.Services.AddAuthorization(options =>
            {
                //自訂授權
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));
            });
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddSingleton(new PermissionRequirement());


            //autoMapper
            builder.Services.AddAutoMapper(typeof(ServiceMappingConfig));
            ServiceMappingConfig.ServiceMappingRegistr();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            //cache
            builder.Services.AddCacheService();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

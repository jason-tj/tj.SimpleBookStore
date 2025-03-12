using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection;
using System.Text;
using tj.DbContexts.SimpleBookStore;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Services.Interface;

namespace tj.SimpleBookStore
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册服务和存储库
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServicesAndRepositories(this IServiceCollection services)
        {
            // 获取当前程序集
            var assembly = Assembly.GetExecutingAssembly();

            // 注册所有 Service
            var serviceInterfaces = assembly.GetTypes()
                .Where(t => t.IsInterface && t.FullName.StartsWith("tj.SimpleBookStore.Services"))
                .ToList();

            foreach (var serviceInterface in serviceInterfaces)
            {
                var serviceImplementation = assembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && t.Name == serviceInterface.Name.Substring(1));

                if (serviceImplementation != null)
                {
                    if (serviceInterface.Name == nameof(IJwtService))
                        services.AddSingleton(serviceInterface, serviceImplementation);
                    else
                        services.AddScoped(serviceInterface, serviceImplementation);
                }
            }

            // 注册所有 Repository
            var repositoryInterfaces = assembly.GetTypes()
                .Where(t => t.IsInterface && t.FullName.StartsWith("tj.SimpleBookStore.Repository"))
                .ToList();

            foreach (var repositoryInterface in repositoryInterfaces)
            {
                var repositoryImplementation = assembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && t.Name == repositoryInterface.Name.Substring(1));

                if (repositoryImplementation != null)
                {
                    services.AddScoped(repositoryInterface, repositoryImplementation);
                }
            }
        }

        /// <summary>
        /// 注册jwt 认证
        /// </summary>
        /// <param name="services"></param>
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };

                // 自定义 Token 提取逻辑
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // 从请求头中提取 Token（不带 Bearer 前缀）
                        var token = context.Request.Headers["Authorization"].ToString();
                        if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
                        {
                            context.Token = token.Substring("Bearer ".Length).Trim();
                        }
                        else
                        {
                            // 如果没有 Bearer 前缀，直接使用 Token
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }

        /// <summary>
        /// 注册Swagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo { Title = "tj.SimpleBookStore", Version = "V1", Description = "" });
                var folder = new DirectoryInfo(AppContext.BaseDirectory);
                var xmlFiles = folder.GetFiles("*.xml");
                foreach (var file in xmlFiles)
                {
                    c.IncludeXmlComments(file.FullName, true);
                }

                // 添加 JWT 支持
                c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = ""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "JWT"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        /// <summary>
        /// 注册OpenTelemetry
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterOpenTelemetry(this IServiceCollection services)
        {
            // 配置 OpenTelemetry
            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    tracerProviderBuilder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyService"))
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddConsoleExporter();
                })
                .WithMetrics(metricsProviderBuilder =>
                {
                    metricsProviderBuilder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyService"))
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddConsoleExporter();
                });
        }
    }
}

using Microsoft.AspNetCore.Diagnostics;
using Taboo.Enums;
using Taboo.Exceptions;
using Taboo.External_Services.Abstracts;
using Taboo.External_Services.Implements;
using Taboo.Services.Abstracts;
using Taboo.Services.Implements;

namespace Taboo
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ILanguageServices, LanguageServices>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IWordService, WordService>();
            //services.AddScoped<ICacheService,RedisService>();
            return services;
        }
        public static IServiceCollection AddCacheService
            (this IServiceCollection services,IConfiguration _conf ,CacheTypes type=CacheTypes.Redis)
        {
            if(type==CacheTypes.Redis)
            {
                services.AddStackExchangeRedisCache(opt =>
                {
                    opt.Configuration =
                      _conf.GetConnectionString("Redis");
                    opt.InstanceName = "Taboo_";
                });
                services.AddScoped<ICacheService,RedisService>();
            }
            else
            {
                services.AddMemoryCache();
                services.AddScoped<ICacheService, localcacheservice>();
            }
            return services;
        }
        public static IApplicationBuilder UseExpectionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(handler =>
            {
         handler.Run(async context =>
                {
                    var feature =
                    context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    Exception exc = feature!.Error;
                    if (exc is IBaseException ibe)
                    {
                        context.Response.StatusCode = ibe.StatusCode;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = ibe.StatusCode,
                            Message = ibe.ErrorMessage
                        });
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "Bir xeta Bash verdi!"
                        });
                    }
                });
               
            });
            return app;
        }
    }
}

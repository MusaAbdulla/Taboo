﻿using Taboo.Services.Abstracts;
using Taboo.Services.Implements;

namespace Taboo
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ILanguageServices, LanguageServices>();
            return services;
        }
    }
}

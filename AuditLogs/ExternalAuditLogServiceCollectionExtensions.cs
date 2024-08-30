using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSF.Infrastructure.Abstractions.AuditLogs;

namespace SSF.EFCore.AuditLogs
{
    public static class ExternalAuditLogServiceCollectionExtensions
    {
        public static IServiceCollection AddZaminHamsterChageDatalog(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExternalAuditLogOptions>(configuration);
            return services;
        }

        public static IServiceCollection AddZaminHamsterChageDatalog(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            services.AddZaminHamsterChageDatalog(configuration.GetSection(sectionName));
            return services;
        }

        public static IServiceCollection AddZaminHamsterChageDatalog(this IServiceCollection services, Action<ExternalAuditLogOptions> setupAction)
        {
            services.Configure(setupAction);
            return services;
        }



        public static IServiceCollection AddServiceExternalAuditLog(this IServiceCollection services,Action<ExternalAuditLogSqlOptions> options)

        {
            services.Configure<ExternalAuditLogSqlOptions>(options);
            services.AddSingleton<IExternalAuditLogRepository,DapperExternalAuditLogRepository>();  
            return services;
        }
    }

}

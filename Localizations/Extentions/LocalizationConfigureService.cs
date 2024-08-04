using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace SSF.Extensions.Implementations.Localizations.Extentions;

public static class LocalizationConfigureService
{
    public static void AddSSFLocalizationJson(this IServiceCollection services)
    {
        services.AddLocalization();
        services.AddTransient<IStringLocalizer, JsonStringLocalizer>();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

    }

    public static IApplicationBuilder UseSSFLocalization(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LocalizationMiddleware>();
    }
}

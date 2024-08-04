using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace SSF.Extensions.Implementations.Localizations;

public class JsonStringLocalizerFactory : IStringLocalizerFactory
{

    private ConcurrentDictionary<string, JsonStringLocalizer> _cache = new ConcurrentDictionary<string, JsonStringLocalizer>();

    private readonly IServiceProvider _serviceProvider;

    //   private readonly IFileProvider _virtualFileProvider;
    private readonly ILogger<JsonStringLocalizer> _logger;
    //public JsonStringLocalizerFactory(ILogger<JsonStringLocalizer> logger)
    //{
    //    _logger = logger;

    //}
    public IStringLocalizer Create(Type resourceType)
    {

        JsonResourceManager? ss = new JsonResourceManager();
        var d = _cache.GetOrAdd(resourceType.Name, _ =>
        {

            ss.LoadJsonResources(resourceType);

            return new JsonStringLocalizer(ss);
        });
        return d;
        //  return new JsonStringLocalizer(resourceSource);
        //var d=  _serviceProvider.GetRequiredService<IStringLocalizer>();
        //  return d;
        //var resourceType = resourceSource.GetTypeInfo();

        //var cultureInfo = CultureInfo.CurrentUICulture;
        //var resourceName = $"{resourceType.FullName}/{cultureInfo}.json";

        //Assembly assembly = Assembly.GetExecutingAssembly();

        //var d = assembly.GetManifestResourceNames();
        //System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("fa-IR");
        //string filePath = $"Resources/{resourceType.Name}/{Thread.CurrentThread.CurrentCulture.Name}.json";

    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer();
    }

    //private IStringLocalizer GetCachedLocalizer(string resourceName, Assembly assembly, CultureInfo cultureInfo)
    //{
    //    string cacheKey = GetCacheKey(resourceName, assembly, cultureInfo);
    //    return _cache.GetOrAdd(cacheKey, new JsonStringLocalizer(resourceName, assembly, cultureInfo, _loggerFactory.CreateLogger<JsonStringLocalizer>()));
    //}

    //private string GetCacheKey(string resourceName, Assembly assembly, CultureInfo cultureInfo)
    //{
    //    return resourceName + ';' + assembly.FullName + ';' + cultureInfo.Name;
    //}
}
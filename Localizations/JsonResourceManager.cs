//using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using SSF.Extensions.Implementations.DependencyInjections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;

namespace SSF.Extensions.Implementations.Localizations;
public class JsonResourceManager
{

    private readonly ILogger<JsonStringLocalizer> _logger;
    //public JsonResourceManager(ILogger<JsonStringLocalizer> logger )
    //{
    //    this._logger = logger;
    //}
    // private ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _resourcesCache = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
    private ConcurrentDictionary<string, string> _resourcesCache = new ConcurrentDictionary<string, string>();

    private readonly JsonDocumentOptions _jsonDocumentOptions = new JsonDocumentOptions
    {
        CommentHandling=JsonCommentHandling.Skip,
        AllowTrailingCommas=true,
    };
    public virtual string GetString(string name)
    {
        return _resourcesCache.TryGetValue(name, out string value) ? value : null;
    }

    public void LoadJsonResources(Type resourceType)
    {
        //        IFileProvider fileProvider = new ManifestEmbeddedFileProvider(Assembly.GetExecutingAssembly());
        //IFileInfo fileInfo = fileProvider.GetFileInfo(path);
        var assemblies = ExtentionAssemblies.GetAssemblies("SSF");
        var rtype = assemblies.SelectMany(x => x.GetTypes()).Where(x => resourceType.IsAssignableFrom(x)&&!x.IsInterface&&!x.IsAbstract).FirstOrDefault();

        var assembly = rtype.Assembly;

        var cultureInfo = CultureInfo.CurrentUICulture;
        var resourceName = $"{rtype.FullName}.{cultureInfo}.json";
        var fff = rtype.Namespace;
        //IDP.Core.Domain.Shared.Resources.IDPResource.en-US.json
        //   Assembly assembly = resourceType.Assembly;
        //  string filePath = $"{assembly.GetName().Name}.Resources.{resourceType.Name}.{cultureInfo.Name}.json";

        string filePath = $"{rtype.Namespace}.Resources.{cultureInfo.TwoLetterISOLanguageName}.json";

        var filestream = assembly.GetManifestResourceStream(filePath);
        //      using (var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        var resources = new Dictionary<string, string>();
        using (var sReader = new StreamReader(filestream))
        {
            using var document = JsonDocument.Parse(sReader.BaseStream, _jsonDocumentOptions);
            resources=document.RootElement.EnumerateObject().ToDictionary(e => e.Name, e => e.Value.ToString());
            foreach (var resource in resources)
            {
                _resourcesCache.TryAdd(resource.Key, resource.Value);
            }
        }

        //using (var reader = new StreamReader(filestream))
        //{
        //    json = reader.ReadToEnd();
        //}

        //   _cache = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(json) ?? throw new InvalidOperationException($"Failed to parse JSON: '{json}'");

    }
    public void LoadJsonResources3(Type resourceType)

    {
        var assemblies = ExtentionAssemblies.GetAssemblies("Mit");

        var resources = new Dictionary<string, string>();
        var rtype = assemblies.SelectMany(x => x.GetTypes()).Where(x => resourceType.IsAssignableFrom(x)&&!x.IsInterface&&!x.IsAbstract).FirstOrDefault();
        var cultureInfo = CultureInfo.CurrentUICulture;
        var assembly = rtype.Assembly;

        string filePath1 = $"Localization\\Resources\\{cultureInfo.TwoLetterISOLanguageName}.json";
        string filePath = Path.Combine(PathHelpers.GetApplicationRoot(), filePath1);
        using (var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (var sReader = new StreamReader(str))
        {
            using var document = JsonDocument.Parse(sReader.BaseStream, _jsonDocumentOptions);
            resources=document.RootElement.EnumerateObject().ToDictionary(e => e.Name, e => e.Value.ToString());
            foreach (var resource in resources)
            {
                _resourcesCache.TryAdd(resource.Key, resource.Value);
            }
        }
    }
}
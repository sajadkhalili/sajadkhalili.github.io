using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace SSF.Extensions.Implementations.Localizations;
public class JsonStringLocalizer : IStringLocalizer
{

    //public JsonStringLocalizer(ILogger<JsonStringLocalizer> logger)
    //{
    //    this._logger = logger;
    //}
    public JsonStringLocalizer()
    {

    }
    public JsonStringLocalizer(JsonResourceManager jsonResourceManager/*, ILogger<JsonStringLocalizer> logger*/)
    {
        // this._logger = logger;
        this.jsonResourceManager=jsonResourceManager;
    }

    private readonly JsonResourceManager jsonResourceManager;
    private readonly ILogger<JsonStringLocalizer> _logger;

    public LocalizedString this[string name]
    {
        get
        {
            string value = GetString(name);
            return new LocalizedString(name, value??name, value==null);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var actualValue = this[name];
            return !actualValue.ResourceNotFound
                ? new LocalizedString(name, string.Format(actualValue.Value, arguments), false)
                : actualValue;
        }
    }

    //public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    //{

    //    var cultureInfo = CultureInfo.CurrentUICulture;
    //    var resourceName = $"{_resourceType.FullName}/{cultureInfo}.json";

    //    Assembly assembly = _resourceType.Assembly;
    //    string filePath = $"Resources/{_resourceType.Name}/{cultureInfo.Name}.json";

    //    using (var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
    //    using (var sReader = new StreamReader(str))
    //    using (var reader = new JsonTextReader(sReader))
    //    {
    //        while (reader.Read())
    //        {
    //            if (reader.TokenType != JsonToken.PropertyName)
    //                continue;
    //            string key = (string)reader.Value;
    //            reader.Read();
    //            string value = _serializer.Deserialize<string>(reader);
    //            yield return new LocalizedString(key, value, false);
    //        }
    //    }
    //}

    private string GetString(string key)
    {

        return jsonResourceManager.GetString(key);

    }

    private void LoadData()
    {

    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }
}

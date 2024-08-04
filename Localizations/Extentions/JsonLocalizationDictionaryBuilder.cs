namespace SSF.Extensions.Implementations.Localizations.Extentions;

//public static class JsonLocalizationDictionaryBuilder
//{

//    /// <summary>
//    ///     Builds an <see cref="JsonLocalizationDictionaryBuilder" /> from given file.
//    /// </summary>
//    /// <param name="filePath">Path of the file</param>
//    public static ILocalizationDictionary BuildFromFile(string filePath)
//    {
//        try
//        {
//            var res = File.ReadAllText(filePath);
//            return BuildFromJsonString(res);
//        }
//        catch (Exception ex)
//        {
//            throw new Exception("Invalid localization file format: " + filePath, ex);
//        }
//    }

//    private static readonly JsonSerializerOptions DeserializeOptions = new JsonSerializerOptions
//    {
//        PropertyNameCaseInsensitive = true,
//        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
//        ReadCommentHandling = JsonCommentHandling.Skip,
//        AllowTrailingCommas = true
//    };

//    /// <summary>
//    ///     Builds an <see cref="JsonLocalizationDictionaryBuilder" /> from given json string.
//    /// </summary>
//    /// <param name="jsonString">Json string</param>
//    public static ILocalizationDictionary BuildFromJsonString(string jsonString)
//    {
//        JsonLocalizationFile jsonFile;
//        try
//        {
//            jsonFile = JsonSerializer.Deserialize<JsonLocalizationFile>(jsonString, DeserializeOptions);
//        }
//        catch (JsonException ex)
//        {
//            throw new Exception("Can not parse json string. " + ex.Message);
//        }

//        var cultureCode = jsonFile.Culture;
//        if (string.IsNullOrEmpty(cultureCode))
//        {
//            throw new Exception("Culture is empty in language json file.");
//        }

//        var dictionary = new Dictionary<string, LocalizedString>();
//        var dublicateNames = new List<string>();
//        foreach (var item in jsonFile.Texts)
//        {
//            if (string.IsNullOrEmpty(item.Key))
//            {
//                throw new Exception("The key is empty in given json string.");
//            }

//            if (dictionary.GetOrDefault(item.Key) != null)
//            {
//                dublicateNames.Add(item.Key);
//            }

//            dictionary[item.Key] = new LocalizedString(item.Key, item.Value.NormalizeLineEndings());
//        }

//        if (dublicateNames.Count > 0)
//        {
//            throw new Exception(
//                "A dictionary can not contain same key twice. There are some duplicated names: " +
//                dublicateNames.JoinAsString(", "));
//        }

//        return new StaticLocalizationDictionary(cultureCode, dictionary);
//    }
//}
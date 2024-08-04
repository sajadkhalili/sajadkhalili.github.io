//using Microsoft.Extensions.Caching.Distributed;
using System.Reflection;

namespace SSF.Extensions.Implementations.Localizations;

public static class PathHelpers
{
    public static string GetApplicationRoot()
        => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
}

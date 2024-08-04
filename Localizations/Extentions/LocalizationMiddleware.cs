using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace SSF.Extensions.Implementations.Localizations.Extentions;
//public class VirtualFileProvider : IFileProvider
//{

//  public IDirectoryContents GetDirectoryContents(string subpath)
//    {
//        IFileInfo? directory = GetFileInfo(subpath);
//        if (!directory.IsDirectory)
//        {
//            return NotFoundDirectoryContents.Singleton;
//        }
//        var fileList = new List<IFileInfo>();

//        var directoryPath = subpath.EnsureEndsWith('/');
//        foreach (var fileInfo in Files.Values)
//        {
//            var fullPath = fileInfo.GetVirtualOrPhysicalPathOrNull();
//            if (!fullPath.StartsWith(directoryPath))
//            {
//                continue;
//            }

//            var relativePath = fullPath.Substring(directoryPath.Length);
//            if (relativePath.Contains("/"))
//            {
//                continue;
//            }

//            fileList.Add(fileInfo);
//        }

//        return new EnumerableDirectoryContents(fileList);
//    }

//    public IFileInfo GetFileInfo(string subpath)
//    {
//        throw new NotImplementedException();
//    }

//    public IChangeToken Watch(string filter)
//    {
//        throw new NotImplementedException();
//    }
//}
public class LocalizationMiddleware : IMiddleware
{
    public LocalizationMiddleware()
    {

    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cultureKey = context.Request.Headers["Accept-Language"];
        if (!string.IsNullOrEmpty(cultureKey))
        {
            if (DoesCultureExist(cultureKey))
            {
                var culture = new CultureInfo(cultureKey);
                Thread.CurrentThread.CurrentCulture=culture;
                Thread.CurrentThread.CurrentUICulture=culture;
            }
        }

        await next(context);
    }
    private bool DoesCultureExist(string cultureName)
    {
        return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => string.Equals(culture.Name, cultureName, StringComparison.CurrentCultureIgnoreCase));
    }
}
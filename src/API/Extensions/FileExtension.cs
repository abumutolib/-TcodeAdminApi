using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace API.Extensions
{
    public static class FileExtension
    {
        public static IApplicationBuilder UseCustomStaticFile(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(config.GetValue<string>("RootFolderFiles")),
                RequestPath = new PathString("/RootPathFiles")
            });
            return app;
        }
    }
}

using System.IO;
using Application.Common.Interfaces;

namespace API.Services
{
    public class PathProvider : IPathProvider
    {
        public string MapPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            //TODO: get host name from appsettings config
            string host = "https://localhost:44307/";
            string filePath = Path.Combine(host, "RootPathFiles", path);
            return filePath;
        }
    }
}

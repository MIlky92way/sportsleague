using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CinemaStore.Infrastructure
{
    public sealed class DirectoryTools
    {
        public static void CheckDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void DeleteFile(Func<string, string> map, string filename, string relPath = null)
        {
            try
            {
                string path = map(relPath + filename);
                File.Delete(path);
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
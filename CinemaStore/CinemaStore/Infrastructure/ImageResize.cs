using ImageResizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CinemaStore.Infrastructure
{
    public static class ImageResize
    {
        public static string Resize(HttpPostedFileBase file, string directory, int width, int height, bool crop = false)
        {
            List<string> resultPaths = new List<string>();
            var fileGuid = Guid.NewGuid().ToString();
            string mode = crop ? "crop" : "max";
            StringBuilder sb = new StringBuilder();

            sb.Append("~");
            sb.Append(directory);
            sb.Append(fileGuid);
            sb.Append(".<ext>");
            
            var image = new ImageJob(file, sb.ToString(), new Instructions($"maxwidth={width}&maxheight={height}&mode={mode}"));
            image.CreateParentDirectory = true;
            image.Build();

            var path = fileGuid + ImageResizer.Util.PathUtils.GetExtension(image.FinalPath);

            return path;
        }
    }
}
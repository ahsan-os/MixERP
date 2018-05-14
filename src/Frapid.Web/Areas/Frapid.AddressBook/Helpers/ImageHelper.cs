using System;
using System.IO;
using System.Net;

namespace Frapid.AddressBook.Helpers
{
    public static class ImageHelper
    {
        public static void SaveBase64Image(string path, string contents)
        {
            try
            {
                if (contents.StartsWith("data:image") && contents.Contains(","))
                {
                    contents = contents.Split(',')[1];
                }

                var bytes = Convert.FromBase64String(contents);
                File.WriteAllBytes(path, bytes);
            }
            catch
            {
                //Swallow
            }
        }

        public static void DownloadImageFromUrl(string path, string url)
        {
            try
            {
                var client = new WebClient();
                client.DownloadFile(url, path);
            }
            catch
            {
                //Swallow
            }
        }

    }
}
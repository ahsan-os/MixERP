using System.Web.Mvc;
using Frapid.Areas.Drawing;
using Frapid.Configuration;

namespace Frapid.Areas.Conventions.Attachments
{
    public sealed class Deserializer
    {
        public Deserializer(string tenant, AreaRegistration area, int width, int height, string path)
        {
            this.Tenant = tenant;
            this.Width = width;
            this.Height = height;
            this.Path = path;

            this.ImagePath = PathMapper.MapPath($"/Tenants/{tenant}/Areas/{area.AreaName}/attachments/" + path);
        }

        public string Tenant { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Path { get; set; }
        private string ImagePath { get; }

        public byte[] Get()
        {
            var bmp = BitmapHelper.ResizeCropExcess(this.ImagePath, this.Width, this.Height);
            return bmp;
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Frapid.Areas.Drawing
{
    public static class BitmapHelper
    {
        public static Image Resize(this Image img, int x, int y, int sourceWidth, int sourceHeight, int destinationWidth, int destinationHeight)
        {
            var bmp = new Bitmap(destinationWidth, destinationHeight);
            {
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        var destRect = new Rectangle(0, 0, destinationWidth, destinationHeight);
                        graphics.DrawImage(img, destRect, x, y, sourceWidth, sourceHeight, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                return bmp;
            }
        }

        public static Image ResizeProportional(this Image img, int width, int height, bool enlarge = false)
        {
            double ratio = Math.Max(img.Width/(double) width, img.Height/(double) height);

            if (ratio < 1 && !enlarge)
            {
                return img;
            }

            return img.Resize(0, 0, img.Width, img.Height, (int) Math.Round(img.Width/ratio), (int) Math.Round(img.Height/ratio));
        }

        public static byte[] ResizeCropExcess(string path, int destinationWidth = 0, int destinationHeight = 0)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            using (var img = new Bitmap(path))
            {
                if (destinationWidth == 0)
                {
                    destinationWidth = img.Width;
                }
                if (destinationHeight == 0)
                {
                    destinationHeight = img.Height;
                }

                double sourceRatio = img.Width/(double) img.Height;
                double destinationRatio = destinationWidth/(double) destinationHeight;
                int x, y, croppedWidth, croppedHeight;

                if (sourceRatio < destinationRatio) // trim top and bottom
                {
                    croppedHeight = destinationHeight*img.Width/destinationWidth;
                    y = (img.Height - croppedHeight)/2;
                    croppedWidth = img.Width;
                    x = 0;
                }
                else // trim left and right
                {
                    croppedWidth = destinationWidth*img.Height/destinationHeight;
                    x = (img.Width - croppedWidth)/2;
                    croppedHeight = img.Height;
                    y = 0;
                }

                using (var stream = new MemoryStream())
                {
                    var image = Resize(img, x, y, croppedWidth, croppedHeight, destinationWidth, destinationHeight);
                    image.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PhotoUploader.Data
{
    public class Photo
    {
        public int Id { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public string FileExtention { get; set; }

        public int FileSize { get; set; }

        public byte[] Content { get; set; }

        public DateTime UploadDate { get; set; }
                
        public void WriteThumbnail(Stream stream)
        {
            using (var image = Resize(Content, 120, 120))
                image.Save(stream, ImageFormat.Jpeg);
        }

        private Image Resize(byte[] bytes, int height, int width)
        {
            var image = new Bitmap(new MemoryStream(bytes));
            double num = (double)height * 100 / (double)image.Height;
            double width1 = (double)width * 100 / (double)image.Width;
            if (num <= width1)
            {
                if (num < width1)
                    width = (int)Math.Round(num * (double)image.Width / 100);
            }
            else
            {
                height = (int)Math.Round(width1 * (double)image.Height / 100);
            }
            if (image.Height != height || image.Width != width)
            {
                var bmp = new System.Drawing.Bitmap(width, height);
                bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                var graphic = System.Drawing.Graphics.FromImage(bmp);
                using (graphic)
                {
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(image, 0, 0, width, height);
                }
                return bmp;
            }
            else
                return image;
        }


    }
}

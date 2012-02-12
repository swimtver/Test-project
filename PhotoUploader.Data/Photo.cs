using System;
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
    }
}

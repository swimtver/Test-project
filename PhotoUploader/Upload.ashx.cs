using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using PhotoUploader.Data;

namespace PhotoUploader
{
    public class Upload : IHttpHandler
    {
        private readonly JavaScriptSerializer js = new JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            string jsonObj = null;
            try
            {
                string fileName = null;
                int fileLength = 0;
                Stream stream = null;
                if (context.Request.Files.Count > 0)
                {
                    var file = context.Request.Files["qqfile"];
                    if (file != null)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        fileLength = file.ContentLength;
                        stream = file.InputStream;
                    }
                }
                else
                {
                    fileName = context.Request.QueryString["qqfile"] ?? context.Request.Headers["X-File-Name"];
                    int.TryParse(context.Request.Headers["Content-Length"], out fileLength);
                    stream = context.Request.InputStream;
                }

                if (stream != null && stream.Length > 0)
                {
                    var fileExtension = Path.GetExtension(fileName).Trim();
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).Trim();
                    if (String.IsNullOrWhiteSpace(fileName)) fileName = "noname";

                    var fileBytes = new byte[fileLength];
                    stream.Read(fileBytes, 0, fileLength);

                    var photo = new Photo
                    {
                        FileName = fileNameWithoutExtension,
                        FileExtention = fileExtension,
                        ContentType = "image/jpeg",
                        FileSize = fileLength,
                        Original = new Original { Content = fileBytes },
                        UploadDate = DateTime.Now
                    };
                    var service = new PhotoService();
                    var id = service.Upload(photo).Id;
                    context.Response.ContentType = "text/html";
                    jsonObj = js.Serialize(new { success = true, Id = id });
                }
            }
            catch (Exception ex)
            {
                jsonObj = js.Serialize(new { success = false, error = "Ошибка при передаче файла. " + ex.Message });
            }
            context.Response.Write(jsonObj);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
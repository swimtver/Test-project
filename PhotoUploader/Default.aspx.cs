using System;
using System.Linq;
using PhotoUploader.Data;
using System.IO;

namespace PhotoUploader
{
    public partial class Default : System.Web.UI.Page
    {
        PhotoService service = new PhotoService();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _resultLabel.Text = "Всего: " + service.Count();                
            }
            catch (Exception ex)
            {
                _resultLabel.Text = ex.Message;
            }
        }

        public void SubmitClick(object sender, EventArgs e)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName).Trim();
                    if (String.IsNullOrWhiteSpace(fileName)) fileName = "noname";

                    var fileBytes = new byte[file.ContentLength];
                    if (file.InputStream != null) file.InputStream.Read(fileBytes, 0, file.ContentLength);

                    var photo = new Photo
                    {
                        FileName = fileName,
                        FileExtention = Path.GetExtension(file.FileName).Trim(),
                        ContentType = file.ContentType,
                        FileSize = file.ContentLength,
                        Content = fileBytes,
                        UploadDate = DateTime.Now
                    };
                    service.Upload(photo);
                }
            }            
            _resultLabel.Text = "Всего: " + service.Count();
        }
    }
}

using System;
using System.Linq;
using PhotoUploader.Data;
using System.IO;
using System.Web.Helpers;

namespace PhotoUploader
{
    public partial class Default : System.Web.UI.Page
    {
        PhotoService service = new PhotoService();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var count = service.GetPhotoesList().Count();
                _resultLabel.Text = "I get " + count + " photoes!!!!";
                if (count > 0)
                {
                    _photoesList.DataSource = service.GetPhotoesList();
                    _photoesList.DataBind();
                }
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
                    var fileUpload = new WebImage(file.InputStream);
                    var fileTitle = Path.GetFileNameWithoutExtension(file.FileName).Trim();
                    if (String.IsNullOrWhiteSpace(fileTitle))
                        fileTitle = "Untitled";
                    var fileExtension = Path.GetExtension(file.FileName).Trim();
                    var fileBytes = fileUpload.GetBytes();

                    var photo = new PhotoUploader.Data.Photo
                    {
                        FileName = fileTitle,
                        FileExtention = fileExtension,
                        ContentType = fileUpload.ImageFormat,
                        FileSize = fileBytes.Length,
                        Content = fileBytes,
                        UploadDate = DateTime.Now
                    };
                    service.Upload(photo);
                }
            }
            _photoesList.DataSource = service.GetPhotoesList();
            _photoesList.DataBind();
        }
    }
}

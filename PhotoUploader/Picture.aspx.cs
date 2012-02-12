using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using PhotoUploader.Data;
using System.Web;

namespace PhotoUploader
{
    public partial class Picture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = 0;
            string size = "full";
            if (Request.Params["id"] != null)
                int.TryParse(Request.Params["id"], out id);
            if (Request.Params["size"] != null)
                size = Request.Params["size"];

            var service = new PhotoService();

            var photo = service.GetById(id);
            if (photo == null)
            {
                Response.StatusCode = 400; // Bad Request
                Response.End();
                return;
            }

            OutputCache(Response.Cache, 60);
            var image = new WebImage(photo.Content);

            if (!size.Equals("full"))
                image.Resize(width: 150, height: 150);
            image.Write();

        }

        private void OutputCache(HttpCachePolicy cache, int numberOfSeconds)
        {
            DateTime timestamp = HttpContext.Current.Timestamp;
            var sliding = false;

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(timestamp.AddSeconds((double)numberOfSeconds));
            cache.SetMaxAge(new TimeSpan(0, 0, numberOfSeconds));
            cache.SetValidUntilExpires(true);
            cache.SetLastModified(timestamp);
            cache.SetSlidingExpiration(sliding);
        }
    }
}
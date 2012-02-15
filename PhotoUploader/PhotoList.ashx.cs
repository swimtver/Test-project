using System;
using System.Linq;
using System.Web;
using PhotoUploader.Data;

namespace PhotoUploader
{
    /// <summary>
    /// Summary description for PhotoList
    /// </summary>
    public class PhotoList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Id"] == null)
            {
                context.Response.End();
                return;
            }
            var count = 6;
            int id;
            int.TryParse(context.Request.QueryString["Id"], out id);
            if (id == 0) //Если вызывается первый раз (при загрузке формы)
            {
                id = int.MaxValue;
            }

            var service = new PhotoService();
            var photoList = service.GetPhotoesList(id, count).ToList();
            //var photoList = service.GetFakePhotoesList().ToList();
            for (int i = 0; i < photoList.Count; i++)
            {
                context.Response.Write("<li");
                if (i == photoList.Count - 1)
                {
                    context.Response.Write(" customId=\"" + photoList[i].Id + "\" class=\"waypoint\"");
                }
                context.Response.Write("><div class=\"picture\"><a href=\"Picture.aspx?id=");
                context.Response.Write(photoList[i].Id.ToString());
                context.Response.Write("\" target=\"_blank\">");
                context.Response.Write("<img src=\"Thumbnail.aspx?id=");
                context.Response.Write(photoList[i].Id.ToString());
                context.Response.Write("\" alt=\"");
                context.Response.Write(photoList[i].FileName);
                context.Response.Write("\">");
                context.Response.Write("</a></li>");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
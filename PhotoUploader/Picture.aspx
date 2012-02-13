﻿<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = -1;
        if (Request.Params["id"] != null)
            int.TryParse(Request.Params["id"], out id);

        var service = new PhotoUploader.Data.PhotoService();
        var photo = service.GetById(id);
        if (photo != null)
        {
            OutputCache(Response.Cache, 60);
            if (!photo.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                photo.ContentType = string.Concat("image/", photo.ContentType);

            Response.ContentType = photo.ContentType;
            Response.BinaryWrite(photo.Content);
        }
        else
            Response.End();
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
</script>

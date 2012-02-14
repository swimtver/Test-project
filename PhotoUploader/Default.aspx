<%@ Page Title="Home Page" Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        var service = new PhotoUploader.Data.PhotoService();
        try
        {
            _resultLabel.Text = service.GetPhotoesList().Count().ToString();
        }
        catch (Exception ex)
        {
            _resultLabel.Text = ex.Message;
        }
    }
</script>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/waypoints.min.js" type="text/javascript"></script>
    <script src="Scripts/fileuploader.js" type="text/javascript"></script>
    <script>
        $(function () {
            GetPhotos(-1);
            createUploader();
        });
        function GetPhotos(id) {
            $.get("/PhotoList.ashx", { Id: id },
            function (data) {
                $('#photolist').append(data);
                $(".waypoint").waypoint(function (event, direction) {
                    var id = $('.waypoint').attr('customId');
                    $('.waypoint').removeClass('waypoint');
                    GetPhotos(id);
                },
                {
                    triggerOnce: true,
                    context: $("#photolist"),
                    offset: '100%'
                });
            });
        }

        function SetWaypoints() {
            $(".waypoint").waypoint(function (event, direction) {

                GetPhotos();
                $('.waypoint').removeClass('.waypoint');
            },
            {
                triggerOnce: true,
                context: $("#photolist"),
                offset: '100%'
            });

        }
        function createUploader() {
            var uploader = new qq.FileUploader(
            {
                element: document.getElementById('file-uploader'),
                action: '/Upload.ashx',
                allowedExtensions: ['jpg', 'jpeg'],
                onSubmit: function (id, fileName) {
                    var html = '<li id=' + id + '><div class="picture">' +
                    '<img class="loading" src="Styles/Images/loading.gif" alt="Идет загрузка..." />' +
                    '<div style="float:left;">' + fileName + '</div></div></li>';
                    $('#photolist').prepend(html);
                },
                onComplete: function (id, fileName, responseJSON) {
                    var html = '<div class="empty"/>';
                    if (responseJSON) {
                        if (responseJSON.success == true) {
                            html = '<a href="Picture.aspx?id=' + responseJSON.Id + '" target="_blank">' +
                                    '<img src="Thumbnail.aspx?id=' + responseJSON.Id + '"/></a>';
                            var count = $('#_resultLabel').text();
                            count++;
                            $('#_resultLabel').text(count);
                        }
                        else {
                            html = '<span class="error">' + responseJSON.error + '</span>';
                        }
                    }
                    $('li[id=' + id + ']>div').html(html);
                }
            });
        }
</script>
    <link rel="stylesheet" type="text/css" href="//webputty.commondatastorage.googleapis.com/agtzfmNzc2ZpZGRsZXIMCxIEUGFnZRjFljcM.css" />
    <script type="text/javascript">
        (function (w, d){ if (w.location != w.parent.location || w.location.search.indexOf('__preview_css__') > -1){ var t = d.createElement('script'); t.type = 'text/javascript'; t.async = true; t.src = 'http://www.webputty.net/js/agtzfmNzc2ZpZGRsZXIMCxIEUGFnZRjFljcM'; (d.body || d.documentElement).appendChild(t); } })(window, document);
</script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    Загрузка Фотографий
                </h1>
            </div>
            <div class="clear hideSkiplink">
            </div>
        </div>
        <div class="main">
            <div id="file-uploader">
            </div>
            <p>
                Всего:
                <asp:Label ID="_resultLabel" runat="server" /></p>
            <ul id="photolist">
            </ul>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
    </div>
    </form>
</body>
</html>

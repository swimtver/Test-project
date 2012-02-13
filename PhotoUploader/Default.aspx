<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="PhotoUploader.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/waypoints.min.js" type="text/javascript"></script>
    <script>
        $(function () {
            GetPhotos(-1);
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
            <asp:FileUpload ID="_upload" runat="server" />
            <asp:Button ID="_submit" runat="server" Text="Загрузить" OnClick="SubmitClick" />
            <p>
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

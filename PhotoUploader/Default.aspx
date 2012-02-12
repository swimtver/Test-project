<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PhotoUploader.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!</h2>
    <asp:FileUpload ID="_upload" runat="server" />
    <asp:Button ID="_submit" runat="server" Text="Загрузить" OnClick="SubmitClick" />
    <asp:Label ID="_resultLabel" runat="server" />
    <div>
        <asp:ListView ID="_photoesList" runat="server">
            <ItemTemplate>
                <asp:HyperLink runat="server" NavigateUrl='<%# ResolveUrl("Picture.aspx?id="+Eval("Id")) %>'
                    ImageUrl='<%# ResolveUrl("Picture.aspx?size=small&id="+Eval("Id")) %>' Target="_blank">
                <asp:Label runat="server"><%#Eval("FileName") %></asp:Label>
                </asp:HyperLink>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

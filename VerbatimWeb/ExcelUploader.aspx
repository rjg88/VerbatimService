<%@ Page Title="Upload Deck" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ExcelUploader.aspx.cs" Inherits="VerbatimWeb.ExcelUploader" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Upload a CSV!</h1>
    </div>
    <asp:FileUpload accept=".csv*" ID="FileUploadCSV" runat="server" />
    <asp:Button CssClass="btn btn-success" Text="Upload" runat="server" onclick="ButtonUpload_Click" />

</asp:Content>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VerbatimWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Search for a Deck</h1>
    </div>

	<asp:textbox ID="SearchInputBox" runat="server"></asp:textbox>
	<asp:Button class="btn btn-info" type="button" ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" Text="Search" />  


</asp:Content>

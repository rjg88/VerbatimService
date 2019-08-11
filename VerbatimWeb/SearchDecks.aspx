<%@ Page Title="Search for a Deck" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SearchDecks.aspx.cs" Inherits="VerbatimWeb.SearchDecks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Search for a Deck</h1>
    </div>

	<asp:textbox ID="SearchInputBox" runat="server"></asp:textbox>
	<asp:Button class="btn btn-info" type="button" ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" Text="Search" />  


</asp:Content>

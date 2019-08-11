<%@ Page Title="Search for a Deck" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VerbatimWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Welcome to Verbatim Deck Managment!</h1>
    <h2>Please search for your deck to edit it, or, create a new one!</h2>
    <a href="SearchDecks" class="btn btn-info mainbtn" role="button">
        <h1>Search for a Deck</h1>
    </a>
    <a href="CreateDeck" class="btn btn-success mainbtn" role="button">
        <h1>Create a Deck</h1>
    </a>

</asp:Content>

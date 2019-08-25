<%@ Page Title="Delete Deck" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="DeleteDeck.aspx.cs" Inherits="VerbatimWeb.DeleteDeck" validateRequest="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Are  you sure you want to delete this deck?</h1>
        <h2>All of the cards and the deck will be gone forever.</h2>
        <h2>If so, type the name of the deck.</h2>
    </div>
    <div>
        <asp:Label>Name:</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxDeckName"></asp:TextBox>
        <br />
<%--        <asp:Label>Password:</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxPassword" TextMode="Password"></asp:TextBox>
        <br />--%>
        <asp:Button class="btn btn-danger" type="button" ID="ButtonDeleteDeck" runat="server" onclick="ButtonDelete_Click" Text="Delete" />  

    </div>
</asp:Content>
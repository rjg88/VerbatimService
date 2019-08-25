<%@ Page Title="Search for a Deck" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VerbatimWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Welcome to Verbatim Deck Management!</h1>
    <table>
        <tr>
            <td>
                <asp:label runat="server" ID="PleaseLoginText" Font-Size="XX-Large">Please log in with steam to Edit or Create a Deck!&nbsp;</asp:label>
            </td>
            <td>
                <asp:ImageButton ID="SteamLoginButton" runat="server" onclick="SteamLogin" ImageUrl="https://steamcdn-a.akamaihd.net/steamcommunity/public/images/steamworks_docs/english/sits_large_border.png"   ></asp:ImageButton>
            </td>
        </tr>
    </table>
    <asp:button runat="server" OnClick="SearchDecksRedirect" id="SearchDecksButton" class="btn btn-info mainbtn" Font-Size="XX-Large" role="button" Text="Search Decks"></asp:button>

    <asp:button runat="server" OnClick="CreateDeckRedirect" id="CreateDeckButton" class="btn btn-success mainbtn" Font-Size="XX-Large" role="button" Text="Create a Deck"></asp:button>

    <asp:button runat="server" OnClick="MyDecksRedirect" id="MyDecksButton" class="btn btn-primary mainbtn" Font-Size="XX-Large" role="button" Text="My Decks"></asp:button>

</asp:Content>

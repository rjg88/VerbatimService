<%@ Page  Title="Create a Deck" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreateDeck.aspx.cs" Inherits="VerbatimWeb.CreateDeck" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Create a Deck</h1>
    </div>
    <asp:FormView DefaultMode="Insert" ID="CreateDeckFormView" runat="server" ItemType="VerbatimService.Deck" InsertMethod="InsertDeck" >
        <InsertItemTemplate>
            <table>
                <tr>
                    <td>
                        <b>Name</b>:
                    </td>
                    <td>
                        <asp:TextBox ID="Name" runat="server" Columns="100" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Description</b>:
                    </td>
                    <td>
                        <asp:TextBox ID="Description" runat="server" Columns="100" TextMode="MultiLine" Text='<%# Bind("Description") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Author</b>:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxAuthor" runat="server" Columns="100" Text='<%# Bind("Author") %>' />

                    </td>
                </tr>
			    <tr>
                    <td>
                        <b>Token to Load on TTS</b>:
                    </td>
                    <td>
					    <asp:TextBox ID="TextBoxIDToken" runat="server" Columns="100" Text='<%# Bind("IdentifiyngToken") %>' />

                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Password</b>:
                    </td>
                    <td>
					    <asp:TextBox ID="TextBoxPassword" runat="server" Columns="100" Text='<%# Bind("Password") %>' />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-success" Text="Insert" runat="server" CommandName="Insert" />
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
	</asp:FormView>
</asp:Content>

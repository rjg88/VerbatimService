<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeckSearchResults.aspx.cs" Inherits="VerbatimWeb.DeckSearchResults" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<asp:HiddenField runat="server" ID="HiddenDeckId"/>
	<div runat="server" id="NoResultsFoundContainer" class="jumbotron">
		<div class="alert alert-danger">
		  <h1 id="NoResultsFoundAlert" runat="server">Danger!</h1>
		</div>
	</div>
	<br />
	<asp:FormView ID="DeckSearchResultsFormView" runat="server" EnableViewstate="true" ItemType="VerbatimService.Deck" SelectMethod ="SearchDecks" RenderOuterTable="false">
        <ItemTemplate>
			<div class="panel panel-default">
				<div class="panel-heading">
					<h1><b><%#:Item.Name %></b></h1>
				</div>
				<br />
				<table>
					<tr>
						<td>&nbsp;</td>  
						<td style="vertical-align: top; text-align:left;">
							<div class="panel-body">
								<h2><b>Description:</b>&nbsp;<%#Item.Description %></h2>
							</div>
							<br />
							<div class="panel-body">
								<h2><b>TTS Load Token:</b>&nbsp;<%#: Item.IdentifiyngToken %></h2>
							</div>
							<br />
							<div class="panel-body">
								<h2><b>Author:</b>&nbsp;<%#:Item.Author %></h2>
							</div>
							<br />
							<br />

							<br />
						</td>
					</tr>
				</table>
			</div>

        </ItemTemplate>
								
    </asp:FormView>
							<asp:Label runat="server" Text="Password:" Font-Size="Medium"></asp:Label>
								<asp:textbox ID="PasswordBox" runat="server"></asp:textbox>
								<asp:Button class="btn btn-info" type="button" ID="ButtonViewCards" runat="server" onclick="ButtonViewCards_Click" Text="View Cards" />  

</asp:Content>


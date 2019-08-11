<%@ Page Title="Deck Details" MasterPageFile="~/Site.Master"  Language="C#" AutoEventWireup="true" CodeBehind="DeckDetails.aspx.cs" Inherits="VerbatimWeb.DeckDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="panel panel-default">

    <div class="panel-heading">
					    <h1><b><asp:Label runat="server" ID="Name"></asp:Label></b></h1>
    </div>

    <table>
					<tr>
						<td>&nbsp;</td>  
						<td style="vertical-align: top; text-align:left;">
							<div class="panel-body">
								<h2><b>Description:</b>&nbsp;<asp:Label runat="server" Id="Description"></asp:Label></h2>
							</div>
							<br />
							<div class="panel-body">
								<h2><b>TTS Load Token:</b>&nbsp;<asp:Label runat="server" Id="Token"></asp:Label></h2>
							</div>
							<br />
							<div class="panel-body">
								<h2><b>Author:</b>&nbsp;<asp:Label runat="server" Id="Author"></asp:Label></h2>
							</div>
							<br />
							<br />

							<br />
						</td>
					</tr>
				</table>
		    <asp:Label runat="server" Text="Password:" Font-Size="Medium"></asp:Label>
		    <asp:textbox ID="PasswordBox" runat="server"></asp:textbox>
		    <asp:Button class="btn btn-info" type="button" ID="ButtonViewCards" runat="server" onclick="ButtonViewCards_Click" Text="View Cards" />  
            <asp:Button class="btn btn-info" type="button" ID="ButtonExcelUpload" runat="server" onclick="ButtonExcelUpload_Click" Text="Bulk Add With Excel Upload" />  
</div>
</asp:Content>
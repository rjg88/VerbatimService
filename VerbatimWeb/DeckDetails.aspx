<%@ Page Title="Deck Details" MasterPageFile="~/Site.Master"  Language="C#" AutoEventWireup="true" CodeBehind="DeckDetails.aspx.cs" Inherits="VerbatimWeb.DeckDetails" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

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
							<div class="panel-body">
								<h2><b>Language:</b>&nbsp;<asp:Label runat="server" Id="Language"></asp:Label></h2>
							</div>
							<br />
                            <div class="panel-body">
								<h2><b>Point Distribution:</b>&nbsp;<asp:Label runat="server" Id="Distribution"></asp:Label></h2>
							</div>
							<br />
                            <div class="panel-body">
								<h2><b>Total Cards:</b>&nbsp;<asp:Label runat="server" Id="TotalCards"></asp:Label></h2>
							</div>

							<br />
						</td>
                        <td style="padding-left:100px"><asp:chart id="Chart1" runat="server"
             Height="300px" Width="400px">
  <titles>
    <asp:Title ShadowOffset="3" Name="Title1" />
  </titles>
  <legends>
    <asp:Legend Alignment="Center" Docking="Bottom"
                IsTextAutoFit="False" Name="Default"
                LegendStyle="Row" Font="Trebuchet MS, 15pt, style=Bold"/>
  </legends>
  <series>
    <asp:Series Name="Default" />
  </series>
  <chartareas>
    <asp:ChartArea Name="ChartArea1"
                     BorderWidth="0" />
  </chartareas>
</asp:chart></td>
					</tr>
				</table>
		    <%--<asp:Label runat="server" Text="Password:" Font-Size="Medium"></asp:Label>
		    <asp:textbox ID="PasswordBox" TextMode="Password" runat="server"></asp:textbox>--%>
            <asp:Button class="btn btn-primary" type="button" ID="ButtonViewCards" runat="server" onclick="ButtonViewCards_Click" Text="View Cards" />  
		    <asp:Button class="btn btn-info" type="button" ID="ButtonEditCards" runat="server" onclick="ButtonEditCards_Click" Text="Edit Cards" />  
            <asp:Button class="btn btn-success" type="button" ID="ButtonExcelClick" runat="server" onclick="ButtonExcelUpload_Click" Text="Upload Spreadsheet" />  
            <asp:Button class="btn btn-warning" type="button" ID="ButtonEditDeck" runat="server" onclick="ButtonEdit_Click" Text="Edit Deck" />  
            <asp:Button class="btn btn-danger" type="button" ID="ButtonDeleteDeck" runat="server" onclick="ButtonDelete_Click" Text="Delete" />  
</div>
</asp:Content>
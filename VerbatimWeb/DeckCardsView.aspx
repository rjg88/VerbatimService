<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeckCardsView.aspx.cs" Inherits="VerbatimWeb.DeckCardsView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<br />
	<asp:Label runat="server" Text="Find Card:" Font-Size="Medium"></asp:Label>
	<asp:textbox ID="FilterInputBox" runat="server"></asp:textbox>
	<asp:Button class="btn btn-info" type="button" ID="ButtonFilter" runat="server" onclick="ButtonFilter_Click" Text="Filter" />  
	<br />
	<br />

	<asp:gridview id="DeckCardsGridView" 
		OnRowCreated="DeckCardsGridView_RowCreated"
		DeleteMethod="DeleteCard"
		InsertMethod="InsertCard"
  SelectMethod ="LoadDeckCards" 
  autogeneratecolumns="true"
		UpdateMethod="EditCard"
  emptydatatext="No data available." 
  allowpaging="True" 
 PageSize="1000"
		CssClass="grid"
        RowStyle-CssClass="grid-row"
        AlternatingRowStyle-CssClass="grid-row-alternating"
        HeaderStyle-CssClass="grid-header"
        FooterStyle-CssClass="grid-footer"
		AllowSorting="true"
		
  runat="server">
    <Columns>
		<asp:CommandField ButtonType="Link" ShowEditButton="True"
     ControlStyle-CssClass="btn btn-warning" />
				<asp:CommandField ButtonType="Link" ShowDeleteButton="True"
     ControlStyle-CssClass="btn btn-danger" />
    </Columns>           
    </asp:GridView>

</asp:Content>

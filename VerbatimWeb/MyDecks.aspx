<%@ Page Language="C#" Title="My Decks" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyDecks.aspx.cs" Inherits="VerbatimWeb.MyDecks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<asp:HiddenField runat="server" ID="HiddenDeckId"/>
	<div runat="server" id="NoResultsFoundContainer" class="jumbotron">
		<div class="alert alert-danger">
		  <h1 id="NoResultsFoundAlert" runat="server">Danger!</h1>
		</div>
	</div>
	<br />
        <asp:GridView id="DeckResultsGridView" 
		OnRowDataBound="DeckResultsGridView_DataBound"
        OnRowCreated="DeckResultsGridView_RowCreated"
  SelectMethod ="LoadDeckCards" 
            DeleteMethod="ViewDetails"
  autogeneratecolumns="true"
  emptydatatext="No data available." 
  allowpaging="True" 
 PageSize="10"
		CssClass="grid" 
        RowStyle-CssClass="grid-row"
        AlternatingRowStyle-CssClass="grid-row-alternating"
        HeaderStyle-CssClass="grid-header"
        FooterStyle-CssClass="grid-footer"
		AllowSorting="true"
		ShowFooter="false"
  runat="server">
    <Columns>
		<asp:CommandField ButtonType="Link" ShowDeleteButton="True"
            ControlStyle-CssClass="btn btn-info" DeleteText="View Details" />
    </Columns>  
        </asp:GridView>

</asp:Content>

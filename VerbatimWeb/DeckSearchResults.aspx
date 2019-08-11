<%@ Page Title="Deck Search Results" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeckSearchResults.aspx.cs" Inherits="VerbatimWeb.DeckSearchResults" %>

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
								
<%--	<div runat="server" ID="PasswordSection">
		<asp:Label runat="server" Text="Password:" Font-Size="Medium"></asp:Label>
		<asp:textbox ID="PasswordBox" runat="server"></asp:textbox>
		<asp:Button class="btn btn-info" type="button" ID="ButtonViewCards" runat="server" onclick="ButtonViewCards_Click" Text="View Cards" />  
	</div>--%>
    <div runat="server" ID="SearchSection" visible="false">
	    <asp:textbox ID="SearchInputBox" runat="server"></asp:textbox>
	    <asp:Button class="btn btn-info" type="button" ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" Text="Search" />  
    </div>
</asp:Content>


<%@ Page Title="View a Deck's Cards" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="DeckCardsView.aspx.cs" Inherits="VerbatimWeb.DeckCardsView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="highslide-html-content" id="highslide-html">
	    <div class="highslide-header">
		    <ul>
			    <li class="highslide-move">
				    <a href="#" onclick="return false">Move</a>
			    </li>
			    <li class="highslide-close">
				    <a href="#" onclick="return hs.close(this)">Close</a>
			    </li>
		    </ul>
	    </div>
	    <div class="highslide-body">
		        <img style="width:100%; height:100%" runat="server" id="ImageHolder" src="tbd" alt="Preview a Card" />
	    </div>
        <div class="highslide-footer">
        <div>
            <span class="highslide-resize" title="Resize">
                <span></span>
            </span>
        </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="HiddenDeckId"/>
    <br />
	<asp:Label runat="server" Text="Find Card:" Font-Size="Medium"></asp:Label>
	<asp:textbox ID="FilterInputBox" runat="server"></asp:textbox>
	<asp:Button class="btn btn-info" type="button" ID="ButtonFilter" runat="server" onclick="ButtonFilter_Click" Text="Filter" />  
	<br />
	<br />

	<asp:gridview id="DeckCardsGridView" 
		OnRowCreated="DeckCardsGridView_RowCreated"
  SelectMethod ="LoadDeckCards" 
        DeleteMethod="LoadImage"
  autogeneratecolumns="true"
  emptydatatext="No cards in the deck!" 
  allowpaging="True" 
 PageSize="20"
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
            ControlStyle-CssClass="btn btn-primary" DeleteText="Preview Card" />
        </Columns>
    </asp:GridView>    

</asp:Content>

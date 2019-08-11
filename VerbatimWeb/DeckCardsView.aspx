<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeckCardsView.aspx.cs" Inherits="VerbatimWeb.DeckCardsView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<br />
	<asp:Label runat="server" Text="Find Card:" Font-Size="Medium"></asp:Label>
	<asp:textbox ID="FilterInputBox" runat="server"></asp:textbox>
	<asp:Button class="btn btn-info" type="button" ID="ButtonFilter" runat="server" onclick="ButtonFilter_Click" Text="Filter" />  
	<br />
	<br />

	<asp:gridview id="DeckCardsGridView" 
		OnRowDataBound="DeckCardsGridView_DataBound"
		OnRowCreated="DeckCardsGridView_RowCreated"
		DeleteMethod="DeleteCard"
		InsertMethod="InsertCard"
  SelectMethod ="LoadDeckCards" 
  autogeneratecolumns="true"
		UpdateMethod="EditCard"
  emptydatatext="No data available." 
  allowpaging="True" 
 PageSize="10"
		CssClass="grid"
        RowStyle-CssClass="grid-row"
        AlternatingRowStyle-CssClass="grid-row-alternating"
        HeaderStyle-CssClass="grid-header"
        FooterStyle-CssClass="grid-footer"
		AllowSorting="true"
		ShowFooter="true"
		
  runat="server">
    <Columns>
		<asp:CommandField ButtonType="Link" ShowEditButton="True"
     ControlStyle-CssClass="btn btn-warning" />
		
<asp:TemplateField>
  <ItemTemplate>
    <asp:LinkButton runat="server" ID="DeleteButton" 
		ControlStyle-CssClass="btn btn-danger"
      CommandName="delete"  
      OnClientClick="if (!window.confirm('Are you sure you want to delete this item?')) return false;"
		Text="Delete"/>
  </ItemTemplate>
</asp:TemplateField>  
				<%--<asp:CommandField ButtonType="Link" ShowDeleteButton="True"
     ControlStyle-CssClass="btn btn-danger" OnClientClick="return confirm('confirm delete')" />--%>
    </Columns>           
    </asp:GridView>
	<br />
	<h2>Insert Card:</h2>
	<asp:FormView DefaultMode="Insert" ID="InsertCardFormView" runat="server" ItemType="VerbatimService.Card" InsertMethod="InsertCard" >
        <InsertItemTemplate>
           <div>
                <b>Title</b>: <asp:TextBox ID="Title" runat="server" Text='<%# Bind("Title") %>' />
            </div>
            <div>
                <b>Description</b>: <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>' />
            </div>
            <div>
                <b>Category</b>: <asp:DropDownList ID="Category" runat="server" Text='<%# Bind("Category") %>'>
										<asp:ListItem>CELEBRITY</asp:ListItem>
										<asp:ListItem>FICTIONAL</asp:ListItem>
										<asp:ListItem>HISTORICAL</asp:ListItem>
										<asp:ListItem>GEOGRAPHY</asp:ListItem>
										<asp:ListItem>QUOTE</asp:ListItem>
										<asp:ListItem>SCIENCE</asp:ListItem>
										<asp:ListItem>POLITICAL</asp:ListItem>
										<asp:ListItem>ET CETERA</asp:ListItem>
								</asp:DropDownList>
            </div>
			<div>
                <b>Point Value</b>: <asp:DropDownList ID="DropDownList" runat="server" Text='<%# Bind("PointValue") %>'>
										<asp:ListItem>1</asp:ListItem>
										<asp:ListItem>2</asp:ListItem>
										<asp:ListItem>3</asp:ListItem>
										<asp:ListItem>4</asp:ListItem>
                                    </asp:DropDownList>
            </div>
            <asp:Button Text="Insert" runat="server" CommandName="Insert" />
        </InsertItemTemplate>
	</asp:FormView>

</asp:Content>

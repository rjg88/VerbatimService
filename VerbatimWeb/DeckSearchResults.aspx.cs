using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class DeckSearchResults : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void DeckResultsGridView_DataBound(object sender, GridViewRowEventArgs e)
        {
            return;

        }
        public void ViewDetails(int VerbatimDeckId)
        {
            Response.Redirect("DeckDetails.aspx?DeckID=" + VerbatimDeckId);
        }

        protected void DeckResultsGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                // hides the Identity columns
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;

            }
        }

        public IQueryable<Deck> LoadDeckCards([QueryString("query")] string query)
        {
            string QueryURL = Utilities.ServerDNS + "/SearchForDeck?Query=" + query;

            List<Deck> Decks = new List<Deck>();

            if (!string.IsNullOrEmpty(query))
                Decks = JsonConvert.DeserializeObject<List<Deck>>(Utilities.MakeGETRequest(QueryURL));
            if(Decks.Count == 0)
            {
                SearchSection.Visible = true;
                DeckResultsGridView.Visible = false;
                NoResultsFoundAlert.InnerText = "No Results Found";
            }
            else
            {
                NoResultsFoundContainer.Visible = false;
            }
            if(Decks.Count > 0)
                HiddenDeckId.Value = Decks[0].VerbatimDeckId.ToString();
            return Decks.AsQueryable();
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeckSearchResults.aspx?query=" + HttpUtility.UrlEncode(SearchInputBox.Text), false);
        }
    }
}
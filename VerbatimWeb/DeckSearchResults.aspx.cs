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
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }

        public IQueryable<Deck> LoadDeckCards([QueryString("query")] string query)
        {
            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/SearchForDeck/" + query;

            List<Deck> Decks = new List<Deck>();

            if (!string.IsNullOrEmpty(query))
                Decks = JsonConvert.DeserializeObject<List<Deck>>(MakeGETRequest(QueryURL));
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
        private string MakeGETRequest(string uri)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeckSearchResults.aspx?query=" + SearchInputBox.Text, false);
        }
    }
}
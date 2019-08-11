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
        protected void ButtonViewCards_Click(object sender, EventArgs e)
        {
            // check Password
            //DeckId = ((Deck)DeckSearchResultsFormView.DataItem).VerbatimDeckId.ToString();

            Deck Deck = JsonConvert.DeserializeObject<Deck>(MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + HiddenDeckId.Value));

            if (Deck.Password == PasswordBox.Text)
                Response.Redirect("DeckCardsView.aspx?DeckId=" + HiddenDeckId.Value, false);
            else
                return;

        }
        public List<Deck> SearchDecks([QueryString("query")] string query)
        {
            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/SearchForDeck/" + query;

            List<Deck> Decks = new List<Deck>();

            if (!string.IsNullOrEmpty(query))
                Decks = JsonConvert.DeserializeObject<List<Deck>>(MakeGETRequest(QueryURL));
            if(Decks.Count == 0)
            {
                NoResultsFoundAlert.InnerText = "No Results Found";
                PasswordSection.Visible = false;
            }
            else
            {
                NoResultsFoundContainer.Visible = false;
            }
            if(Decks.Count > 0)
                HiddenDeckId.Value = Decks[0].VerbatimDeckId.ToString();
            return Decks;
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
    }
}
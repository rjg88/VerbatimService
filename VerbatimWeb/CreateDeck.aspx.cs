using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class CreateDeck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void InsertDeck(Deck Deck)
        {

            if (string.IsNullOrEmpty(Deck.IdentifiyngToken))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "TTS Token is required!" + "')", true);
                return;
            }
            if (string.IsNullOrEmpty(Deck.Author))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Author is required!" + "')", true);
                return;
            }
            if (string.IsNullOrEmpty(Deck.Description))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Description is required!" + "')", true);
                return;
            }
            if (string.IsNullOrEmpty(Deck.Password))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Password is required!" + "')", true);
                return;
            }
            if (string.IsNullOrEmpty(Deck.Name))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Name is required!" + "')", true);
                return;
            }
            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/GetAllDecks";

            List<Deck> Decks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Deck>>(MakeGETRequest(QueryURL));

            foreach(Deck DeckFromDB in Decks)
            {
                if(Deck.Name == DeckFromDB.Name)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Name is already taken!" + "')", true);
                    return;
                }
                else if (Deck.IdentifiyngToken == DeckFromDB.IdentifiyngToken)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Token is already taken!" + "')", true);
                    return;
                }
            }

            QueryURL = "http://platypuseggs.com/VerbatimService.svc/InsertDeck";

            using (var client = new System.Net.WebClient())
            {
                byte[] response = client.UploadData(QueryURL, "PUT", Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Deck)));
                Application["DeckId"] = client.Encoding.GetString(response);
            }

            Response.Redirect("DeckCardsView.aspx");

        }
        private string MakeGETRequest(string uri)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (System.Net.HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
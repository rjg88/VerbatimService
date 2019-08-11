using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class DeckDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["DeckId"] == null)
                Response.Redirect("Default.aspx");


            Deck Deck = JsonConvert.DeserializeObject<Deck>(MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Request.QueryString["DeckId"]));
            Name.Text = Deck.Name;
            Description.Text = Deck.Description;
            Author.Text = Deck.Author;
            Token.Text = Deck.IdentifiyngToken;
            Application["DeckId"] = Deck.VerbatimDeckId.ToString();
        }
        protected void ButtonViewCards_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Application["DeckId"].ToString()));

            Application["DeckId"] = Deck.VerbatimDeckId.ToString();
            if (Deck.Password == PasswordBox.Text)
                Response.Redirect("DeckCardsView.aspx", false);
            else
                return;
        }
        protected void ButtonExcelUpload_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Application["DeckId"].ToString()));

            Application["DeckId"] = Deck.VerbatimDeckId.ToString();
            if (Deck.Password == PasswordBox.Text)
                Response.Redirect("ExcelUploader.aspx", false);
            else
                return;
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
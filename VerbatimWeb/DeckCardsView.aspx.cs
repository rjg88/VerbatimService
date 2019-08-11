using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class DeckCardsView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ButtonFilter_Click(object sender, EventArgs e)
        {
            Uri uri = HttpContext.Current.Request.Url;
            if(uri.PathAndQuery.Contains("&"))
                Response.Redirect(uri.PathAndQuery.Substring(0, uri.PathAndQuery.IndexOf("&")) + "&Filter=" + FilterInputBox.Text, false);
            else
                Response.Redirect(uri.PathAndQuery + "&Filter=" + FilterInputBox.Text, false);
        }
        public IQueryable<Card> LoadDeckCards([QueryString("DeckId")] string DeckId, [QueryString("Filter")]string Filter)
        {
            if (Filter == null)
                Filter = "";
            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/GetDeckCards/" + DeckId + "?filter=" + Filter;

            List<Card> Decks = null;

            if (!string.IsNullOrEmpty(DeckId))
                Decks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Card>>(MakeGETRequest(QueryURL));
            else
                return null;
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
        public void EditCard(Card Card)
        {

            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/EditCard";

            using (var client = new System.Net.WebClient())
            {
                client.UploadData(QueryURL, "PUT", Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Card)));
            }
        }
        public void DeleteCard(Card Card)
        {
            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/DeleteCard";

            using (var client = new System.Net.WebClient())
            {
                client.UploadData(QueryURL, "PUT", Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Card)));
            }
        }
        public void InsertCard(string Title, string Description, string Category, int PointValue)
        {
            Card Card = new Card();
            Card.Title = Title;
            Card.Description = Description;
            Card.Category = Category;
            Card.PointValue = PointValue;

            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/InsertCard";

            using (var client = new System.Net.WebClient())
            {
                client.UploadData(QueryURL, "PUT", Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Card)));
            }
        }
        protected void DeckCardsGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.Cells.Count>1)
                e.Row.Cells[2].Visible = false; // hides the Identity column
        }
        protected void DeckCardsGridView_DataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                ((TextBox)e.Row.Cells[4].Controls[0]).TextMode = TextBoxMode.MultiLine;
                ((TextBox)e.Row.Cells[4].Controls[0]).Rows = 10;
            }

        }
    }
}
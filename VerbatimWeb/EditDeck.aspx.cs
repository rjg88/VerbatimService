using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class EditDeck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CheckForValidSteamSession(Request.Cookies["AccessToken"]))
            {
                Response.Redirect("Default");
            }
            object DeckIdCookie = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"];
            if (DeckIdCookie == null || string.IsNullOrEmpty(DeckIdCookie.ToString()))
                Response.Redirect("Default");
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + DeckIdCookie.ToString()));

            if (String.IsNullOrEmpty(((TextBox)this.Master.FindControl("MainContent").FindControl("EditDeckFormView").Controls[0].Controls[1].Controls[0].FindControl("Name")).Text))
            {
                ((TextBox)this.Master.FindControl("MainContent").FindControl("EditDeckFormView").Controls[0].Controls[1].Controls[0].FindControl("Name")).Text = Deck.Name;
                ((TextBox)this.Master.FindControl("MainContent").FindControl("EditDeckFormView").Controls[0].Controls[1].Controls[0].FindControl("Description")).Text = Deck.Description;
                ((TextBox)this.Master.FindControl("MainContent").FindControl("EditDeckFormView").Controls[0].Controls[1].Controls[0].FindControl("TextBoxAuthor")).Text = Deck.Author;
                ((TextBox)this.Master.FindControl("MainContent").FindControl("EditDeckFormView").Controls[0].Controls[1].Controls[0].FindControl("TextBoxIDToken")).Text = Deck.IdentifiyngToken;
                ((RadioButtonList)this.Master.FindControl("MainContent").FindControl("EditDeckFormView").Controls[0].Controls[1].Controls[0].FindControl("RadioDistribution")).SelectedIndex = Deck.UseStandardDistribution ? 0 : 1;
            }

        }
        public void UpdateDeck(Deck Deck)
        {
            string DeckIdCookieString = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString();
            if (string.IsNullOrEmpty(DeckIdCookieString))
                Response.Redirect("Default");
            Deck.VerbatimDeckId = Int32.Parse(DeckIdCookieString.ToString());
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
            if (string.IsNullOrEmpty(Deck.Name))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Name is required!" + "')", true);
                return;
            }
            string QueryURL = Utilities.ServerDNS + "/GetAllDecks";

            List<Deck> Decks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Deck>>(Utilities.MakeGETRequest(QueryURL));

            foreach (Deck DeckFromDB in Decks)
            {
                if (Deck.VerbatimDeckId == Deck.VerbatimDeckId)
                    break;
                if (Deck.Name == DeckFromDB.Name)
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

            QueryURL = Utilities.ServerDNS + "/EditDeck";

            using (var client = new System.Net.WebClient())
            {
                byte[] response = client.UploadData(QueryURL, "PUT", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Deck)));
                HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
                DeckIdCookie.Values.Add("VerbatimDeckId", client.Encoding.GetString(response));
                DeckIdCookie.Expires = DateTime.Now.AddHours(24);
                Response.Cookies.Add(DeckIdCookie);
            }

            Response.Redirect("DeckDetails.aspx?DeckId=" + Deck.VerbatimDeckId);
        }
    }
}
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
    public partial class DeleteDeck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CheckForValidSteamSession(Request.Cookies["AccessToken"]))
            {
                HttpCookie myCookie = new HttpCookie("SteamUserData");
                myCookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(myCookie); Response.Redirect("Default");
            }
            object DeckIdCookie = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"];
            if (DeckIdCookie == null || string.IsNullOrEmpty(DeckIdCookie.ToString()))
                Response.Redirect("Default");

        }

        public void ButtonDelete_Click(object sender, EventArgs e)
        {
            string DeckIdCookieString = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString();
            if (string.IsNullOrEmpty(DeckIdCookieString))
                Response.Redirect("Default");
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + DeckIdCookieString));

            if(TextBoxDeckName.Text == Deck.Name)
            {
                string QueryURL = Utilities.ServerDNS + "/DeleteDeck";

                using (var client = new System.Net.WebClient())
                {
                    byte[] response = client.UploadData(QueryURL, "PUT", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Deck)));
                    HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
                    DeckIdCookie.Values.Add("VerbatimDeckId", client.Encoding.GetString(response));
                    DeckIdCookie.Expires = DateTime.Now.AddHours(24);
                    Response.Cookies.Add(DeckIdCookie);
                }
                Response.Redirect("Default");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                    "alertMessage", @"alert('" + "Incorrect Name!" + "')", true);
            }

        }


    }
}
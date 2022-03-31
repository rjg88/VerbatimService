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
            if (Request.QueryString["DeckId"] == null)
                Response.Redirect("Default");
        }

        public void ButtonDelete_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + Request.QueryString["DeckId"]));

            if(TextBoxDeckName.Text == Deck.Name)
            {
                string QueryURL = Utilities.ServerDNS + "/DeleteDeck";

                using (var client = new System.Net.WebClient())
                {
                    byte[] response = client.UploadData(QueryURL, "PUT", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Deck)));
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
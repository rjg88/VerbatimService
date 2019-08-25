using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class MyDecks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CheckForValidSteamSession(Request.Cookies["AccessToken"]))
            {
                HttpCookie myCookie = new HttpCookie("SteamUserData");
                myCookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(myCookie); Response.Redirect("Default");
            }
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
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;

            }
        }

        public IQueryable<Deck> LoadDeckCards()
        {
            string SteamId = "";
            HttpCookie SteamCookie = Request.Cookies["SteamUserData"];
            if (SteamCookie != null)
            {
                SteamId = JObject.Parse(SteamCookie.Values["SteamUserData"].ToString())["response"]["players"][0]["steamid"].ToString();
            }

            string QueryURL = Utilities.ServerDNS + "/GetAllDecks";

            List<Deck> Decks = new List<Deck>();
            List<Deck> MyDecks = new List<Deck>();


            Decks = JsonConvert.DeserializeObject<List<Deck>>(Utilities.MakeGETRequest(QueryURL));
            if (Decks.Count == 0)
            {
                DeckResultsGridView.Visible = false;
                NoResultsFoundAlert.InnerText = "You don't have any decks!";
            }
            else
            {

                foreach (Deck Deck in Decks)
                {
                    string CheckAccessUrl = Utilities.ServerDNS + "/CheckDeckAccess?SteamId=" + SteamId + "&DeckId=" + Deck.VerbatimDeckId;
                    if(bool.Parse(JObject.Parse(Utilities.MakeGETRequest(CheckAccessUrl))["CheckDeckAccessResult"].ToString()))
                    {
                        MyDecks.Add(Deck);
                    }
                }
                NoResultsFoundContainer.Visible = false;
            }
            if (Decks.Count > 0)
                HiddenDeckId.Value = Decks[0].VerbatimDeckId.ToString();
            return MyDecks.AsQueryable();
        }

    }
}
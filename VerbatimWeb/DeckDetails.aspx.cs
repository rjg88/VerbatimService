using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class DeckDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["DeckId"] == null)
                Response.Redirect("Default");
            string SteamId = "";
            HttpCookie myCookie = Request.Cookies["SteamUserData"];
            if(myCookie != null)
                SteamId = JObject.Parse(myCookie.Values["SteamUserData"].ToString())["response"]["players"][0]["steamid"].ToString();

            if(!string.IsNullOrEmpty(SteamId))
            {
                string Response = Utilities.MakeGETRequest(Utilities.ServerDNS + "/CheckDeckAccess?SteamId=" + SteamId + "&DeckId=" + Request.QueryString["DeckId"]);

                if(bool.Parse(JObject.Parse(Response)["CheckDeckAccessResult"].ToString()))
                {
                    ButtonEditCards.Visible = true;
                    ButtonEditDeck.Visible = true;
                    ButtonExcelClick.Visible = true;
                    ButtonDeleteDeck.Visible = true;
                }
                else
                {
                    ButtonEditCards.Visible = false;
                    ButtonEditDeck.Visible = false;
                    ButtonExcelClick.Visible = false;
                    ButtonDeleteDeck.Visible = false;
                }
                
            }
            else
            {
                ButtonEditCards.Visible = false;
                ButtonEditDeck.Visible = false;
                ButtonExcelClick.Visible = false;
                ButtonDeleteDeck.Visible = false;
            }

            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + Request.QueryString["DeckId"]));
            Name.Text = HttpUtility.HtmlEncode(Deck.Name);
            Description.Text = HttpUtility.HtmlEncode(Deck.Description);
            Author.Text = HttpUtility.HtmlEncode(Deck.Author);
            Token.Text = HttpUtility.HtmlEncode(Deck.IdentifiyngToken);
            TotalCards.Text = Deck.TotalCards.ToString();
            Distribution.Text = Deck.UseStandardDistribution ? "Standard" : "Random" ; 
            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);

            double[] yValues = { Deck.OnePointTotalCards, Deck.TwoPointTotalCards, Deck.ThreePointTotalCards, Deck.FourPointTotalCards};
            string[] xValues = { "1", "2", "3", "4" };

            Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);

            Chart1.Series["Default"].Points[0].Color = Color.DarkGreen;
            Chart1.Series["Default"].Points[1].Color = Color.Navy;
            Chart1.Series["Default"].Points[2].Color = Color.OrangeRed;
            Chart1.Series["Default"].Points[3].Color = Color.Black;

            Chart1.Series["Default"].ChartType = SeriesChartType.Pie;
            Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
        }
        protected void ButtonViewCards_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));
            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            Response.Redirect("DeckCardsView.aspx", false);
        }

        protected void ButtonEditCards_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));
            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            Response.Redirect("DeckCardsEdit.aspx", false);

        }
        protected void ButtonExcelUpload_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));

            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            Response.Redirect("ExcelUploader.aspx", false);

        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));

            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            Response.Redirect("EditDeck.aspx", false);

        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest(Utilities.ServerDNS + "/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));

            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            Response.Redirect("DeleteDeck.aspx", false);

        }
    }
}
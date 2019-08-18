using Newtonsoft.Json;
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
                Response.Redirect("Default.aspx");


            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Request.QueryString["DeckId"]));
            Name.Text = Deck.Name;
            Description.Text = Deck.Description;
            Author.Text = Deck.Author;
            Token.Text = Deck.IdentifiyngToken;
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
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));
            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            Response.Redirect("DeckCardsView.aspx", false);
        }

        protected void ButtonEditCards_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));
            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            if (Deck.Password == PasswordBox.Text || Deck.Password == Utilities.sha256_hash(PasswordBox.Text))
                Response.Redirect("DeckCardsEdit.aspx", false);
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Incorrect password!" + "')", true);
            }
        }
        protected void ButtonExcelUpload_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));

            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            if (Deck.Password == PasswordBox.Text || Deck.Password == Utilities.sha256_hash(PasswordBox.Text))
                Response.Redirect("ExcelUploader.aspx", false);
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Incorrect password!" + "')", true);
            }
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));

            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            if (Deck.Password == PasswordBox.Text || Deck.Password == Utilities.sha256_hash(PasswordBox.Text))
                Response.Redirect("EditDeck.aspx", false);
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Incorrect password!" + "')", true);
            }
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            Deck Deck = JsonConvert.DeserializeObject<Deck>(Utilities.MakeGETRequest("http://platypuseggs.com/VerbatimService.svc/GetDeck/" + Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"].ToString()));

            HttpCookie DeckIdCookie = new HttpCookie("VerbatimDeckId");
            DeckIdCookie.Values.Add("VerbatimDeckId", Deck.VerbatimDeckId.ToString());
            DeckIdCookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(DeckIdCookie);
            if (Deck.Password == PasswordBox.Text || Deck.Password == Utilities.sha256_hash(PasswordBox.Text))
                Response.Redirect("DeleteDeck.aspx", false);
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "Incorrect password!" + "')", true);
            }
        }
    }
}
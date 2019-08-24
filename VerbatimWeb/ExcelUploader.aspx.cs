using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimService;

namespace VerbatimWeb
{
    public partial class ExcelUploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CheckForValidSteamSession(Request.Cookies["AccessToken"]))
            {
                HttpCookie myCookie = new HttpCookie("SteamUserData");
                myCookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(myCookie);

                Response.Redirect("Default");
            }
            object DeckIdCookie = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"];
            if (DeckIdCookie == null || string.IsNullOrEmpty(DeckIdCookie.ToString()))
                Response.Redirect("Default");
        }
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            object DeckIdCookie = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"];
            if (DeckIdCookie == null || string.IsNullOrEmpty(DeckIdCookie.ToString()))
                return;
            string DeckId = DeckIdCookie.ToString();
            if (!FileUploadCSV.FileName.EndsWith(".csv") && !FileUploadCSV.FileName.EndsWith(".tsv"))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "File MUST be a .CSV/.TSV (You can save to this from Excel/Sheets)" + "')", true);
                return;
            }

            StreamReader reader = new StreamReader(FileUploadCSV.FileContent);
            string Line = "";
            string QueryURL = Utilities.ServerDNS + "/InsertCard";

            while ((Line = reader.ReadLine()) != null)
            {

                List<string> LineValues = new List<string>();
                if (FileUploadCSV.FileName.EndsWith(".tsv"))
                    LineValues = Line.Split('\t').ToList();
                else
                    LineValues = Regex.Split(Line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)").ToList();
                Card Card = new Card();
                Card.VerbatimDeckId = Int32.Parse(DeckId);
                Card.Title = LineValues[0].ToString();
                Card.Description = LineValues[1].ToString();
                Card.Category = LineValues[2].ToString();
                Card.PointValue = Int32.Parse(LineValues[3].ToString());

                using (var client = new System.Net.WebClient())
                {
                    try
                    {
                        client.UploadData(QueryURL, "PUT", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Card)));
                    }
                    catch
                    {

                    }
                }
            }
            reader.Close();

            Response.Redirect("DeckCardsView.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            object DeckIdCookie = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"];
            if (DeckIdCookie == null || string.IsNullOrEmpty(DeckIdCookie.ToString()))
                Response.Redirect("Default.aspx");

        }
        protected void ButtonFilter_Click(object sender, EventArgs e)
        {
            string QueryURL = "DeckCardsView.aspx?filter=" + FilterInputBox.Text;

            Response.Redirect(QueryURL, false);
        }
        public IQueryable<Card> LoadDeckCards([QueryString("DeckPassword")] string DeckPassword, [QueryString("Filter")]string Filter)
        {
            object DeckIdCookie = Request.Cookies["VerbatimDeckId"].Values["VerbatimDeckId"];
            if (DeckIdCookie == null || string.IsNullOrEmpty(DeckIdCookie.ToString()))
                return null;

            string DeckId = DeckIdCookie.ToString();
            if (Filter == null)
                Filter = "";
            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/GetDeckCards/" + DeckId + "?filter=" + Filter;

            List<Card> Cards = null;

            if (!string.IsNullOrEmpty(DeckId))
            {
                Cards = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Card>>(Utilities.MakeGETRequest(QueryURL));
                HiddenDeckId.Value = DeckId;

            }
            else
                return null;
            return Cards.AsQueryable();
        }
        protected void DeckCardsGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                // hides the Identity columns
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
        public void LoadImage(Card Card)
        {
            string QueryURL = "http://platypuseggs.com/VerbatimService.svc/RenderCard/" + Card.VerbatimCardId;
            MemoryStream ms = Utilities.MakeGETRequestStream(QueryURL);
            string base64Data = Convert.ToBase64String(ms.ToArray());
            ImageHolder.Src = "data:image/gif;base64," + base64Data;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RunCode", "hs.htmlExpand(document.getElementById('test'), { contentId: 'highslide-html' });", true);
        }
    }
}
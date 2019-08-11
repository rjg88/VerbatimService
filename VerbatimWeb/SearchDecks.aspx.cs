using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VerbatimWeb
{
    public partial class SearchDecks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeckSearchResults.aspx?query=" + SearchInputBox.Text, false);
        }
    }
}
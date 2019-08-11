using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VerbatimWeb
{
    public partial class ExcelUploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["DeckId"] == null || string.IsNullOrEmpty(Application["DeckId"].ToString()))
                Response.Redirect("Default.aspx");
        }
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (Application["DeckId"] == null || string.IsNullOrEmpty(Application["DeckId"].ToString()))
                return;
            string DeckId = Application["DeckId"].ToString();
            if (!FileUploadCSV.FileName.EndsWith(".csv"))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            "alertMessage", @"alert('" + "File MUST be a .CSV (You can save to this from Excel)" + "')", true);
                return;
            }
        }
    }
}
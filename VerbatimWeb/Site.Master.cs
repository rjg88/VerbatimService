using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimWeb.Models;

namespace VerbatimWeb
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utilities.CheckForValidSteamSession(Request.Cookies["AccessToken"]))
            {
                HttpCookie myCookie = Request.Cookies["SteamUserData"];
                if (myCookie != null)
                {
                    if (!string.IsNullOrEmpty(myCookie.Values["SteamUserData"]))
                    {
                        SteamName.Text = JObject.Parse(myCookie.Values["SteamUserData"].ToString())["response"]["players"][0]["personaname"].ToString();
                        SteamImage.ImageUrl = JObject.Parse(myCookie.Values["SteamUserData"].ToString())["response"]["players"][0]["avatar"].ToString();

                        CreateOrRefreshSession CreateSession = new CreateOrRefreshSession();
                        CreateSession.SteamId = JObject.Parse(myCookie.Values["SteamUserData"].ToString())["response"]["players"][0]["steamid"].ToString();
                        CreateSession.ServerPassword = Utilities.ServerPassword;
                        Utilities.MakePOSTRequest(Utilities.ServerDNS + "/RefreshAccessToken", CreateSession);

                        HttpCookie AccessTokenCookie = new HttpCookie("AccessToken");
                        AccessTokenCookie.Expires = DateTime.Now.AddMinutes(5);
                        Response.Cookies.Add(myCookie);
                    }
                }
                
            }
            else
            {
                //HttpCookie myCookie2 = new HttpCookie("SteamUserData");
                //myCookie2.Expires = DateTime.Now.AddHours(-1);
                //Response.Cookies.Add(myCookie2);
            }
            
        }
    }
}
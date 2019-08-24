using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerbatimService;
using Owin.Security.Providers.Steam;
using DotNetOpenAuth.OpenId.RelyingParty;
using VerbatimWeb.Models;
using Newtonsoft.Json.Linq;

namespace VerbatimWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CheckForValidSteamSession(Request.Cookies["AccessToken"]))
            {
                var openid = new OpenIdRelyingParty();
                var response = openid.GetResponse();

                if (response != null)
                {
                    switch (response.Status)
                    {
                        case AuthenticationStatus.Authenticated:

                            string responseURI = response.ClaimedIdentifier.ToString();

                            CreateOrRefreshSession CreateSession = new CreateOrRefreshSession();
                            CreateSession.SteamId = responseURI.Substring(responseURI.LastIndexOf("/") + 1);
                            CreateSession.ServerPassword = Utilities.ServerPassword;

                            string TokenWrapper = Utilities.MakePOSTRequest(Utilities.ServerDNS + "/CreateSession", CreateSession);
                            JObject JObject = JObject.Parse(TokenWrapper);
                            string Token = JObject["CreateSessionResult"].ToString();
                            HttpCookie myCookie = new HttpCookie("AccessToken");
                            myCookie.Values.Add("AccessToken", Token);
                            myCookie.Expires = DateTime.Now.AddHours(12);
                            Response.Cookies.Add(myCookie);

                            string SteamUserData = SteamAPI.GetData(CreateSession.SteamId);
                            HttpCookie UserDataCookie = new HttpCookie("SteamUserData");
                            UserDataCookie.Values.Add("SteamUserData", SteamUserData);
                            UserDataCookie.Expires = DateTime.Now.AddHours(12);
                            Response.Cookies.Add(UserDataCookie);
                            SteamLoginButton.Visible = false;
                            Response.Redirect("Default", false);

                            break;

                        case AuthenticationStatus.Canceled:
                        case AuthenticationStatus.Failed:
                            {
                                Response.Redirect("Default", false);
                            }
                            break;
                    }
                }
                else
                {
                    CreateDeckButton.CssClass = "btn btn-success mainbtn disabled";
                }
            }
            else
            {
                SteamLoginButton.Visible = false;
            }
        }

        protected void SteamLogin(object sender, EventArgs e)
        {

            using (OpenIdRelyingParty openidd = new OpenIdRelyingParty())
            {
                IAuthenticationRequest request = openidd.CreateRequest("http://steamcommunity.com/openid");
                request.RedirectToProvider();
            }
            
        }

        protected void CreateDeckRedirect(object ender, EventArgs e)
        {
            Response.Redirect("CreateDeck", false);
        }

    }
}
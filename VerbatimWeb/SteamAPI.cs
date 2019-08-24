using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerbatimWeb
{
    public class SteamAPI
    {
        private static readonly string SteamBaseURL = "http://api.steampowered.com";

        private static readonly string APIKey = "F583A839EC9CCD0C1B922787787E977C";
        public static string GetData(string SteamId)
        {
            return Utilities.MakeGETRequest(SteamBaseURL + @"/ISteamUser/GetPlayerSummaries/v0002/?key=" + APIKey + "&steamids=" + SteamId);
        }
    }
}
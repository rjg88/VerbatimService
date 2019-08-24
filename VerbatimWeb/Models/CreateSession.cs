using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerbatimWeb.Models
{
    public class CreateOrRefreshSession
    {
        public string SteamId { get; set; }
        public string ServerPassword { get; set; }
    }
}
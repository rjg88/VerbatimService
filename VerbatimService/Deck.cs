using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VerbatimService
{
    [DataContract]
    public class Deck
    {
        [DataMember]
        public int VerbatimDeckId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string IdentifiyngToken { get; set; }
        [DataMember]
        public string Password { get; set; }

    }
}
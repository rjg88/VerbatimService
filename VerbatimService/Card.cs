using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VerbatimService
{
    [DataContract]
    public class Card
    {
        [DataMember]
        public int VerbatimCardId { get; set; }
        [DataMember]
        public int VerbatimDeckId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public int PointValue { get; set; }
        [DataMember]
        public string PictureURL { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;

namespace VerbatimService
{
    public class RawContentTypeMapper : WebContentTypeMapper
    {
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
                return WebContentFormat.Json;

            //if(string.IsNullOrEmpty(contentType))
            //    return WebContentFormat.Json;
            //switch (contentType.ToLowerInvariant())
            //{
            //    case "":
            //    case "text/plain":
            //    case "application/json":
            //        return WebContentFormat.Json;
            //    case "application/xml":
            //        return WebContentFormat.Xml;
            //    default:
            //        return WebContentFormat.Default;
            //}
        }
    }
}
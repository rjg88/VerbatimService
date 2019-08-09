using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VerbatimService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IVerbatimService
    {

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, 
        //    BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetDeck/{DeckSize}")]
        //Deck GetDeck(string DeckSize);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetDeckWithSteamIds/{DeckSize}")]
        SpawnedDeck SpawnPlayDeck(string DeckSize, string SteamIDs);



        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeck/{DeckId}")]
        Deck GetDeck(string DeckId);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "EditDeck")]
        void EditDeck(Deck Deck);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SearchForDeck/{Query}")]
        List<Deck> SearchForDeck(string Query);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeckCards/{DeckId}?filter={Filter}")]
        List<Card> GetDeckCards(string DeckId, string Filter);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "EditCard")]
        void EditCard(Card Card);

    }


 
}

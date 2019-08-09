using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VerbatimService
{
    public class VerbatimService : IVerbatimService
    {
        private SQLiteConnection Connection;
        private Persistence Persistence;
        private ImageProcessing ImageProcessing;
        private string DriveLetter = "C";

        private void Initialize()
        {
            if (Directory.Exists("E:"))
                DriveLetter = "E";
            Connection = new SQLiteConnection("Data Source=" + DriveLetter + @":\Verbatim\Verbatim.sqlite;Version=3;");
            Connection.Open();
            Persistence = new Persistence();
            Persistence.Connection = Connection;

            ImageProcessing = new ImageProcessing();
            ImageProcessing.DriveLetter = DriveLetter;
            ImageProcessing.Connection = Connection;

        }

        public void EditCard(Card Card)
        {
            Initialize();
            Persistence.EditCard(Card);
        }

        public void EditDeck(Deck Deck)
        {
            Initialize();
            Persistence.EditDeck(Deck);
        }

        public Deck GetDeck(string DeckId)
        {
            Initialize();
            return Persistence.GetDeck(DeckId);
        }

        public List<Deck> SearchForDeck(string Query)
        {
            Initialize();
            return Persistence.SearchForDeck(Query);
        }

        public List<Card> GetDeckCards(string DeckId, string Filter)
        {
            Initialize();
            return Persistence.GetDeckCards(DeckId, Filter);
        }

        // DEPRECATED
        //public Deck GetDeck(string DeckSize)
        //{
        //    Deck Deck = new Deck();

        //    try
        //    {
        //        string Host = "";
        //        if(System.Web.HttpContext.Current != null)
        //            Host = System.Web.HttpContext.Current.Request.Url.Host;

        //        ImageProcessing ImageProcessing = new ImageProcessing();
        //        Deck.ImageFile = Host + "/Verbatim/Sheets/Original/" + ImageProcessing.CreateDeck(Int32.Parse(DeckSize), new List<string>());
        //        Deck.Cards = ImageProcessing.CardObjects;

        //        return Deck;
        //    }
        //    catch (Exception E)
        //    {
        //        Deck.ImageFile = E.Message;
        //        return Deck;
        //    }


        //}

        public SpawnedDeck SpawnPlayDeck(string DeckSize, string SteamIDs)
        {
            Initialize();
            SpawnedDeck Deck = new SpawnedDeck();

            try
            {
                string Host = "";
                if (System.Web.HttpContext.Current != null)
                    Host = System.Web.HttpContext.Current.Request.Url.Host;

                List<string> ListSteamIDs = SteamIDs.Split(',').ToList();
                Deck.ImageFile = Host + "/Verbatim/Sheets/Original/" + ImageProcessing.CreateDeck(Int32.Parse(DeckSize), ListSteamIDs);
                Deck.Cards = ImageProcessing.CardObjects;

                return Deck;
            }
            catch (Exception E)
            {
                Deck.ImageFile = E.Message;
                return Deck;
            }


        }
    }
}

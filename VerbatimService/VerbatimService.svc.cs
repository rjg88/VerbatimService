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

        public SpawnedDeck GetDeckWithSteamIdsAndToken(string DeckSize, string Token, List<string> SteamIDs)
        {
            Initialize();
            SpawnedDeck Deck = new SpawnedDeck();

            try
            {
                string Host = "";
                if (System.Web.HttpContext.Current != null)
                    Host = System.Web.HttpContext.Current.Request.Url.Host;

                if (string.IsNullOrEmpty(Token))
                {
                    Deck.ImageFile = Host + "/Verbatim/Sheets/Original/" + ImageProcessing.CreateDeck(Int32.Parse(DeckSize), SteamIDs, "");
                    Deck.Cards = ImageProcessing.CardObjects;
                }
                else
                {
                    Deck.ImageFile = Host + "/Verbatim/Sheets/" + Token + "/" + ImageProcessing.CreateDeck(Int32.Parse(DeckSize), SteamIDs, Token);
                    Deck.Cards = ImageProcessing.CardObjects;
                }
                return Deck;
            }
            catch (Exception E)
            {
                Deck.ImageFile = E.Message;
                return Deck;
            }


        }

        public void InsertCard(Card Card)
        {
            Initialize();
            Persistence.InsertCard(Card);
        }

        public void DeleteCard(Card Card)
        {
            Initialize();
            Persistence.DeleteCard(Card);
        }

        public int InsertDeck(Deck Deck)
        {
            Initialize();
            return Persistence.InsertDeck(Deck);
        }

        public List<Deck> GetAllDecks()
        {
            Initialize();
            return Persistence.GetAllDecks();
        }

        public SpawnedDeck GetDeckWithSteamIds(string DeckSize, string SteamIDs)
        {
            List<string> ListSteamIDs = SteamIDs.Split(',').ToList();
            return GetDeckWithSteamIdsAndToken(DeckSize, "", ListSteamIDs);
        }

        public List<string> GetDeckCategories(string DeckId)
        {
            Initialize();
            return Persistence.GetDeckCategories(DeckId);
        }
    }
}

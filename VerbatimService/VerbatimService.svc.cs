using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;

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

        public Card GetCard(string CardId)
        {
            Initialize();
            return Persistence.GetCard(CardId);
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
                string Host = "platypuseggs.com";
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

        public void DeleteDeck(Deck Deck)
        {
            Initialize();
            Persistence.DeleteDeck(Deck);
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

        public void DeleteOneCardPlayHistory(string SteamID, int CardId)
        {
            Initialize();
            Persistence.DeleteOneCardPlayHistory(CardId, SteamID);
        }

        public Stream RenderCard(string CardId)
        {
            try
            {
                Initialize();
                ImageProcessing.Initialize();
                Card Card = GetCard(CardId);

                Bitmap bmp = ImageProcessing.GenerateImage(Card);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Position = 0;  //This's a very important
                return ms;
            }
            catch (Exception e)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(e.Message + e.StackTrace));
            }
        }

        public void DeleteCardPlayHistories(List<string> SteamIDs, List<int> CardIDs)
        {
            Initialize();
            Persistence.DeleteCardPlayHistories(SteamIDs, CardIDs);
        }

        public string CreateSession(string SteamId, string ServerPassword)
        {
            try
            {
                Initialize();
                if (ServerPassword == "PlatypusServerToken35_cgYjqa*345gfdr")
                    return Persistence.CreateSession(SteamId);
                else
                    return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public bool VerfiySession(string AccessToken)
        {
            Initialize();
            return Persistence.VerfiySession(AccessToken);
        }

        public bool CheckDeckAccess(string SteamId, int DeckId)
        {
            Initialize();
            return Persistence.CheckDeckAccess(SteamId, DeckId);
        }

        public void RefreshAccessToken(string SteamId, string ServerPassword)
        {
            Initialize();
            if (ServerPassword == "PlatypusServerToken35_cgYjqa*345gfdr")
                Persistence.RefreshAccessToken(SteamId);
        }

        public string vip(string s)
        {
            List<string> o = new List<string>();
            int i = 1, j = 1, k = 1, l = s.Length;
            for (; i < l; i++)
                for (j = 1; j < l; j++)
                    for (k = 1; k < l; k++)
                    {
                        try
                        {
                            string t = s.Insert(i, ".").Insert(j, ".").Insert(k, ".");
                            t = Regex.Replace(t, "[.]0[0-9]", "!");
                            IPAddress.Parse(t);
                            o.Add(t);
                        }
                        catch
                        {

                        }
                    }
            o = o.Distinct().ToList();
            return String.Join(",", o.Select(x => x.ToString()).ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace VerbatimService
{
    public class Persistence
    {
        public SQLiteConnection Connection { get; set; }
        private SQLiteCommand SQLiteCommand;

        public void EditCard(Card Card)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"UPDATE VerbatimCard
                           SET Title = :Title,
                               Description = :Description,
                               Category = :Category,
                               PointValue = :PointValue
                         WHERE VerbatimCardId = :VerbatimCardId";

            SQLiteCommand.Parameters.Add("Title", DbType.String).Value = Card.Title;
            SQLiteCommand.Parameters.Add("Description", DbType.String).Value = Card.Description;
            SQLiteCommand.Parameters.Add("Category", DbType.String).Value = Card.Category;
            SQLiteCommand.Parameters.Add("PointValue", DbType.String).Value = Card.PointValue;
            SQLiteCommand.Parameters.Add("VerbatimCardId", DbType.String).Value = Card.VerbatimCardId;

            SQLiteCommand.ExecuteNonQuery();
        }

        public void EditDeck(Deck Deck)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"
                           UPDATE VerbatimDeck
                           SET Name = :Name,
                               Description = :Description,
                               Author = :Author,
                               IdentifiyngToken = :IdentifiyngToken
                               UseStandardDistribution = :UseStandardDistribution
                         WHERE VerbatimDeckId = :VerbatimDeckId";
            SQLiteCommand.Parameters.Add("Name", DbType.String).Value = Deck.Name;
            SQLiteCommand.Parameters.Add("Description", DbType.String).Value = Deck.Description;
            SQLiteCommand.Parameters.Add("Author", DbType.String).Value = Deck.Author;
            SQLiteCommand.Parameters.Add("IdentifiyngToken", DbType.String).Value = Deck.IdentifiyngToken;
            SQLiteCommand.Parameters.Add("VerbatimDeckId", DbType.String).Value = Deck.VerbatimDeckId;
            SQLiteCommand.Parameters.Add("UseStandardDistribution", DbType.String).Value = Deck.UseStandardDistribution;
            SQLiteCommand.ExecuteNonQuery();
        }

        public Deck GetDeck(string DeckId)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            Deck Deck = new Deck();
            SQLiteCommand.CommandText = @"SELECT VerbatimDeck.VerbatimDeckId, Name, VerbatimDeck.Description, Author, IdentifiyngToken, Password, UseStandardDistribution, COUNT(*), sum(case when pointvalue = 1 then 1 else 0 end), sum(case when pointvalue = 2 then 1 else 0 end), sum(case when pointvalue = 3 then 1 else 0 end), sum(case when pointvalue = 4 then 1 else 0 end) 
                        FROM VerbatimDeck  
                        LEFT JOIN VerbatimCard ON VerbatimCard.VerbatimDeckId = VerbatimDeck.VerbatimDeckId 					                        
                        WHERE VerbatimDeck.VerbatimDeckId = :VerbatimDeckId";
            SQLiteCommand.Parameters.Add("VerbatimDeckId", DbType.String).Value = DeckId;
            using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            {
                while (SQLiteDataReader.Read())
                {
                    Deck.VerbatimDeckId = SQLiteDataReader.GetInt32(0);
                    Deck.Name = SQLiteDataReader.GetString(1);
                    Deck.Description = SQLiteDataReader.GetString(2);
                    Deck.Author = SQLiteDataReader.GetString(3);
                    Deck.IdentifiyngToken = SQLiteDataReader.GetString(4);
                    Deck.Password = SQLiteDataReader.GetString(5);
                    Deck.UseStandardDistribution = bool.Parse(SQLiteDataReader.GetString(6));
                    Deck.TotalCards = SQLiteDataReader.GetInt32(7);
                    Deck.OnePointTotalCards = SQLiteDataReader.GetInt32(8);
                    Deck.TwoPointTotalCards = SQLiteDataReader.GetInt32(9);
                    Deck.ThreePointTotalCards = SQLiteDataReader.GetInt32(10);
                    Deck.FourPointTotalCards = SQLiteDataReader.GetInt32(11);
                }
            }
            return Deck;
        }

        public List<Deck> GetAllDecks()
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            List<Deck> Decks = new List<Deck>();
            SQLiteCommand.CommandText = @"SELECT Name, Description, Author, IdentifiyngToken, UseStandardDistribution
                        FROM VerbatimDeck";
            using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            {
                while (SQLiteDataReader.Read())
                {
                    Deck Deck = new Deck();
                    Deck.Name = SQLiteDataReader.GetString(0);
                    Deck.Description = SQLiteDataReader.GetString(1);
                    Deck.Author = SQLiteDataReader.GetString(2);
                    Deck.IdentifiyngToken = SQLiteDataReader.GetString(3);
                    Deck.UseStandardDistribution = bool.Parse(SQLiteDataReader.GetString(4));
                    Decks.Add(Deck);
                }
            }
            return Decks;
        }

        public List<Card> GetDeckCards(string DeckId, string Filter)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            List<Card> DeckCards = new List<Card>();
            SQLiteCommand.CommandText = @"SELECT VerbatimCardId, Title, Description, Category, PointValue
                        FROM VerbatimCard
                        WHERE VerbatimDeckId = :VerbatimDeckId
                        AND (
                            Title LIKE @Filter
                            OR Description LIKE @Filter
                            )
                        ORDER BY Title";
            SQLiteCommand.Parameters.Add("VerbatimDeckId", DbType.String).Value = DeckId;
            SQLiteCommand.Parameters.Add("@Filter", DbType.String).Value = "%" + Filter + "%";

            using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            {
                while (SQLiteDataReader.Read())
                {
                    Card Card = new Card();
                    Card.VerbatimCardId = SQLiteDataReader.GetInt32(0);
                    Card.Title = SQLiteDataReader.GetString(1);
                    Card.Description = SQLiteDataReader.GetString(2);
                    Card.Category = SQLiteDataReader.GetString(3);
                    Card.PointValue = SQLiteDataReader.GetInt32(4);
                    DeckCards.Add(Card);
                }
            }
            return DeckCards;
        }

        public List<Deck> SearchForDeck(string Query)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            List<Deck> FoundDecks = new List<Deck>();
            SQLiteCommand.CommandText = @"SELECT VerbatimDeckId, Name, Description, Author, IdentifiyngToken, UseStandardDistribution
                        FROM VerbatimDeck
                        WHERE Name like @Query
                        ORDER BY Name";
            SQLiteCommand.Parameters.Add("@Query", DbType.String).Value = "%" + Query + "%";
            using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            {
                while (SQLiteDataReader.Read())
                {
                    Deck Deck = new Deck();
                    Deck.VerbatimDeckId = SQLiteDataReader.GetInt32(0);
                    Deck.Name = SQLiteDataReader.GetString(1);
                    Deck.Description = SQLiteDataReader.GetString(2);
                    Deck.Author = SQLiteDataReader.GetString(3);
                    Deck.IdentifiyngToken = SQLiteDataReader.GetString(4);
                    Deck.UseStandardDistribution = bool.Parse(SQLiteDataReader.GetString(5));
                    FoundDecks.Add(Deck);

                }
            }
            return FoundDecks;
        }
        public void InsertCard(Card Card)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"INSERT INTO VerbatimCard (VerbatimDeckId, Title, Description, Category, PointValue)
                        VALUES (:VerbatimDeckId,:Title,:Description,:Category,:PointValue)";

            SQLiteCommand.Parameters.Add("VerbatimDeckId", DbType.String).Value = Card.VerbatimDeckId;
            SQLiteCommand.Parameters.Add("Title", DbType.String).Value = Card.Title;
            SQLiteCommand.Parameters.Add("Description", DbType.String).Value = Card.Description;
            SQLiteCommand.Parameters.Add("Category", DbType.String).Value = Card.Category;
            SQLiteCommand.Parameters.Add("PointValue", DbType.String).Value = Card.PointValue;

            SQLiteCommand.ExecuteNonQuery();
        }
        public int InsertDeck(Deck Deck)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"INSERT INTO VerbatimDeck (Name, Description, Author, IdentifiyngToken, Password, UseStandardDistribution)
                        VALUES (:Name,:Description,:Author,:IdentifiyngToken,:Password, :UseStandardDistribution)";

            SQLiteCommand.Parameters.Add("Name", DbType.String).Value = Deck.Name;
            SQLiteCommand.Parameters.Add("Description", DbType.String).Value = Deck.Description;
            SQLiteCommand.Parameters.Add("Author", DbType.String).Value = Deck.Author;
            SQLiteCommand.Parameters.Add("IdentifiyngToken", DbType.String).Value = Deck.IdentifiyngToken;
            SQLiteCommand.Parameters.Add("Password", DbType.String).Value = Deck.Password;
            SQLiteCommand.Parameters.Add("UseStandardDistribution", DbType.String).Value = Deck.UseStandardDistribution;

            SQLiteCommand.ExecuteNonQuery();

            SQLiteCommand.CommandText = "select last_insert_rowid()";
            return Int32.Parse(SQLiteCommand.ExecuteScalar().ToString());
        }
        public void DeleteCard(Card Card)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"DELETE FROM VerbatimCardPlayHistory
                                           WHERE  VerbatimCardId = :VerbatimCardId";
            SQLiteCommand.Parameters.Add("VerbatimCardId", DbType.String).Value = Card.VerbatimCardId;
            SQLiteCommand.ExecuteNonQuery();

            SQLiteCommand.CommandText = @"DELETE FROM VerbatimCard
                                           WHERE  VerbatimCardId = :VerbatimCardId";
            SQLiteCommand.Parameters.Add("VerbatimCardId", DbType.String).Value = Card.VerbatimCardId;
            SQLiteCommand.ExecuteNonQuery();
        }

        public int GetDeckIdByToken(string IdentifiyngToken)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"SELECT VerbatimDeckId FROM VerbatimDeck
                                           WHERE IdentifiyngToken  = :IdentifiyngToken COLLATE NOCASE";
            SQLiteCommand.Parameters.Add("IdentifiyngToken", DbType.String).Value = IdentifiyngToken;
            
            return Int32.Parse(SQLiteCommand.ExecuteScalar().ToString());
        }

        public List<string> GetDeckCategories(string DeckId)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"SELECT DISTINCT Category FROM VerbatimCard
                                           WHERE VerbatimDeckId  = :VerbatimDeckId";
            SQLiteCommand.Parameters.Add("VerbatimDeckId", DbType.String).Value = DeckId;
            List<string> Categories = new List<string>();
            using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            {
                while (SQLiteDataReader.Read())
                {
                    Categories.Add(SQLiteDataReader.GetString(0));
                }
            }
            return Categories;
        }

        public void DeleteOneCardPlayHistory(int CardId, string SteamID)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"DELETE FROM VerbatimCardPlayHistory
                         WHERE VerbatimCardId = :VerbatimCardId
                         AND SteamID = :SteamID";

            SQLiteCommand.Parameters.Add("VerbatimCardId", DbType.Int32).Value = CardId;
            SQLiteCommand.Parameters.Add("SteamID", DbType.String).Value = SteamID;
        
            SQLiteCommand.ExecuteNonQuery();
        }
    }
}
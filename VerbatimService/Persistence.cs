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
                         WHERE VerbatimDeckId = :VerbatimDeckId";
            SQLiteCommand.Parameters.Add("Name", DbType.String).Value = Deck.Name;
            SQLiteCommand.Parameters.Add("Description", DbType.String).Value = Deck.Description;
            SQLiteCommand.Parameters.Add("Author", DbType.String).Value = Deck.Author;
            SQLiteCommand.Parameters.Add("IdentifiyngToken", DbType.String).Value = Deck.IdentifiyngToken;
            SQLiteCommand.Parameters.Add("VerbatimDeckId", DbType.String).Value = Deck.VerbatimDeckId;
            SQLiteCommand.ExecuteNonQuery();
        }

        public Deck GetDeck(string DeckId)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            Deck Deck = new Deck();
            SQLiteCommand.CommandText = @"SELECT VerbatimDeckId, Name, Description, Author, IdentifiyngToken, Password
                        FROM VerbatimDeck
                        WHERE VerbatimDeckId = :VerbatimDeckId";
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
                }
            }
            return Deck;
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
            SQLiteCommand.CommandText = @"SELECT VerbatimDeckId, Name, Description, Author, IdentifiyngToken
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
                    FoundDecks.Add(Deck);

                }
            }
            return FoundDecks;
        }
        public void InsertCard(Card Card)
        {
            SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"INSERT INTO VerbatimCard (Title, Description, Category, PointValue)
                        VALUES (:Title,:Description,:Category,:PointValue)";

            SQLiteCommand.Parameters.Add("Title", DbType.String).Value = Card.Title;
            SQLiteCommand.Parameters.Add("Description", DbType.String).Value = Card.Description;
            SQLiteCommand.Parameters.Add("Category", DbType.String).Value = Card.Category;
            SQLiteCommand.Parameters.Add("PointValue", DbType.String).Value = Card.PointValue;

            SQLiteCommand.ExecuteNonQuery();
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
    }
}
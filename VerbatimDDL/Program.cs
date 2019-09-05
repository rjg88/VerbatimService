using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerbatimDDL
{
    class Program
    {
        //public static List<string> NotConflictRedWords = new List<string> { "its", "it's", "at", "her", "what", "who", "your", "we're", "you're", "they're", "i'm", "by", "their", "than", "it", "as", "they", "you", "his", "&", "from", "with", "in", "of", "on", "you", "too", "to", "that", "the", "and", "but", "for", "nor", "or", "so", "yet", "a", "an", "be", "am", "are", "is", "was", "were", "being", "been", "can", "could", "dare", "do", "does", "did", "have", "has", "had", "having", "may", "might", "must", "need", "ought", "shall", "should", "will", "would" };

        static void Main(string[] args)
        {
            SQLiteConnection Connection = new SQLiteConnection("Data Source=" + "C" + @":\Verbatim\Verbatim.sqlite;Version=3;");
            Connection.Open();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"UPDATE VerbatimDeck SET VerbatimDeckID = 1 WHERE VerbatimDeckID = 28 ";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"UPDATE VerbatimCArd SET VerbatimDeckID = 1 WHERE VerbatimDeckID = 28 ";
            //SQLiteCommand.ExecuteNonQuery();


            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"ALTER TABLE VerbatimCard ADD PictureURL TEXT ";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"INSERT INTO DeckAccess (SteamID, VerbatimDeckId) VALUES (76561198245719610,1)";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"CREATE TABLE Session (SessionId INTEGER PRIMARY KEY, SteamID TEXT NOT NULL, AccessToken TEXT, CreatedDate INT NOT NULL, ExpiryDate INT NOT NULL)";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"CREATE TABLE DeckAccess (DeckAccessId INTEGER PRIMARY KEY, SteamID TEXT NOT NULL, VerbatimDeckId INT NOT NULL, FOREIGN KEY(VerbatimDeckId) REFERENCES VerbatimDeck(VerbatimDeckId))";
            //SQLiteCommand.ExecuteNonQuery();

            // Delete Lobsters play hs
            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"DELETE FROM VerbatimCardPlayhistory WHERE SteamID = 76561198050223251";
            //SQLiteCommand.ExecuteNonQuery();

            //delete broken cards
            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"DELETE FROM VerbatimCard where title is null;";
            //SQLiteCommand.ExecuteNonQuery();


            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"CREATE UNIQUE INDEX ux_VerbatimCard_Title ON VerbatimCard(Title, VerbatimDeckID);";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"CREATE UNIQUE INDEX ux_VerbatimDeck_Token ON VerbatimDeck(IdentifiyngToken);";
            //SQLiteCommand.ExecuteNonQuery();

            #region modify columns

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = "create table VerbatimCard_backup (VerbatimCardId INTEGER PRIMARY KEY, VerbatimDeckId INTEGER, Title TEXT NOT NULL, Description TEXT NOT NULL, Category TEXT NOT NULL, PointValue INTEGER, FOREIGN KEY(VerbatimDeckId) REFERENCES VerbatimDeck(VerbatimDeckId))";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = "INSERT INTO VerbatimCard_backup SELECT * FROM VerbatimCard";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = "DROP TABLE VerbatimCArd";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = "create table VerbatimCard (VerbatimCardId INTEGER PRIMARY KEY, VerbatimDeckId INTEGER, Title TEXT NOT NULL, Description TEXT NOT NULL, Category TEXT NOT NULL, PointValue INTEGER, FOREIGN KEY(VerbatimDeckId) REFERENCES VerbatimDeck(VerbatimDeckId))";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = "INSERT INTO VerbatimCard SELECT * FROM VerbatimCard_Backup";
            //SQLiteCommand.ExecuteNonQuery();

            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = "DROP TABLE VerbatimCard_Backup";
            //SQLiteCommand.ExecuteNonQuery();
            #endregion


            //SQLiteCommand.CommandText = @"ALTER TABLE VerbatimDeck ADD UseStandardDistribution text";
            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"SELECT VerbatimCard.Title, SteamId FROM VerbatimCardPlayHistory Inner JOIN VerbatimCard ON VerbatimCard.VerbatimCardId = VerbatimCardPlayHistory.VerbatimCardId
            //                            INNER JOIN VerbatimDeck on VerbatimDeck.VerbatimDeckId = 1";
            //using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            //{
            //    while (SQLiteDataReader.Read())
            //    {
            //        File.AppendAllText(@"C:\Verbatim\test\historyTEst.txt", SQLiteDataReader.GetString(0) + "|" + SQLiteDataReader.GetString(1) + "\n");
            //    }
            //}
            SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);

            foreach (string line in File.ReadAllLines(@"C:\Verbatim\test\historyTEst.txt"))
            {
                List<string> vals = line.Split('|').ToList();
                // select card by title
                SQLiteCommand.CommandText = @"Select VerbatimCardId from VerbatimCard where VerbatimDeckId = 1 AND VerbatimCard.title = '" + vals[0].Replace("'", "''") + "'";
                var val = SQLiteCommand.ExecuteScalar();
                string VerbatimCardId;
                if (val != null)
                {
                    VerbatimCardId = val.ToString();
                    //insert
                    SQLiteCommand.CommandText = @"INSERT INTO VerbatimCardPlayHistory (VerbatimCardId, SteamId) VALUES (" + VerbatimCardId + ", " + vals[1] + ")";
                    SQLiteCommand.ExecuteNonQuery();
                }

            }

        }
    }
}

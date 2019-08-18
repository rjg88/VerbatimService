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
        public static List<string> NotConflictRedWords = new List<string> { "its", "it's", "at", "her", "what", "who", "your", "we're", "you're", "they're", "i'm", "by", "their", "than", "it", "as", "they", "you", "his", "&", "from", "with", "in", "of", "on", "you", "too", "to", "that", "the", "and", "but", "for", "nor", "or", "so", "yet", "a", "an", "be", "am", "are", "is", "was", "were", "being", "been", "can", "could", "dare", "do", "does", "did", "have", "has", "had", "having", "may", "might", "must", "need", "ought", "shall", "should", "will", "would" };

        static void Main(string[] args)
        {
            SQLiteConnection Connection = new SQLiteConnection("Data Source=" + "C" + @":\Verbatim\Verbatim.sqlite;Version=3;");
            Connection.Open();

            // Delete Lobsters play hs
            //SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"DELETE FROM VerbatimCardPlayhistory WHERE SteamID = 76561198050223251";
            //SQLiteCommand.ExecuteNonQuery();

            //delete broken cards
            SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            SQLiteCommand.CommandText = @"DELETE FROM VerbatimCard where title is null;";
            SQLiteCommand.ExecuteNonQuery();


            //SQLiteCommand.CommandText = @"ALTER TABLE VerbatimDeck ADD UseStandardDistribution text";
            //SQLiteCommand.CommandText = @"SELECT TITLE, Description FROM VerbatimCard WHERE VerbatimDeckId = 1";
            //using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            //{
            //    while (SQLiteDataReader.Read())
            //    {
            //        string title = SQLiteDataReader.GetString(0);
            //        string decription = SQLiteDataReader.GetString(1);
            //        List<string> redwords = DetectWordInString(decription, title);
            //        if (redwords.Count > 0)
            //        {
            //            if (redwords[0].Trim() != "")
            //                File.AppendAllText("test.txt", "Title:" + title + " In Desc:" + redwords[0] + "\n");
            //        }
            //    }
            //}

            //SQLiteCommand.ExecuteNonQuery();


        }
    }
}

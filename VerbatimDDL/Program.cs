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

            SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);
            //SQLiteCommand.CommandText = @"DELETE FROM VerbatimCard WHERE VerbatimDECKID = 17";
            //SQLiteCommand.ExecuteNonQuery();


            SQLiteCommand.CommandText = @"ALTER TABLE VerbatimDeck ADD UseStandardDistribution text";
            SQLiteCommand.CommandText = @"SELECT TITLE, Description FROM VerbatimCard WHERE VerbatimDeckId = 1";
            using (SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader())
            {
                while (SQLiteDataReader.Read())
                {
                    string title = SQLiteDataReader.GetString(0);
                    string decription = SQLiteDataReader.GetString(1);
                    List<string> redwords = DetectWordInString(decription, title);
                    if (redwords.Count > 0)
                    {
                        if (redwords[0].Trim() != "")
                            File.AppendAllText("test.txt", "Title:" + title + " In Desc:" + redwords[0] + "\n");
                    }
                }
            }


            SQLiteCommand.ExecuteNonQuery();


        }
        private static List<string> DetectWordInString(string Description, string Title)
        {
            List<string> Detected = new List<string>();
            Description = Description.Replace(",", "");
            Description = Description.Replace("\"", "");

            List<string> DescriptionWords = Description.Split(' ').ToList();
            List<string> TitleWords = Title.Split(' ').ToList();

            foreach (string DescriptionWord in DescriptionWords)
                foreach (string TitleWord in TitleWords)
                    if (DescriptionWord.ToLower() == TitleWord.ToLower() && !NotConflictRedWords.Contains(TitleWord.ToLower()))
                        Detected.Add(TitleWord);
            return Detected;

        }
    }
}

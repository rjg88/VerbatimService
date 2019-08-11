using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerbatimDDL
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection Connection = new SQLiteConnection("Data Source=" + "C" + @":\Verbatim\Verbatim.sqlite;Version=3;");
            Connection.Open();

            SQLiteCommand SQLiteCommand = new SQLiteCommand(Connection);

            SQLiteCommand.CommandText = @"DELETE FROM VerbatimCard where VerbatimCardId = 3142";

            SQLiteCommand.ExecuteNonQuery();
        }
    }
}

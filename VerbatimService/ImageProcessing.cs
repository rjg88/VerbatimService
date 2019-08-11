using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerbatimService
{
    class ImageProcessing
    {
        private static Font StringFontDesc = new Font("Roboto Medium", 20, FontStyle.Regular);
        private static Font StringFontCat = new Font("Proxima Nova Soft", 25, FontStyle.Regular);
        private static Font StringFontTitle = new Font("Proxima Nova Soft", 40, FontStyle.Bold);
        private static StringFormat StringFormat = new StringFormat();
        private static StringFormat StringFormatDesc = new StringFormat();
        private static Image TemplateImage1;
        private static Image TemplateImage2;
        private static Image TemplateImage3;
        private static Image TemplateImage4;
        private static List<Bitmap> CardImages = new List<Bitmap>();
        private static List<float> Distribution = new List<float> { 0.25f, 0.4f, 0.25f, 0.1f };
        public SQLiteConnection Connection;
        private int UnixTimeStamp;
        public static string DriveLetter;

        private static readonly int CardWidth = 663;
        private static readonly int CardHeight = 1001;

        public List<Card> CardObjects = new List<Card>();
        public Persistence Persistence = new Persistence();
        private int DeckId = -1;


        public string CreateDeck(int DeckSize, List<string> SteamIds, string Token)
        {
            Initialize(); 

            DeleteOldFiles();

            List<int> CardCounts = FixDistribution(Distribution, DeckSize);

            if (string.IsNullOrEmpty(Token))
                DeckId = 1;
            else
                DeckId = Persistence.GetDeckIdByToken(Token);

            if (DeckId < 1)
                return "";


            GenerateCards(CardCounts, SteamIds, DeckId);

            foreach (Card Card in CardObjects)
                CardImages.Add(GenerateImage(Card));

            Bitmap SheetBitMap = CreateSheetFromCards();
            string FileName = DateTime.Now.ToString("MM-dd-yyyy-HH：mm：ss") + ".png";
            string CurrentDirectory = "";
            if (DeckId == 1)
                CurrentDirectory = DriveLetter + @":\inetpub\wwwroot\Verbatim\Sheets\Original\";
            else
            {
                if (!Directory.Exists(DriveLetter + @":\inetpub\wwwroot\Verbatim\Sheets\" + Token + "\\"))
                    Directory.CreateDirectory(DriveLetter + @":\inetpub\wwwroot\Verbatim\Sheets\" + Token + "\\");
                    
                CurrentDirectory = DriveLetter + @":\inetpub\wwwroot\Verbatim\Sheets\" + Token + "\\";
            }

            string FullPictureFileName = CurrentDirectory + "\\" + FileName;

            SheetBitMap.Save(FullPictureFileName);

            return FileName;
        }

        private void GenerateCards(List<int> CardCounts, List<string> SteamIDs, int DeckId)
        {
            int count = 1;

            string SteamIDList = "";
            foreach (string SteamID in SteamIDs)
            {
                SteamIDList += "'" + SteamID + "',";
            }
            if(SteamIDs.Count > 0)
                SteamIDList = SteamIDList.Substring(0, SteamIDList.Length-1);

            //SELECT VerbatimCard.VerbatimCardId, Title, Description, Category, PointValue 
            //FROM VerbatimCard
            //    LEFT JOIN VerbatimCardPlayHistory 
            //        ON VerbatimCard.VerbatimCardId = VerbatimCardPlayHistory.VerbatimCardId
            //        AND VerbatimCardPlayHistory.SteamID IN ('test')
            //WHERE VerbatimDeckId = 1
            //AND PointValue = 1
            //AND VerbatimCardPlayHistory.VerbatimCardId IS NULL
            //ORDER BY RANDOM() LIMIT 10


            string TemplateSQL = @"		SELECT VerbatimCard.VerbatimCardId, Title, Description, Category, PointValue, 0 as rowOrder, RANDOM() as Random
								FROM VerbatimCard 
								LEFT JOIN VerbatimCardPlayHistory
								ON VerbatimCardPlayHistory.VerbatimCardId = VerbatimCard.VerbatimCardId
								AND VerbatimCardPlayHistory.SteamID IN({0})
								WHERE VerbatimDeckId = {3}
								AND PointValue = {1}   
								AND VerbatimCardPlayHistory.VerbatimCardId IS NULL
								UNION
								SELECT VerbatimCard.VerbatimCardId, Title, Description, Category, PointValue, COUNT(*) as rowOrder, RANDOM() as Random
								FROM VerbatimCard 
								INNER JOIN VerbatimCardPlayHistory
								ON VerbatimCardPlayHistory.VerbatimCardId = VerbatimCard.VerbatimCardId
								WHERE VerbatimDeckId = {3}
								AND VerbatimCardPlayHistory.SteamID IN({0})
								AND PointValue = {1}      
								GROUP BY VerbatimCard.VerbatimCardId, Title, Description, Category, PointValue
								ORDER BY rowOrder ASC, RANDOM()
								LIMIT {2}";

            foreach (int CardCount in CardCounts)
            {
                string SelectSQL = String.Format(TemplateSQL, SteamIDList, count, CardCount, DeckId);
                //string sql = "SELECT VerbatimCardId, Title, Description, Category, PointValue FROM VerbatimCard WHERE VerbatimDeckId = 1 AND PointValue = " + count + " ORDER BY RANDOM() LIMIT " + CardCount;
                SQLiteCommand Command = new SQLiteCommand(SelectSQL, Connection);
                using (SQLiteDataReader SQLiteDataReader = Command.ExecuteReader())
                {
                    while (SQLiteDataReader.Read())
                    {
                        Card Card = new Card();
                        Card.VerbatimCardId = SQLiteDataReader.GetInt32(0);
                        Card.Title = SQLiteDataReader.GetString(1);
                        Card.Description = SQLiteDataReader.GetString(2);
                        Card.Category = SQLiteDataReader.GetString(3);
                        Card.PointValue = SQLiteDataReader.GetInt32(4);
                        CardObjects.Add(Card);

                        string UpsertSQLSelect = @"SELECT VerbatimCardId
								            FROM VerbatimCardPlayHistory
								            WHERE VerbatimCardId = {0}
								            AND SteamID = '{1}'";
                        string InsertSQL = @"INSERT INTO VerbatimCardPlayHistory (VerbatimCardId, SteamID, DateTimeUsed)
								             VALUES ({0},'{1}',{2})";
                        string UpdateSQL = @"UPDATE VerbatimCardPlayHistory
								            SET DateTimeUsed = {2}
								            WHERE VerbatimCardId = {0}
								            AND SteamID = '{1}'";
                        // UPSERT INTO VerbatimCardPlayHistory
                        foreach (string SteamID in SteamIDs)
                        {
                            SQLiteCommand UpsertSelectCommand = new SQLiteCommand(String.Format(UpsertSQLSelect, Card.VerbatimCardId, SteamID), Connection);
                            if(UpsertSelectCommand.ExecuteScalar() == null)
                            {

                                SQLiteCommand InsertCommand = new SQLiteCommand(String.Format(InsertSQL, Card.VerbatimCardId, SteamID, UnixTimeStamp), Connection);
                                InsertCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                SQLiteCommand UpdateCommand = new SQLiteCommand(String.Format(UpdateSQL, Card.VerbatimCardId, SteamID, UnixTimeStamp), Connection);
                                UpdateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                count++;
            }


        }

        private static Bitmap CreateSheetFromCards()
        {
            Bitmap SheetBitMap = new Bitmap(CardWidth * 10, CardHeight * 7);
            using (Graphics Graphics = Graphics.FromImage(SheetBitMap))
            {
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 10; j++)
                        if ((i * 10) + j != 69)
                        {
                            if (CardImages.Count - 1 < (i * 10) + j)
                                break;
                            Graphics.DrawImage(CardImages[(i * 10) + j], j * CardWidth, i * CardHeight);
                        }

            }

            return SheetBitMap;
        }

        private void Initialize()
        {
            TemplateImage1 = Image.FromFile(DriveLetter + @":\Verbatim\Template1.png");
            TemplateImage2 = Image.FromFile(DriveLetter + @":\Verbatim\Template2.png");
            TemplateImage3 = Image.FromFile(DriveLetter + @":\Verbatim\Template3.png");
            TemplateImage4 = Image.FromFile(DriveLetter + @":\Verbatim\Template4.png");

            StringFormat.LineAlignment = StringAlignment.Center;
            StringFormat.Alignment = StringAlignment.Center;

            StringFormatDesc.LineAlignment = StringAlignment.Near;
            StringFormatDesc.Alignment = StringAlignment.Center;

            Connection = new SQLiteConnection("Data Source=" + DriveLetter + @":\Verbatim\Verbatim.sqlite;Version=3;");
            Connection.Open();
            CardImages = new List<Bitmap>();
            CardObjects = new List<Card>();

            UnixTimeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            Persistence.Connection = Connection;
        }

        private static Bitmap GenerateImage(Card Card)
        {
            Bitmap OutPutImage;

            if (Card.PointValue == 1)
                OutPutImage = new Bitmap(TemplateImage1);
            else if (Card.PointValue == 2)
                OutPutImage = new Bitmap(TemplateImage2);
            else if (Card.PointValue == 3)
                OutPutImage = new Bitmap(TemplateImage3);
            else if (Card.PointValue == 4)
                OutPutImage = new Bitmap(TemplateImage4);
            else
                return null;

            using (Graphics graphics = Graphics.FromImage(OutPutImage))
            {
                //                graphics.DrawString("testtesttesttesttesttesttesttesttestt esttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttes ttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttes ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest ttesttesttesttest end", StringFontDesc, Brushes.Black,

                // add title
                graphics.DrawString(Card.Title, StringFontTitle, Brushes.Black,
                                     new Rectangle(35, 35, OutPutImage.Size.Width - 70, 250), StringFormat);
                // add desc
                graphics.DrawString(Card.Description, StringFontDesc, Brushes.Black,
                     new Rectangle(35, 300, OutPutImage.Size.Width - 70, 500), StringFormatDesc);

                // add category
                graphics.DrawString(Card.Category, StringFontCat, Brushes.Black,
                     new Rectangle(35, 740, OutPutImage.Size.Width - 70, 70), StringFormat);


            }
            return OutPutImage;
        }

        private static List<int> FixDistribution(List<float> Distribution, int DeckSize)
        {
            // 25%, 40%, 25%, 10%

            // 5 8 5 2

            // FLOOR ALL 
            // SAVE FLOORS
            // SUM THIS AND GET DELTA WITH N
            // DISTRUBTE ACROSS FLOORS

            List<RoundedValue> RoundedValues = new List<RoundedValue>();
            foreach (float Percentage in Distribution)
            {
                // ROUND ALL 

                RoundedValue RoundedValue = new RoundedValue();
                float FuzzyNumber = Percentage * DeckSize;
                RoundedValue.RoundedInt = (int)Math.Round(FuzzyNumber);

                // SAVE ROUNDS POLARITY
                if (FuzzyNumber != Math.Floor(FuzzyNumber))
                {
                    RoundedValue.WasRounded = true;
                    if ((int)FuzzyNumber == RoundedValue.RoundedInt)
                        RoundedValue.Polarity = -1;
                    else
                        RoundedValue.Polarity = 1;
                }
                else
                {
                    RoundedValue.WasRounded = false;
                }
                RoundedValues.Add(RoundedValue);
            }

            // SUM THIS AND GET DELTA WITH N
            int Sum = 0;
            foreach (RoundedValue RoundedValue in RoundedValues)
            {
                Sum += RoundedValue.RoundedInt;
            }
            List<int> FinalValues = new List<int>();
            if(Sum == DeckSize)
            {
                foreach (RoundedValue RoundedValue in RoundedValues)
                {
                    FinalValues.Add(RoundedValue.RoundedInt);
                }
            }
            // ADD OR SUB ACROSS ROUNDS
            else
            {
                int Delta = Sum - DeckSize;
                foreach (RoundedValue RoundedValue in RoundedValues)
                {
                    if(!RoundedValue.WasRounded)
                        FinalValues.Add(RoundedValue.RoundedInt);
                    else
                    {
                        if (RoundedValue.Polarity == -1 && Delta < 0)
                        {
                            FinalValues.Add(RoundedValue.RoundedInt + 1);
                            Delta++;
                        }
                        else if (RoundedValue.Polarity == 1 && Delta > 0)
                        {
                            FinalValues.Add(RoundedValue.RoundedInt - 1);
                            Delta--;
                        }
                        else
                            FinalValues.Add(RoundedValue.RoundedInt);

                    }
                }
            }
            return FinalValues;
        }

        private static void DeleteOldFiles()
        {
            List<string> Files = Directory.GetFiles(DriveLetter + @":\inetpub\wwwroot\Verbatim\Sheets\Original\").ToList();
            foreach (string SheetFile in Files)
            {
                if (Directory.GetCreationTime(SheetFile) < DateTime.Now.AddMinutes(-300))
                    File.Delete(SheetFile);
            }

        }
    }
}

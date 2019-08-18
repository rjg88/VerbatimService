using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VerbatimWeb
{
    public class Utilities
    {
        public static MemoryStream MakeGETRequestStream(string uri)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())

            {
                MemoryStream memStream;
                using (Stream ResponseStream = response.GetResponseStream())
                {
                    memStream = new MemoryStream();

                    byte[] buffer = new byte[1024];
                    int byteCount;
                    do
                    {
                        byteCount = ResponseStream.Read(buffer, 0, buffer.Length);
                        memStream.Write(buffer, 0, byteCount);
                    } while (byteCount > 0);
                }

                // If you're going to be reading from the stream afterwords you're going to want to seek back to the beginning.
                memStream.Seek(0, SeekOrigin.Begin);
                return memStream;
            }

        }
        public static string MakeGETRequest(string uri)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
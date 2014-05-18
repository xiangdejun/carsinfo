using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using HTMLParser_ns;

namespace Carsinfo.BusinessInfrastructure_ns
{
    public class HtmlAnalyzer
    {
        /// <summary>
        ///  Get Json Data from specific HTML, Trim the start and end.
        /// </summary>
        public static string GetJsonContent(string html)
        {
            string strJsonData = String.Empty;

            if (html == null || html == string.Empty)
            {
                return strJsonData;
            }

            var parser = new HTMLparser(html, Encoding.UTF8);

            HTMLchunk chunk = parser.ParseNext();

            HTMLchunk oChunk = chunk;

            int scriptCount = 0;

            do
            {
                chunk = parser.ParseNext();

                if (chunk == null)
                {
                    return string.Empty;
                }


                if (chunk.sTag == "script" && chunk.oType == HTMLchunkType.Script)
                {
                    scriptCount++;
                    if (scriptCount == 7)
                    {
                        strJsonData = chunk.oHTML.TrimStart();
                        break;
                    }
                }
            } while (true);

            return strJsonData.Trim();
        }

        /// <summary>
        ///  Get Html from specific web page
        /// </summary>
        public static string GetHtml(string url)
        {
            string strHtml = String.Empty;

            var webClient = new WebClient();
            webClient.Headers.Add("Accept-Encoding", "gzip, deflate");
            var byteArray = webClient.DownloadData(url);

            string sContentEncoding = webClient.ResponseHeaders["Content-Encoding"];
            if (sContentEncoding == "gzip")
            {
                MemoryStream ms = new MemoryStream(byteArray);
                MemoryStream msTemp = new MemoryStream();
                int count = 0;
                GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                byte[] buf = new byte[1000];
                while((count = gzip.Read(buf, 0, buf.Length)) > 0)
                { 
                    msTemp.Write(buf, 0, count);
                }
                byteArray = msTemp.ToArray();
            } // end-gzip
            string sHtml = Encoding.GetEncoding(936).GetString(byteArray);

            return sHtml;
        }

        /// <summary>
        /// Read content from a file
        /// </summary>
        public static string ReadFile(string filePath, Encoding encoding = null)
        {
            string strContent = "";

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            strContent = File.ReadAllText(filePath, encoding);

            return strContent;
        }

        public static void SaveText(string content, string saveTo, Encoding encoding = null)
        {
            File.WriteAllText(saveTo, content);
        }
    }
}
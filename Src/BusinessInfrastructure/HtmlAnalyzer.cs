using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using HTMLParser_ns;
using CarsInfo.DataModel_ns;

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
                while ((count = gzip.Read(buf, 0, buf.Length)) > 0)
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

        private static string strLastCategory = "";
        private static string strLastClass = "";

        public static List<CarShort> GetCarListWithPrice(string html)
        {
            var cars = new List<CarShort>();

            var parser = new HTMLparser(html, Encoding.UTF8);

            HTMLchunk chunk = parser.ParseNext();

            //HTMLchunk oChunk = chunk;

            do
            {
                chunk = parser.ParseNext();
                if (chunk == null)
                {
                    return null;
                }

                var id = chunk.GetParamValue("id");

                if (id.StartsWith("box"))
                {
                    strLastCategory = id.Replace("box", "");
                }

                if (!string.IsNullOrEmpty(strLastCategory) && chunk.sTag == "dl")
                {
                    string strCategoryName = "";


                    chunk = parser.ParseNextByTagName("dt");
                    chunk = parser.ParseNextByTagName("div");
                    chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                    chunk = parser.ParseNext();
                    if (chunk == null) break;

                    if (chunk.oType == HTMLchunkType.Text)
                    {
                        strCategoryName = chunk.oHTML.Trim();

                        var carCategory = new CarsCategory
                        {
                            BrandName = strCategoryName,
                            BeginLetter = strLastCategory,
                        };

                        do
                        {
                            chunk = parser.ParseNext();

                            if (chunk.sTag == "li" && chunk.oType == HTMLchunkType.OpenTag)
                            {

                                string strCarName = "";
                                string strRetailPrice = "";
                                chunk = parser.ParseNextByTagName("h4");
                                chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                                chunk = parser.ParseNext();
                                if (chunk.oType == HTMLchunkType.Text)
                                {
                                    strCarName = chunk.oHTML.Trim();
                                    chunk = parser.ParseNextByTagName("div");
                                    chunk = parser.ParseNext();
                                    if (chunk.oType == HTMLchunkType.Text)
                                    {
                                        chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                                        chunk = parser.ParseNext();
                                        if (chunk.oType == HTMLchunkType.Text)
                                        {
                                            strRetailPrice = chunk.oHTML.Trim();
                                        }
                                    }
                                }

                                var car = new CarShort
                                {
                                    CarName = strCarName,
                                    RetailPrice = strRetailPrice
                                };
                                cars.Add(car);
                            }
                        } while (!(chunk.sTag == "dd" && chunk.oType == HTMLchunkType.CloseTag));

                        //lstCarsCategory.Add(carCategory);
                    }
                }


            } while (!(chunk.oType == HTMLchunkType.CloseTag && chunk.sTag == "body"));

            return cars;
        }

        public static List<CarsCategory> GetCarListWithFactory(String html)
        {
            var lstCarsCategory = new List<CarsCategory>();

            var parser = new HTMLparser(html, Encoding.UTF8);

            HTMLchunk chunk = parser.ParseNext();

            //HTMLchunk oChunk = chunk;

            do
            {
                chunk = parser.ParseNext();
                if (chunk == null)
                {
                    return null;
                }

                var id = chunk.GetParamValue("id");

                if (id.StartsWith("tab"))
                {
                    strLastCategory = id.Replace("tab", "");
                }

                if (!string.IsNullOrEmpty(strLastCategory) && chunk.sTag == "dl" && chunk.oType== HTMLchunkType.OpenTag) 
                {
                    string strCategoryName = "";


                    chunk = parser.ParseNextByTagNameAndType("dt", HTMLchunkType.OpenTag);
                    //chunk = parser.ParseNextByTagNameAndType("p", HTMLchunkType.OpenTag);
                    chunk = parser.ParseNextByTagNameAndType("p", HTMLchunkType.OpenTag);
                    chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                    chunk = parser.ParseNextByTagNameAndType("img", HTMLchunkType.OpenTag);
                    chunk = parser.ParseNextByTagNameAndType("p", HTMLchunkType.OpenTag);
                    chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);

                    chunk = parser.ParseNext();
                    if (chunk == null) break;

                    if (chunk.oType == HTMLchunkType.Text)
                    {
                        strCategoryName = chunk.oHTML.Trim();

                        var carCategory = new CarsCategory
                        {
                            BrandName = strCategoryName,
                            BeginLetter = strLastCategory,
                        };


                        string strFactoryName = "";

                        chunk = parser.ParseNextByTagName("dd");

                        do
                        {
                            chunk = parser.ParseNext();
                            if (chunk.sTag == "h3" && chunk.oType == HTMLchunkType.OpenTag)
                            {
                                chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                                chunk = parser.ParseNext();
                                if (chunk.oType == HTMLchunkType.Text)
                                {
                                    strFactoryName = chunk.oHTML.Trim();
                                }
                                chunk = parser.ParseNextByTagNameAndType("ul", HTMLchunkType.OpenTag);
                            }
                            else
                            {
                                strFactoryName = strCategoryName;
                            }

                            if (chunk.sTag == "ul" && chunk.oType == HTMLchunkType.OpenTag)
                            {

                                do
                                {
                                    chunk = parser.ParseNext();

                                    if (chunk.sTag == "li" && chunk.oType == HTMLchunkType.OpenTag)
                                    {

                                        string strCarName = "";
                                        string strRetailPrice = "";
                                        chunk = parser.ParseNextByTagName("h4");
                                        chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                                        chunk = parser.ParseNext();
                                        if (chunk.oType == HTMLchunkType.Text)
                                        {
                                            strCarName = chunk.oHTML.Trim();
                                            //chunk = parser.ParseNextByTagName("div");
                                            //chunk = parser.ParseNext();
                                            //if (chunk.oType == HTMLchunkType.Text)
                                            //{
                                            //    chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                                            //    chunk = parser.ParseNext();
                                            //    if (chunk.oType == HTMLchunkType.Text)
                                            //    {
                                            //        strRetailPrice = chunk.oHTML.Trim();
                                            //    }
                                            //}
                                        }

                                        var car = new CarShort
                                        {
                                            CarName = strCarName,
                                            RetailPrice = strRetailPrice,
                                            FactoryName = strFactoryName
                                        };
                                        carCategory.Cars.Add(car);
                                    }
                                } while (!(chunk.sTag == "ul" && chunk.oType == HTMLchunkType.CloseTag));

                            }

                        } while (!(chunk.sTag == "dd" && chunk.oType == HTMLchunkType.CloseTag));

                        lstCarsCategory.Add(carCategory);
                    }
                }

            } while (!(chunk.oType == HTMLchunkType.CloseTag && chunk.sTag == "body"));

            return lstCarsCategory;
        }

        public static List<CarShort> GetCarListWithClass(string html)
        {
            var cars = new List<CarShort>();

            var parser = new HTMLparser(html, Encoding.UTF8);

            HTMLchunk chunk = parser.ParseNext();

            //HTMLchunk oChunk = chunk;

            do
            {
                chunk = parser.ParseNext();
                if (chunk == null)
                {
                    return null;
                }

                var id = chunk.GetParamValue("id");

                if (id.StartsWith("tab"))
                {

                    chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);

                    chunk = parser.ParseNext();
                    if (chunk == null) break;

                    if (chunk.oType == HTMLchunkType.Text)
                    {
                        strLastClass = chunk.oHTML.Trim();

                        //var carshort = new CarShort()
                        //{
                        //    ClassName = strLastClass
                        //};
                    }
                }

                if (chunk.sTag == "dl" && chunk.oType == HTMLchunkType.OpenTag)
                {
                        chunk = parser.ParseNextByTagName("dd");

                        do
                        {
                            chunk = parser.ParseNext();

                            if (chunk.sTag == "ul" && chunk.oType == HTMLchunkType.OpenTag)
                            {

                                do
                                {
                                    chunk = parser.ParseNext();

                                    if (chunk.sTag == "li" && chunk.oType == HTMLchunkType.OpenTag)
                                    {

                                        string strCarName = "";
                                        string strRetailPrice = "";
                                        chunk = parser.ParseNextByTagName("h4");
                                        chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                                        chunk = parser.ParseNext();
                                        if (chunk.oType == HTMLchunkType.Text)
                                        {
                                            strCarName = chunk.oHTML.Trim();
                                            //chunk = parser.ParseNextByTagName("div");
                                            //chunk = parser.ParseNext();
                                            //if (chunk.oType == HTMLchunkType.Text)
                                            //{
                                            //    chunk = parser.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
                                            //    chunk = parser.ParseNext();
                                            //    if (chunk.oType == HTMLchunkType.Text)
                                            //    {
                                            //        strRetailPrice = chunk.oHTML.Trim();
                                            //    }
                                            //}
                                        }

                                        var car = new CarShort
                                        {
                                            CarName = strCarName,
                                            RetailPrice = strRetailPrice,
                                            ClassName = strLastClass
                                        };
                                        cars.Add(car);
                                    }
                                } while (!(chunk.sTag == "ul" && chunk.oType == HTMLchunkType.CloseTag));

                            }

                        } while (!(chunk.sTag == "dd" && chunk.oType == HTMLchunkType.CloseTag));
                    }

            } while (!(chunk.oType == HTMLchunkType.CloseTag && chunk.sTag == "body"));

            return cars;
        }

        public static string GetSerieName(string html)
        {
            var op = new HTMLparser(html, Encoding.UTF8);

            HTMLchunk  oc;

            do
            {
                oc = op.ParseNext();

                if (oc.oHTML.Trim().Replace("：", "") == "当前位置")
                {

                    oc = GetTagByIndex(op, 9);
                    var carClassName = oc.oHTML.Trim();

                    oc = GetTagByIndex(op, 7);
                    var carName = oc.oHTML.Trim();

                    return carName;
                    //oc = GetTagByIndex(op, 4);
                    //subCarName = oc.oHTML.Trim();

                    break;
                }

            } while (true);
            
        }

        public static HTMLchunk GetTagByIndex(HTMLparser parser, int tagIndex)
        {
            HTMLchunk chunk;

            do
            {
                tagIndex--;

                chunk = parser.ParseNext();

            } while (tagIndex != 0);

            return chunk;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using HTMLParser_ns;
using System.IO;
using Newtonsoft.Json;


namespace HtmlAnalyzer_ns
{


    //public class HtmlAnalyzer
    //{
    //    public static List<string> pGroup = new List<string>();
    //    public static List<string> pList = new List<string>();

    //    public static Dictionary<UInt16, CarParams> GetFullPage(string url)
    //    {
    //        #region 获得网页的html
    //        try
    //        {
    //            //Dictionary<UInt16, CarParams> carList = new Dictionary<ushort, CarParams>();
    //            string html = String.Empty;

    //            string testFile = @"..\..\..\Test\TestCases\a1_gb2312.txt";
    //            FileInfo fi = new FileInfo(testFile);
    //            if (!fi.Exists)
    //            {
    //                WebClient webClient = new WebClient();
    //                webClient.Encoding = System.Text.Encoding.GetEncoding("gb2312");
    //                html = webClient.DownloadString(url);

    //                System.IO.StreamWriter sr = System.IO.File.CreateText(testFile);
    //                sr.Write(html);
    //                sr.Close();
    //            }
    //            else
    //            {
    //                StreamReader sr = new StreamReader(testFile, System.Text.Encoding.UTF8);
    //                html = sr.ReadToEnd();
    //            }

    //            HTMLparser parser = new HTMLparser(html, Encoding.UTF8);
    //            //GetHeader(parser, null);
    //            HTMLchunk chunk = parser.ParseNext();
    //            carList = parserTR(ref parser, chunk);

    //            return carList;
    //        }
    //        catch (Exception ex)
    //        {
    //            //throw;
    //            return null;
    //        }
    //        #endregion
    //    }

    //    public static Dictionary<UInt16, string> GetNewCarList()
    //    {
    //        #region 获得网页的html
    //        try
    //        {
    //            Dictionary<UInt16, string> carList = new Dictionary<ushort, string>();
    //            //WebClient webClient = new WebClient();
    //            //webClient.Encoding = System.Text.Encoding.Default;
    //            //string html = webClient. DownloadString(url);
    //            string html = System.IO.File.ReadAllText(@"..\..\..\html.txt", Encoding.Default);

    //            HTMLparser parser = new HTMLparser(html);
    //            //GetHeader(parser, null);

    //            HTMLchunk chunk = parser.ParseNext();
    //            carList = parserTR_New(ref parser, chunk);

    //            return carList;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        #endregion
    //    }

    //    private static Dictionary<ushort, string> parserTR_New(ref HTMLparser op, HTMLchunk oc)
    //    {
    //        //Get Mfc Name by the html
    //        string mfcName = "";

    //        //Get Car Name by the html
    //        string carName = "";

    //        //Get Car Class Name by the html
    //        string carClassName = "";

    //        //Get Sub Car Name by the html
    //        string subCarName = "";

    //        //Get Sub Car Price by the html
    //        decimal price = 0;

    //        //#1 Get BrandID
    //        int brandID = 0; ;

    //        XmlDocument xml = new XmlDocument();

    //        HTMLchunk oChunk = oc;

    //        bool finished = false;

    //        Boolean tableContent = false;

    //        Boolean OptionTable = false;
    //        Boolean ConfigTable = false;

    //        Boolean CarAdded = false;

    //        string currentParamName = "";

    //        //当前参数块索引
    //        Int16 currentParamPartIndex = -1;

    //        //当前具体参数项索引
    //        UInt16 currentParamItemIndex = 0;

    //        string tagText = "";

    //        UInt16 colunmNum = 0;
    //        UInt16 rowNum = 0;

    //        //carParam.Struct = "涡轮";

    //        //string x = carParam.Struct;

    //        Dictionary<UInt16, string> carList = new Dictionary<UInt16, string>();

    //        #region GetHeader

    //        do
    //        {
    //            oc = op.ParseNext();

    //            if (oc.oHTML.Trim().Replace("：", "") == "当前位置")
    //            {
    //                oc = GetTagByIndex(op, 6);
    //                carClassName = oc.oHTML.Trim();

    //                oc = GetTagByIndex(op, 4);
    //                carName = oc.oHTML.Trim();

    //                //oc = GetTagByIndex(op, 4);
    //                //subCarName = oc.oHTML.Trim();

    //                break;
    //            }

    //        } while (true);

    //        #endregion

    //        do
    //        {
    //            if (finished)
    //            {
    //                break;
    //            }

    //            #region OptionTable

    //            if (oChunk.sTag == "table" && oChunk.oType == HTMLchunkType.OpenTag && oChunk.GetParamValue("id") == "OptionTable")
    //            {
    //                do
    //                {
    //                    oChunk = op.ParseNext();

    //                    //找到参数头开始的 tr
    //                    if (oChunk.sTag == "tr" && oChunk.oType == HTMLchunkType.OpenTag && oChunk.GenerateParamsHTML() == "")
    //                    {
    //                        HTMLchunkType tTpye = oChunk.oType;
    //                        //标题行开始
    //                        oChunk = op.ParseNextByTagNameAndType(oChunk.sTag, oChunk.oType);

    //                        //指向下一个参数块
    //                        currentParamPartIndex++;
    //                        currentParamItemIndex = 0;

    //                        continue;
    //                    }

    //                    //找到参数开始的 td
    //                    if (oChunk.sTag == "td" && oChunk.oType == HTMLchunkType.OpenTag)
    //                    {
    //                        oChunk = op.ParseNext();
    //                        if (oChunk.oType == HTMLchunkType.OpenTag)
    //                        {
    //                            oChunk = op.ParseNext();
    //                            currentParamName = oChunk.oHTML;
    //                            if (currentParamPartIndex == 0 && currentParamItemIndex == (UInt16)BasicItems.Warranty)
    //                            {
    //                                oChunk = op.ParseNextByTagNameAndType("a", HTMLchunkType.OpenTag);
    //                            }
    //                            else
    //                            {
    //                                oChunk = op.ParseNextByTagNameAndType("td", HTMLchunkType.OpenTag);
    //                            }
    //                        }
    //                    }

    //                    //找到具体参数值
    //                    if (oChunk.oType == HTMLchunkType.Text)
    //                    {
    //                        tagText = oChunk.oHTML;

    //                        if (currentParamName.Contains("车型名称"))
    //                        {
    //                            CarParams car = new CarParams();
    //                            car.BasicParams.ModelName = tagText;
    //                            carList.Add(colunmNum, tagText);
    //                        }
    //                        else
    //                        {
    //                            //carList[colunmNum].SetParamValue(currentParamPartIndex, currentParamItemIndex, tagText);
    //                            carList.Add(colunmNum, tagText);
    //                        }

    //                        colunmNum++;
    //                    }

    //                    if (oChunk.sTag == "tr" && oChunk.oType == HTMLchunkType.CloseTag)
    //                    {
    //                        currentParamItemIndex++;

    //                        colunmNum = 0;
    //                    }

    //                } while (!(oChunk.sTag == "table" && oChunk.oType == HTMLchunkType.CloseTag));

    //            }

    //            #endregion

    //            #region ConfigTable

    //            #endregion

    //            if (oChunk.sTag == "table" && oChunk.oType == HTMLchunkType.CloseTag)
    //            {
    //                tableContent = false;
    //                break;
    //            }


    //        } while ((oChunk = op.ParseNextTag()) != null);

    //        return carList;
    //    }

    //    public static Dictionary<UInt16, CarParams> parserTR(ref HTMLparser op, HTMLchunk oc)
    //    {
    //        string strJsonData = "";

    //        //Get Mfc Name by the html
    //        string mfcName = "";

    //        //Get Car Name by the html
    //        string carName = "";

    //        //Get Car Class Name by the html
    //        string carClassName = "";

    //        //Get Sub Car Name by the html
    //        string subCarName = "";

    //        //Get Sub Car Price by the html
    //        decimal price = 0;

    //        //#1 Get BrandID
    //        int brandID = 0; ;

    //        /*


    //        //#2 Get Mfc ID

    //        //#3 Get Car ID

    //        //#4 Get Sub Car ID
    //        int subCarID;
    //        {
    //            B_SubCar bll = new B_SubCar();
    //            M_SubCar model = new M_SubCar();
    //            model.CAR_ID = carID;
    //            model.SUBCAR_NAME = subCarName;
    //            model.PRICE = price;
    //            subCarID = bll.AddAndGetMaxID(model);
    //        }
    //        */


    //        XmlDocument xml = new XmlDocument();

    //        HTMLchunk oChunk = oc;

    //        bool finished = false;

    //        Boolean tableContent = false;

    //        Boolean OptionTable = false;
    //        Boolean ConfigTable = false;

    //        Boolean CarAdded = false;

    //        string currentParamName = "";

    //        //当前参数块索引
    //        Int16 currentParamPartIndex = -1;

    //        //当前具体参数项索引
    //        UInt16 currentParamItemIndex = 0;

    //        string tagText = "";

    //        UInt16 colunmNum = 0;
    //        UInt16 rowNum = 0;

    //        //carParam.Struct = "涡轮";

    //        //string x = carParam.Struct;

    //        Dictionary<UInt16, CarParams> carList = new Dictionary<UInt16, CarParams>();

    //        #region GetHeader

    //        do
    //        {
    //            oc = op.ParseNext();

    //            if (oc.oHTML.Trim().Replace("：", "") == "当前位置")
    //            {

    //                oc = GetTagByIndex(op, 9);
    //                carClassName = oc.oHTML.Trim();

    //                oc = GetTagByIndex(op, 7);
    //                carName = oc.oHTML.Trim();

    //                //oc = GetTagByIndex(op, 4);
    //                //subCarName = oc.oHTML.Trim();

    //                break;
    //            }

    //        } while (true);

    //        #endregion

    //        #region Get Json Data

    //        int scriptCount = 0;
    //        do
    //        {
    //            oc = op.ParseNext();

    //            if (oc.sTag == "script" && oc.oType == HTMLchunkType.Script)
    //            {
    //                scriptCount++;
    //                if (scriptCount == 3)
    //                {
    //                    strJsonData = oc.oHTML.TrimStart();
    //                    break;
    //                }

    //            }

    //        } while (true);

    //        #endregion

    //        List<JsonObj> objList = SerializeJsonData(strJsonData);

    //        ProcessJsonObjects(objList);

    //        return carList;
    //    }

    //    private static void ProcessJsonObjects(List<JsonObj> objList)
    //    {
    //        JsonObjItem jsonObjItem;
    //        string strGroup = String.Empty;
    //        string strItem = String.Empty;
    //        string strType = String.Empty;

    //        int indexOfParamInGroup = 1;

    //        List<string> lstGroups = new List<string>();
    //        List<string> lstItems = new List<string>();
    //        List<string> lstTypes = new List<string>();

    //        List<M_Param> lstParams = new List<M_Param>();


    //        foreach (var jsonObj in objList)
    //        {
    //            if (jsonObj.success == "1")
    //            {
    //                for (int i = 0; i < jsonObj.body.items.Count; i++)
    //                {
    //                    jsonObjItem = jsonObj.body.items[i];

    //                    if (!lstGroups.Contains(jsonObjItem.Item)) { lstGroups.Add(jsonObjItem.Item); }

    //                    if (strGroup == String.Empty)
    //                    {
    //                        strGroup = jsonObjItem.Item;
    //                    }
    //                    else if (strGroup != jsonObjItem.Item)
    //                    {
    //                        strGroup = jsonObjItem.Item;
    //                        indexOfParamInGroup = 1;
    //                    }
    //                    else
    //                    {
    //                        indexOfParamInGroup++;
    //                    }

    //                    if (!lstItems.Contains(jsonObjItem.Name)) { lstItems.Add(jsonObjItem.Name); }

    //                    if (!lstTypes.Contains(jsonObjItem.ItemType)) { lstTypes.Add(jsonObjItem.ItemType); }

    //                    M_Param m_Param = new M_Param();
    //                    m_Param.p_name = jsonObjItem.Name;
    //                    m_Param.group_id = lstGroups.Count;
    //                    m_Param.p_index = indexOfParamInGroup;

    //                    lstParams.Add(m_Param);
    //                }
    //            }
    //        }


    //        #region Insert Base Params
    //        //B_Groups group = new B_Groups();
    //        //group.InsertByGroup(lstGroups);

    //        //B_Params param = new B_Params();
    //        //param.InsertByGroup(lstParams);
    //        #endregion

    //    }

    //    public static void GetHeader(HTMLparser parser, HTMLchunk oc)
    //    {


    //    }

    //    public static HTMLchunk GetTagByIndex(HTMLparser parser, int tagIndex)
    //    {
    //        HTMLchunk chunk;

    //        do
    //        {
    //            tagIndex--;

    //            chunk = parser.ParseNext();

    //        } while (tagIndex != 0);

    //        return chunk;
    //    }

    //    public static List<JsonObj> SerializeJsonData(string str)
    //    {
    //        List<JsonObj> objList = new List<JsonObj>(2);
    //        JsonSerializer serializer = new JsonSerializer();

    //        //0-术语，1-配置，2-选项，3-颜色，4-价格
    //        string[] varList = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

    //        string strConfig = varList[1].Substring(varList[1].IndexOf('{'), varList[1].LastIndexOf('}') - varList[1].IndexOf('{') + 1);
    //        string strOption = varList[2].Substring(varList[2].IndexOf('{'), varList[2].LastIndexOf('}') - varList[2].IndexOf('{') + 1);

    //        JsonObj obj;

    //        obj = (JsonObj)serializer.Deserialize(new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(strConfig)), typeof(JsonObj));
    //        objList.Add(obj);

    //        obj = (JsonObj)serializer.Deserialize(new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(strOption)), typeof(JsonObj));
    //        objList.Add(obj);

    //        return objList;
    //    }
    //} 

    
    public class JsonAnalyzer
    {
        private string strJson = string.Empty;

        public JsonAnalyzer(string json)
        {
            this.strJson = json;
        }

        public static void ParseJsonData(string str)
        {
            //0-术语，1-配置，2-选项，3-颜色，4-价格
            string[] varList = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static List<JsonObj> SerializeJsonData(string str)
        {
            List<JsonObj> objList = new List<JsonObj>(2);
            JsonSerializer serializer = new JsonSerializer();

            //0-术语，1-配置，2-选项，3-颜色，4-价格
            string[] varList = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            string strConfig = varList[2].Substring(varList[2].IndexOf('{'), varList[2].LastIndexOf('}') - varList[2].IndexOf('{') + 1);
            string strOption = varList[3].Substring(varList[3].IndexOf('{'), varList[3].LastIndexOf('}') - varList[3].IndexOf('{') + 1);

            JsonObj obj;

            obj = (JsonObj)serializer.Deserialize(new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(strConfig)), typeof(JsonObj));
            objList.Add(obj);

            obj = (JsonObj)serializer.Deserialize(new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(strOption)), typeof(JsonObj));
            objList.Add(obj);

            return objList;
        }
    }

    #region JsonData Model

    public class JsonObj
    {
        public string returncode { get; set; }
        public string message { get; set; }
        //public string cache { get; set; }
        public JsonObjBody result { get; set; }
    }

    public class JsonObjBody
    {
        public string seriesid { get; set; }
        public string yearid { get; set; }
        public List<JsonObjItem> paramtypeitems { get; set; }
        public List<JsonObjItem> configtypeitems { get; set; }

    }

    public class JsonObjItem
    {
        //public string Item { get; set; }
        public string Name { get; set; }
        //public string ItemType { get; set; }
        public List<JsonObjValue> paramitems { get; set; }
        public List<JsonObjValue> configitems { get; set; }
    }

    public class JsonObjValue
    {
        public string name { get; set; }
        public List<JsonParamValue> valueitems { get; set; }
    }

    public class JsonParamValue
    {
        public string specid { get; set; }
        public string value { get; set; }
    }

    #endregion

}

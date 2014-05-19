using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Carsinfo.BusinessInfrastructure_ns;
using HtmlAnalyzer_ns;

namespace DBImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"D:\CarsData");
            foreach (var file in files)
            {
                var html = File.ReadAllText(file);

                var jsonData = HtmlAnalyzer.GetJsonContent(html);

                var lst = HtmlAnalyzer_ns.JsonAnalyzer.SerializeJsonData(jsonData);

                InsertBrand(lst);
                InsertFactory(lst);
                InsertModel(lst);
                InsertParamGroup(lst);
                break;
            }
        }

        static void InsertBrand(List<JsonObj> lst)
        {




            //var list = new List<JsonParamValue>();
            //JsonParamValue xValue;
            //try
            //{
            //    foreach (var obj in brand)
            //    {
            //        xValue = obj;

            //        list.Add(obj);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

        }

        static void InsertFactory(List<JsonObj> lst)
        {
            
        }

        static void InsertModel(List<JsonObj> lst)
        {
            
        }

        private static void InsertParamGroup(List<JsonObj> lst)
        {
            var groupNames =
                (
                    (from param in lst[0].result.paramtypeitems
                        //from paramGroup in param.paramitems
                        select param.Name)
                        .Union
                        (from config in lst[1].result.configtypeitems
                            //from configGroup in config.configitems
                            select config.Name)
                    ).ToList();
        }

    }
}

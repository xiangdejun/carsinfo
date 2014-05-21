using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Carsinfo.BusinessInfrastructure_ns;
using CarsInfo.DALSqlite_ns;
using CarsInfo.DataEntitySqlite;
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
                InsertClass(lst);
                //InsertParamGroup(lst);
                //InsertParams(lst);
                break;
            }
        }

        static void InsertBrand(List<JsonObj> lst)
        {
        }

        static void InsertFactory(List<JsonObj> lst)
        {
            
        }

        static void InsertModel(List<JsonObj> lst)
        {
            
        }

        static void InsertClass(List<JsonObj> lst)
        {
            var classes =
                (from p in lst[0].result.paramtypeitems[0].paramitems[3].valueitems
                    select p.value).ToList();

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
                ).ToList<string>();

            SqliteHelper.InsertParamGroup(groupNames);
        }

        private static void InsertParams(List<JsonObj> lst)
        {
            var dtGroup = SqliteHelper.GetTable("paramgroup");
            //dtGroup.AsEnumerable().Where()

            var paramNames =
                (
                    (from param in lst[0].result.paramtypeitems
                        from paramGroup in param.paramitems
                        select
                            new
                            param
                            {
                                pgid = 
                                    int.Parse(
                                        dtGroup.AsEnumerable()
                                            .FirstOrDefault(row => row["pgname"].ToString().Equals(param.Name))["pgid"]
                                            .ToString()),
                                pname = paramGroup.name

                            })
                        .Union
                        (from config in lst[1].result.configtypeitems
                            from configGroup in config.configitems
                            select
                                new
                                param
                                {
                                    pgid = 
                                        int.Parse(
                                            dtGroup.AsEnumerable()
                                                .FirstOrDefault(row => row["pgname"].ToString().Equals(config.Name))[
                                                    "pgid"].ToString()),
                                    pname = configGroup.name

                                })
                    ); //.ToDictionary(p=>p.paramname, p=>p.groupid);

            //var dict = paramNames.ToDictionary(p => p.groupid, p => p.paramname);
            SqliteHelper.InsertParams(paramNames);
        }

    }


}

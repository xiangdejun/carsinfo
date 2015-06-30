using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Carsinfo.BusinessInfrastructure_ns;
using CarsInfo.DALSqlite_ns;
using CarsInfo.DataEntitySqlite;
using HtmlAnalyzer_ns;
using  CarsInfo.DataModel_ns;


namespace DBImport
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var files = Directory.GetFiles(@"D:\CarsData");
            //foreach (var file in files)
            //{
            //    var html = File.ReadAllText(file);

            //    var jsonData = HtmlAnalyzer.GetJsonContent(html);

            //    var lst = HtmlAnalyzer_ns.JsonAnalyzer.SerializeJsonData(jsonData);

            //    InsertBrand(lst);
            //    InsertFactory(lst);
            //    InsertModel(lst);
            //    InsertClass(lst);
            //    //InsertParamGroup(lst);
            //    //InsertParams(lst);
            //    break;
            //}

            //var list = GetAllCarsInfoFromHtml();

            //var category = GetCarCategory();

            //InsertFactory();


            //InsertFactory(list);

            //InsertSeries();

            //InsertCar();
        }

        private static List<List<JsonObj>> GetAllCarsInfoFromHtml()
        {
            var carList = new List<List<JsonObj>>();

            var files = Directory.GetFiles(@"D:\CarsData");
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Length < 10*1024)
                {
                    File.Move(fileInfo.FullName, fileInfo.DirectoryName + @"\bad\" + fileInfo.Name);
                    //File.WriteAllText(fileInfo.DirectoryName + @"\Json\" + fileInfo.Name, jsonString);
                    continue;
                    ;
                }

                var html = File.ReadAllText(file);

                var jsonString = HtmlAnalyzer.GetJsonContent(html);
                if (string.IsNullOrEmpty(jsonString))
                {
                    File.Move(fileInfo.FullName, fileInfo.DirectoryName + @"\bad\" + fileInfo.Name);
                    continue;
                    ;
                }

                File.WriteAllText(fileInfo.DirectoryName + @"\Json\" + fileInfo.Name, jsonString);

                var lst = HtmlAnalyzer_ns.JsonAnalyzer.SerializeJsonData(jsonString);

                //carList.Add(lst);
                //break;;
            }

            return carList;
        }

        private static List<CarShort> GetCarCategoryWithPrice()
        {

            var carlistFile = File.ReadAllText("../../../../TestCases/carlist_withPrice", Encoding.Default);

            return HtmlAnalyzer.GetCarListWithPrice(carlistFile);

        }

        private static List<CarsCategory> GetCarCategoryWithFactoryName()
        {
            var carlistFile = File.ReadAllText("../../../../TestCases/carlist", Encoding.Default);

            return HtmlAnalyzer.GetCarListWithFactory(carlistFile);
        }

        private static List<CarShort> GetCarCategoryWithCarCLass()
        {
            var carlistFile = File.ReadAllText("../../../../TestCases/carlist_WithClass", Encoding.Default);

            return HtmlAnalyzer.GetCarListWithClass(carlistFile);
        }

        private static void InsertBrand(List<CarsCategory> category)
        {
            var brands =
                (from c in category
                    select c.BrandName).ToList<string>();

            SqliteHelper.InsertBrand(brands);
        }

        private static void InsertFactory()
        {
            var list = GetCarCategoryWithFactoryName();

            var dtBrands = SqliteHelper.GetTable("Brand");

            var factoryList =
                (from c in list
                    from o in c.Cars
                    where !string.IsNullOrEmpty(o.FactoryName)
                    select new factory
                    {
                        fname = o.FactoryName,
                        bid = int.Parse(
                            dtBrands.AsEnumerable().FirstOrDefault(row => row["bname"].ToString().Equals(c.BrandName))[
                                "bid"].ToString())
                    })
                    .Distinct(new FactoryComparer());

            //SqliteHelper.InsertFactory(factoryList);
        }

        private static void InsertSeries()
        {
            var dtClass = SqliteHelper.GetTable("carclass");
            var dtFactory = SqliteHelper.GetTable("factory");

            var listWithClass = GetCarCategoryWithCarCLass();
            var listWithPrice = GetCarCategoryWithPrice();
            var listWithFactory = GetCarCategoryWithFactoryName();

            //var carsInLC = (from c in listWithClass
            //                where !String.IsNullOrEmpty(c.CarName)
            //                where !string.IsNullOrWhiteSpace(c.CarName)
            //                select c.CarName).ToList();
            //var carsInLP = (from c in listWithPrice
            //                where !String.IsNullOrEmpty(c.CarName)
            //                where !string.IsNullOrWhiteSpace(c.CarName)
            //                select c.CarName).ToList();

            //var diff =
            //    (from c in carsInLP
            //        where !carsInLC.Contains(c)
            //        select c).ToList();

            //var diff1 =
            //                    (from c in carsInLC
            //                     where !carsInLP.Contains(c)
            //                     select c).ToList();

            var carseries = new List<series>();

            foreach (var car in listWithPrice)
            {
                var cc = listWithClass.FirstOrDefault(c => c.CarName == car.CarName);
                var cf =
                    (from c in listWithFactory
                        from o in c.Cars
                        where o.CarName == car.CarName
                        select o.FactoryName).FirstOrDefault();

                if (cf == null)
                {
                    continue;
                }

                var classid =
                    int.Parse(
                        dtClass.AsEnumerable().FirstOrDefault(r => r["classname"].ToString().Equals(cc.ClassName))[
                            "classid"].ToString());
                var facId =
                    int.Parse(
                        dtFactory.AsEnumerable().FirstOrDefault(r => r["fname"].ToString().Equals(cf))["fid"].ToString());

                var serie = new series
                {
                    sname = car.CarName,
                    classid = classid,
                    fid = facId,
                    price = string.IsNullOrEmpty(car.RetailPrice) ? "" : car.RetailPrice.Replace("万", "")
                };

                carseries.Add(serie);
            }


            SqliteHelper.InsertSeries(carseries);

            //var series =
            //    (from c in list
            //     from o in c.Cars
            //     select new series()
            //     {
            //         fid = int.Parse(dtFactory.AsEnumerable().FirstOrDefault(row => row["fname"].ToString().Equals(c.BrandName))["fid"].ToString()),
            //         sname = o.CarName,
            //     }).ToList();
        }

        private static void InsertClass(List<JsonObj> lst)
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

        private static void InsertCar()
        {
            var dtSeries = SqliteHelper.GetTable("series");

            var car = new car();

            var files = Directory.GetFiles(@"D:\CarsData").ToList();
            files.Sort((s, s1) => { return int.Parse(s.Remove(0, 12)).CompareTo(int.Parse(s1.Remove(0, 12))); });
            foreach (var file in files)
            {

                var fileInfo = new FileInfo(file);
                try
                {
                    var html = File.ReadAllText(file);

                    var serieName = HtmlAnalyzer.GetSerieName(html);

                    var jsonString = HtmlAnalyzer.GetJsonContent(html);

                    if (string.IsNullOrEmpty(jsonString))
                    {
                        continue;
                        ;
                    }

                    var lst = HtmlAnalyzer_ns.JsonAnalyzer.SerializeJsonData(jsonString);

                    var cars =
                        (from c in lst[0].result.paramtypeitems[0].paramitems[0].valueitems
                            select new car
                            {
                                cname = c.value,
                                sid = int.Parse(dtSeries.AsEnumerable()
                                    .FirstOrDefault(row => row["sname"].ToString().Equals(serieName))["sid"]
                                    .ToString())
                            }).ToList();

                    SqliteHelper.InsertCar(cars);

                    //Thread.Sleep(10);
                    Console.WriteLine(file);
                }
                catch (Exception)
                {
                    File.Copy(fileInfo.FullName, fileInfo.DirectoryName + @"\failed\" + fileInfo.Name);
                    continue;
                }
            }

            //return carList;

        }

    }

    public class FactoryComparer : IEqualityComparer<factory>
    {
        bool IEqualityComparer<factory>.Equals(factory x, factory y)
        {
            bool b = true;
            if (x == null)
                return y == null;
            return x.fname == y.fname;
        }
        int IEqualityComparer<factory>.GetHashCode(factory obj)
        {
            if (obj == null)
                return 0;
            return obj.fname.GetHashCode();
        }
    } 


}

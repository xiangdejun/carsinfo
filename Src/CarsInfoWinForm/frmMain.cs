using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarsInfo.BusinessRepository_ns;
using CarsInfo.DataModel_ns;
using Carsinfo.BusinessInfrastructure_ns;

namespace CarsInfoWinForm
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //LoadBrands();
            //LoadFactories();
            //var fileName = "7996";

            //ThreadPool.QueueUserWorkItem(delegate(object state)
            //    {
            //        for (int i = 1300; i <3000; i++)
            //        {
            //            var fileName = i.ToString();

            //            var str = HtmlAnalyzer.GetHtml("http://www.autohome.com.cn/spec/" + fileName + "/config.html");

            //            HtmlAnalyzer.SaveText(str, @"D:\CarsData\" + fileName);

            //            Thread.Sleep(2000);
            //        }

            //        MessageBox.Show("Done!");

            //    });

            //ThreadPool.QueueUserWorkItem((state) => { SaveData(10000, 10500); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(11000, 11500); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(12000, 12500); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(13000, 13500); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(14000, 14500); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(15000, 15500); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(16000, 16500); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(17000, 17500); });

            //ThreadPool.QueueUserWorkItem((state) => { SaveData(10500, 11000); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(11500, 12000); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(12500, 13000); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(13500, 14000); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(14500, 15000); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(15500, 16000); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(16500, 17000); });
            //ThreadPool.QueueUserWorkItem((state) => { SaveData(17500, 18000); });

            GetJsonData();
        }

        private void GetJsonData()
        {
            var html = File.ReadAllText(@"D:\CarsData\1000");
            var jsonData = HtmlAnalyzer.GetJsonContent(html);

            //var anlyzer = new HtmlAnalyzer_ns.JsonAnalyzer(jsonData); 
            
            var lst = HtmlAnalyzer_ns.JsonAnalyzer.SerializeJsonData(jsonData);

        }

        private void SaveData(int start, int end)
        {
            //ThreadPool.QueueUserWorkItem()

            for (int i = start; i < end; i++)
            {
                var fileName = i.ToString();

                var str = HtmlAnalyzer.GetHtml("http://www.autohome.com.cn/spec/" + fileName + "/config.html");

                HtmlAnalyzer.SaveText(str, @"D:\CarsData\" + fileName);

                Thread.Sleep(1000);
            }
        }

        private void LoadBrands()
        {
            var brands = BrandRepository.GetAll();

            foreach (var brand in brands)
            {
                this.tv.Nodes.Add(key: brand.Id.ToString(), text: brand.Name);
            }
        }

        private void LoadFactories()
        {
            var repository = new BaseRepository<Brand>();
            //var factories = repository.GetAll();

            //foreach (var factory in factories)
            //{
            //    this.tv.Nodes.Add(key: factory.Id.ToString(), text: factory.Name);
            //}
        }
    }
}

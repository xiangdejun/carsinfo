using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Carsinfo.BusinessInfrastructure_ns;
using NUnit.Framework;

namespace BusinessInfrastructure.Test
{
    [TestFixture]
    public class HtmlAnalyzerTests
    {
        [Test]
        public void GetJsonContent_Null_ReturnEmpty()
        {
            Assert.AreEqual(HtmlAnalyzer.GetJsonContent(null), "");
        }

        [Test]
        public void GetJsonContent_Empty_ReturnEmpty()
        {
            Assert.AreEqual(HtmlAnalyzer.GetJsonContent(""), "");
        }

        [Test]
        public void GetJsonContent_DoNotContainJson_ReturnEmpty()
        {
            Assert.AreEqual(HtmlAnalyzer.GetJsonContent("123"), "");
        }

        [Test]
        public void GetJsonContent_CorrectHtml_ReturnJsonData()
        {
            string strHtml = HtmlAnalyzer.ReadFile(@"..\..\..\..\TestCases\a1_gb2312.txt");
            string strJson = HtmlAnalyzer.ReadFile(@"..\..\..\..\TestCases\a1_gb2312_json.txt").Trim();
            Assert.AreEqual(strJson, HtmlAnalyzer.GetJsonContent(strHtml));
        }
    }
}

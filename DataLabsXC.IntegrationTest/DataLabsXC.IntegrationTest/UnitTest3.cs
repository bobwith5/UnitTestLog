using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Data.Linq;
using System.Linq;
namespace DataLabsXC.IntegrationTest
{
    [TestClass]
    public class UnitTest3
    {
        public TestContext TestContext { get; set; }

        public enum Status
        {
            Start,
            End
        }

        //// Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //    Log(TestContext.FullyQualifiedTestClassName, TestContext.TestName, Status.Start);
        //}

        //// Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //    Log(TestContext.FullyQualifiedTestClassName, TestContext.TestName, Status.End);
        //}



        public void Log(string className, string methodName, Status status)
        {
            //string fileName = @"C:\D\UnitTestLog\log.xml";
            //XDocument xmlDocument;
            //XElement rootElement;
            //if (!File.Exists(fileName))
            //{
            //    xmlDocument = new XDocument();
            //    rootElement = new XElement("TestMethods");
            //    xmlDocument.Add(rootElement);
            //}
            //else
            //{
            //    xmlDocument = XDocument.Load(fileName);
            //    rootElement = xmlDocument.Element("TestMethods");
            //}

            //var element = rootElement.Elements("TestMethod").Count() == 0 ? null : rootElement.Elements("TestMethod").FirstOrDefault(x => x.Attribute("ClassName").Value == className && x.Attribute("Method").Value == methodName);
            //if (element == null)
            //{
            //    var newElement = new XElement("TestMethod");
            //    newElement.Add(new XAttribute("ClassName", className));
            //    newElement.Add(new XAttribute("Method", methodName));
            //    newElement.Add(new XAttribute("StartTime", DateTime.Now));
            //    rootElement.Add(newElement);
            //}
            //else
            //{
            //    DateTime endTime= DateTime.Now;
            //    element.Add(new XAttribute("EndTime",endTime));
            //    DateTime startTime = Convert.ToDateTime(element.Attribute("StartTime").Value);
            //    element.Add(new XAttribute("ExecutionTime", (endTime - startTime).TotalSeconds));
                
            //}
            //xmlDocument.Save(fileName);
        }
    }
}

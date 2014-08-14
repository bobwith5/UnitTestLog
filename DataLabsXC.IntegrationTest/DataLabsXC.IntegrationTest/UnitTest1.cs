using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
//using System.Xml.Linq;
//using System.Linq;


namespace DataLabsXC.IntegrationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 class1 = new Class1();
            string result = class1.MethodA();
            Assert.AreEqual("Class1 - MethodA", result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Class1 class1 = new Class1();
            string result = class1.MethodB();
            Assert.AreEqual("Class1 - MethodB", result);
           // GenerateIntegrationResult();
            // GetResults();

        }
        public void GenerateIntegrationResult(string standardResultPath, string recentResultFilePath)
        {
            //string standardResultPath = @"C:\UnitTestResults\IntegrationResults";
            //string recentResultFilePath = @"C:\UnitTestResults\1.0.5.2.0.2014081343\results.xml";
            if (!Directory.Exists(standardResultPath))
                Directory.CreateDirectory(standardResultPath);
            DirectoryInfo resultDirectory = new DirectoryInfo(standardResultPath);
            bool isResultGenerated = false;
            StringBuilder innerHtml = new StringBuilder();
            string defaultStyle = "style='background-color: #FFFFFF;'";
            string newTestStyle = "style='background-color: #00FF1E;'";
            string increaseTimeStyle = "style='background-color: #A41E0F; color: #FFFFFF;'";
            foreach (FileInfo file in resultDirectory.GetFiles("*.xml"))
            {

                if (!isResultGenerated && File.Exists(recentResultFilePath))
                {
                    XmlDocument sourceResult = new XmlDocument();
                    sourceResult.Load(recentResultFilePath);
                    XmlDocument destinationResult = new XmlDocument();
                    destinationResult.Load(standardResultPath + "\\results.xml");
                    foreach (XmlNode sourceNode in sourceResult.DocumentElement.SelectNodes("/Results/TestMethod"))
                    {
                        string testName = sourceNode.Attributes["TestName"].Value;
                        string sourceDuration = sourceNode.Attributes["duration"].Value;
                        string selectedStyle = string.Empty;
                        TimeSpan souceTime = TimeSpan.Parse(sourceDuration);
                        TimeSpan destinationTime;
                        XmlNode destinationNode = destinationResult.DocumentElement.SelectSingleNode(string.Format("/Results/TestMethod[@TestName='{0}']", testName));
                        string destinationDuration = string.Empty;
                        if (destinationNode != null)
                        {
                            destinationDuration = destinationNode.Attributes["duration"].Value;
                            destinationTime = TimeSpan.Parse(destinationDuration);
                            if (souceTime == destinationTime)
                                selectedStyle = defaultStyle;
                            else
                                selectedStyle = increaseTimeStyle;
                        }
                        else
                        {
                            selectedStyle = newTestStyle;
                        }
                        innerHtml.AppendFormat("<tr {0} ><td>{1}</td><td>{2}</td><td>{3}</td></tr>", selectedStyle, testName, destinationDuration, sourceDuration);
                    }
                    isResultGenerated = true;
                }
            }
            if (isResultGenerated && innerHtml.Length > 0)
            {
                string tblHeader = "<table cellpadding='2' cellspacing='2' border='1' style='border-style: 2; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: 12px;'>";
                tblHeader += "<tr style='background-color: #11B1FF; font-weight: bold;'>";
                tblHeader += "<td>Test Method Name</td>";
                tblHeader += "<td>Previous Run Duration</td>";
                tblHeader += "<td>Current Run Duration</td></tr>";
                innerHtml.Insert(0, tblHeader);
                innerHtml.Append("</table>");
                foreach (FileInfo file in resultDirectory.GetFiles("*.xml"))
                {
                    file.Delete();
                }
                File.Move(recentResultFilePath, standardResultPath + "\\results.xml");
                using (FileStream htmlStream = new FileStream(standardResultPath + "\\"+ DateTime.Now.ToFileTime() + ".htm", FileMode.Create))
                {
                    using (StreamWriter htmlWriter = new StreamWriter(htmlStream, Encoding.UTF8))
                    {
                        htmlWriter.WriteLine(innerHtml.ToString());
                    }
                }
            }
            else if (!isResultGenerated)
            {
                File.Move(recentResultFilePath, standardResultPath + "\\results.xml");
            }


        }
        public void GetResults()
        {
            string source = @"C:\Users\bommitc\Desktop\results.trx";
            string resultFileName = @"C:\D\UnitTestLog\log.xml";
            XmlDocument xDoc = new XmlDocument();
            XmlDocument xmlDocument;
            xDoc.Load(source);
            XmlNode rootNode;
            var mgr = new XmlNamespaceManager(xDoc.NameTable);
            mgr.AddNamespace("", "http://schemas.microsoft.com/appx/2010/manifest");
            XmlNode root = xDoc.DocumentElement;
            var nodes = root.SelectNodes("//*[local-name()='TestRun']/*[local-name()='Results']/*[local-name()='TestResultAggregation']/*[local-name()='InnerResults']/*[local-name()='UnitTestResult']");
            if (!File.Exists(resultFileName))
            {
                xmlDocument = new XmlDocument();
                rootNode = xmlDocument.CreateNode(XmlNodeType.Element, "Results", null);
                xmlDocument.AppendChild(rootNode);
            }
            else
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(resultFileName);
                rootNode = xmlDocument.DocumentElement;
            }
            foreach (XmlNode item in nodes)
            {
                XmlNode resultNode = xmlDocument.CreateNode(XmlNodeType.Element, "TestMethod", null);
                XmlAttribute className = xmlDocument.CreateAttribute("TestName");

                className.Value = item.Attributes["testName"].Value;

                XmlAttribute outcome = xmlDocument.CreateAttribute("outcome");

                outcome.Value = item.Attributes["outcome"].Value;
                XmlAttribute duration = xmlDocument.CreateAttribute("duration");

                duration.Value = item.Attributes["duration"].Value;
                resultNode.Attributes.Append(className);
                resultNode.Attributes.Append(outcome);
                resultNode.Attributes.Append(duration);
                rootNode.AppendChild(resultNode);

            }

            xmlDocument.Save(resultFileName);
        }


        //public void Log(string className, string methodName)
        //{
        //    string fileName = @"C:\D\UnitTestLog\log.xml";
        //    XDocument xmlDocument;
        //    XElement rootElement;
        //    if (!File.Exists(fileName))
        //    {
        //        xmlDocument = new XDocument();
        //        rootElement = new XElement("TestMethods");
        //        xmlDocument.Add(rootElement);
        //    }
        //    else
        //    {
        //        xmlDocument = XDocument.Load(fileName);
        //        rootElement = xmlDocument.Element("TestMethods");
        //    }

        //    var element = rootElement.Elements("TestMethod").Count() == 0 ? null : rootElement.Elements("TestMethod").FirstOrDefault(x => x.Attribute("ClassName").Value == className && x.Attribute("Method").Value == methodName);
        //    if (element == null)
        //    {
        //        var newElement = new XElement("TestMethod");
        //        newElement.Add(new XAttribute("ClassName", className));
        //        newElement.Add(new XAttribute("Method", methodName));
        //        newElement.Add(new XAttribute("StartTime", DateTime.Now));
        //        rootElement.Add(newElement);
        //    }
        //    else
        //    {
        //        DateTime endTime = DateTime.Now;
        //        element.Add(new XAttribute("EndTime", endTime));
        //        DateTime startTime = Convert.ToDateTime(element.Attribute("StartTime").Value);
        //        element.Add(new XAttribute("ExecutionTime", (endTime - startTime).TotalSeconds));

        //    }
        //    xmlDocument.Save(fileName);
        //}
        //public void GetResults()
        //{
        //    string source = @"C:\Users\bommitc\Desktop\results.trx";
        //    string resultFileName = @"C:\D\UnitTestLog\log.xml";
        //    XDocument xDoc = XDocument.Load(source);
        //    XNamespace defaultNs = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
        //    var UnitTestResultNode = (from data in xDoc.Descendants(defaultNs + "Results")
        //                              select data).Descendants(defaultNs + "UnitTestResult");
        //    var recordSet = from src in UnitTestResultNode
        //                    select new XElement("UnitTestResult",
        //                        new XAttribute("TestRunID", src.Attribute("testId").Value),
        //                        new XAttribute("TestName", src.Attribute("testName").Value),
        //                        new XAttribute("TestOutcome", src.Attribute("outcome").Value),
        //                        new XAttribute("Duration", src.Attribute("duration").Value)
        //                    );

        //    XDocument xmlDocument;
        //    XElement rootElement;
        //    if (!File.Exists(resultFileName))
        //    {
        //        xmlDocument = new XDocument();
        //        rootElement = new XElement("Results");
        //        xmlDocument.Add(rootElement);
        //    }
        //    else
        //    {
        //        xmlDocument = XDocument.Load(resultFileName);
        //        rootElement = xmlDocument.Element("Results");
        //    }
        //    rootElement.Add(recordSet);
        //    xmlDocument.Save(resultFileName);
        //}
        //new XAttribute("StackTrace",src
        //                                      .Descendants(defaultNs + "Output")
        //                                      .Descendants(defaultNs + "ErrorInfo")
        //                                      .Descendants(defaultNs + "StackTrace"))



    }
}

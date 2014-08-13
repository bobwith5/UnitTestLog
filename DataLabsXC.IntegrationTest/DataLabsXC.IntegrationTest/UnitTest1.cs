using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

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

            var doc = new System.Xml.XmlDocument();
            doc.Load(@"C:\D\UnitTestLog\DataLabsXC.IntegrationTest\DataLabsXC.IntegrationTest\testdefinition\TestDefinitionConfiguration.xml");

            System.Xml.XmlNodeList testNodes = doc.SelectNodes("BuildDefinition/TestDefinitionConfiguration/*");
            if (testNodes == null)
            {
                throw new Exception("No TestDefinitionConfiguration node was found in the XML document.");
            }

            uint selections = 0;
            int index = 1;
            int executeTestCount = 0;
            bool isFirstRecord = true;
            var testsToExecute = new StringBuilder();
            foreach (System.Xml.XmlNode testNode in testNodes)
            {
                //uint switchValue = Convert.ToUInt32(testNode.Attributes["Value"].InnerText);
                //if ((selections & switchValue) != 0)
                //{
                executeTestCount++;
                Console.WriteLine(testNode.Attributes["Name"].InnerText);
                if (isFirstRecord)
                {
                    testsToExecute.Append(index);
                }
                else
                {
                    testsToExecute.Append(String.Format(",{0}", index));
                }

                isFirstRecord = false;
                // }

                index++;
            }

            if (testNodes.Count == executeTestCount)
            {
                string GenerateArtifacts = "true";
            }

            string TestsToExecute = testsToExecute.ToString();
        }
    }
}

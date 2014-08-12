using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLabsXC.IntegrationTest
{
    [TestClass]
    public class UnitTest1:UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 class1 = new Class1();
            string result=class1.MethodA();
            Assert.AreEqual("Class1 - MethodA", result);
        }
         [TestMethod]
        public void TestMethod2()
        {
            Class1 class1 = new Class1();
            string result=class1.MethodB();
            Assert.AreEqual("Class1 - MethodB", result);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLabsXC.IntegrationTest
{
    [TestClass]
    public class UnitTest2:UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class2 class2 = new Class2();
            string result = class2.MethodA();
            Assert.AreEqual("Class2 - MethodA", result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Class2 class2 = new Class2();
            string result = class2.MethodB();
            Assert.AreEqual("Class2 - MethodB", result);
        }
    }
}

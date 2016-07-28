using System;
using NUnit.Framework;

namespace AbiokaApi.UnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1() {
            var a = 4;
            Assert.AreEqual(a, 4);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ethereum.Utilities;
using System.Text;

namespace Ethereum.Test
{
    [TestClass]
    public class CompactEncoderTest
    {
        [TestMethod]
        public void TestCompactHexDecode()
        {
            string test = "verb";
            byte[] expected = new byte[] { 7, 6, 6, 5, 7, 2, 6, 2, 16 };
            byte[] result = CompactEncoder.CompactHexDecode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }
    }
}

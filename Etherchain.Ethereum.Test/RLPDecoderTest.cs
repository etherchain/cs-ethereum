using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etherchain.Ethereum.Utilities;

namespace Etherchain.Ethereum.Test
{
    [TestClass]
    public class RLPDecoderTest
    {
        [TestMethod]
        public void TestDecodeSingleCharacter()
        {
            string Test = "64";
            string Expected = "d";
            Object Result = RLPDecoder.Decode(Test);
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestDecodeSingleString()
        {
            string Test = "83646f67";
            string Expected = "dog";
            Object Result = RLPDecoder.Decode(Test);
            Assert.AreEqual(Result, Expected);
        }
    }
}

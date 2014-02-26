using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etherchain.Ethereum.Utilities;

namespace Etherchain.Ethereum.Test.Utilities
{
    [TestClass]
    public class RLPEncoderTest
    {
        [TestMethod]
        public void TestEncodeSingleLetter()
        {
            Assert.AreEqual(RLPEncoder.Encode(" ")[0], 32);
            Assert.AreEqual(RLPEncoder.Encode("!")[0], 33);
            Assert.AreEqual(RLPEncoder.Encode("$")[0], 36);
            Assert.AreEqual(RLPEncoder.Encode("%")[0], 37);
            Assert.AreEqual(RLPEncoder.Encode("&")[0], 38);
            Assert.AreEqual(RLPEncoder.Encode("0")[0], 48);
            Assert.AreEqual(RLPEncoder.Encode("9")[0], 57);
            Assert.AreEqual(RLPEncoder.Encode("A")[0], 65);
            Assert.AreEqual(RLPEncoder.Encode("_")[0], 95);
            Assert.AreEqual(RLPEncoder.Encode("a")[0], 97);
        }
        
        [TestMethod]
        public void TestEncodeSingleString()
        {
            byte[] result = RLPEncoder.Encode("dog");
            Assert.AreEqual(result[0], 131);
            Assert.AreEqual(result[1], Convert.ToByte('d'));
            Assert.AreEqual(result[2], Convert.ToByte('o'));
            Assert.AreEqual(result[3], Convert.ToByte('g'));
        }

        [TestMethod]
        public void TestEncodeEmptyString()
        {
            Assert.AreEqual(RLPEncoder.Encode("")[0], 0x80);
        }

        [TestMethod]
        public void TestEncodeArrayOfEmptyStrings()
        {
            Assert.AreEqual(RLPEncoder.Encode(new string[] { }), new object[] { 0xc0 });
        }

        [TestMethod]
        public void TestEncodeLowInteger()
        {
            Assert.AreEqual(RLPEncoder.Encode(15)[0], 15);
        }

        [TestMethod]
        public void TestEncodeHighInteger()
        {
            Assert.AreEqual(RLPEncoder.Encode(1024)[0], 130);
            Assert.AreEqual(RLPEncoder.Encode(1024)[1], 4);
            Assert.AreEqual(RLPEncoder.Encode(1024)[2], 0);
        }

        [TestMethod]
        public void TestEncodeLongString()
        {
            Assert.AreEqual(RLPEncoder.Encode("Lorem ipsum dolor sit amet, consectetur adipisicing elit"), new object[] { 0xb8, 0x38, 'L', 'o', 'r', 'e', 'm', ' ', 'i', 'p', 's', 'u', 'm', ' ', 'd', 'o', 'l', 'o', 'r', ' ', 's', 'i', 't', ' ', 'a', 'm', 'e', 't', ',', ' ', 'c', 'o', 'n', 's', 'e', 'c', 't', 'e', 't', 'u', 'r', ' ', 'a', 'd', 'i', 'p', 'i', 's', 'i', 'c', 'i', 'n', 'g', ' ', 'e', 'l', 'i', 't' });
        }
    }
}

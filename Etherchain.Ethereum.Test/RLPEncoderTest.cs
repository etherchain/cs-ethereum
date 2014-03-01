using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etherchain.Ethereum.Utilities;

namespace Etherchain.Ethereum.Test.Utilities
{
    [TestClass]
    public class RLPEncoderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The input is null")]
        public void TestEncodeNull()
        {
            byte[] result = RLPEncoder.Encode(null);
        }
        
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
            Assert.AreEqual(RLPEncoder.Encode(new string[] { })[0], 0xc0);
        }

        [TestMethod]
        public void TestEncodeZero()
        {
            Assert.AreEqual(RLPEncoder.Encode(0)[0], 0x80);
        }

        [TestMethod]
        public void TestEncodeLowInteger()
        {
            Assert.AreEqual(RLPEncoder.Encode(15)[0], 15);
        }

        [TestMethod]
        public void TestEncodeMediumInteger()
        {
            Assert.AreEqual(RLPEncoder.Encode(1024)[0], 130);
            Assert.AreEqual(RLPEncoder.Encode(1024)[1], 4);
            Assert.AreEqual(RLPEncoder.Encode(1024)[2], 0);
        }

        [TestMethod]
        public void TestEncodeLongString()
        {
            byte[] result = RLPEncoder.Encode("Lorem ipsum dolor sit amet, consectetur adipisicing elit");
            Assert.AreEqual(result[0], 0xb8);
            Assert.AreEqual(result[1], 0x38);
            Assert.AreEqual(result[2], Convert.ToByte('L'));
            Assert.AreEqual(result[3], Convert.ToByte('o'));
            Assert.AreEqual(result[4], Convert.ToByte('r'));
            Assert.AreEqual(result[5], Convert.ToByte('e'));
            Assert.AreEqual(result[6], Convert.ToByte('m'));
        }
    }
}

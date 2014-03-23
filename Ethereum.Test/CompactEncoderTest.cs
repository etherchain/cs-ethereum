using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ethereum.Utilities;
using System.Text;

namespace Ethereum.Test
{
    [TestClass]
    public class CompactEncoderTest
    {
        private readonly static byte T = 16; // terminator

        [TestMethod]
        public void TestCompactEncodeOne()
        {
            byte[] test = new byte[] {  1, 2, 3, 4, 5 };
            byte[] expected = new byte[] { 0x11, 0x23, 0x45 };
            byte[] result = CompactEncoder.CompactEncode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactEncodeTwo()
        {
            byte[] test = new byte[] { 0, 1, 2, 3, 4, 5 };
            byte[] expected = new byte[] { 0x00, 0x01, 0x23, 0x45 };
            byte[] result = CompactEncoder.CompactEncode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactEncodeThree()
        {
            byte[] test = new byte[] { 0, 15, 1, 12, 11, 8, T };
            byte[] expected = new byte[]  { 0x20, 0x0f, 0x1c, 0xb8 };
            byte[] result = CompactEncoder.CompactEncode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactEncodeFour()
        {
            byte[] test = new byte[] { 15, 1, 12, 11, 8, T };
            byte[] expected = new byte[] { 0x3f, 0x1c, 0xb8 };
            byte[] result = CompactEncoder.CompactEncode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactHexDecode()
        {
            byte[] test = Encoding.ASCII.GetBytes("verb");
            byte[] expected = new byte[] { 7, 6, 6, 5, 7, 2, 6, 2, 16 };
            byte[] result = CompactEncoder.CompactHexDecode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactDecodeOne()
        {
            byte[] test = new byte[] { 0x11, 0x23, 0x45 };
            byte[] expected = new byte[] { 1, 2, 3, 4, 5 };
            byte[] result = CompactEncoder.CompactDecode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactDecodeTwo()
        {
            byte[] test = new byte[] { 0x00, 0x01, 0x23, 0x45 };
            byte[] expected = new byte[] { 0, 1, 2, 3, 4, 5 };
            byte[] result = CompactEncoder.CompactDecode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactDecodeThree()
        {
            byte[] test = new byte[] { 0x20, 0x0f, 0x1c, 0xb8 };
            byte[] expected = new byte[] { 0, 15, 1, 12, 11, 8, T };
            byte[] result = CompactEncoder.CompactDecode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }

        [TestMethod]
        public void TestCompactDecodeFour()
        {
            byte[] test = new byte[] { 0x3f, 0x1c, 0xb8 };
            byte[] expected = new byte[] { 15, 1, 12, 11, 8, T };
            byte[] result = CompactEncoder.CompactDecode(test);
            Assert.AreEqual(Encoding.ASCII.GetString(result), Encoding.ASCII.GetString(expected));
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ethereum.Utilities;
using System.Text;

namespace Ethereum.Test
{
    [TestClass]
    public class RLPDecoderTest
    {
        [TestMethod]
        public void TestDecodeSingleCharacter()
        {
            string test = "64";
            string expected = "d";
            Object result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeSingleString()
        {
            string test = "83646f67";
            string expected = "dog";
            Object result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeEmptyString()
        {
            string test = "80";
            string expected = "";
            Object result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeArrayOfEmptyStrings()
        {
            string test = "c0";
            string[] expected = new string[] { };
            bool expectedBool = (expected == null || expected.Length == 0);
            Object[] result = (Object[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded();
            bool resultBool = result.ContainsOnlyEmpty();
            Assert.AreEqual(expectedBool, resultBool);
        }

        [TestMethod]
        public void TestDecodeZero()
        {
            string test = "80";
            UInt64 expected = 0;
            Object result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeLowInteger()
        {
            string test = "0f";
            UInt64 expected = 15;
            Object result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeMediumInteger()
        {
            string test = "820400";
            UInt64 expected = 1024;
            Object result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeBigInteger()
        {
            string test = "88ffffffffffffffff";
            UInt64 expected = 18446744073709551615;
            Object result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeLongString()
        {
            string test = "b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string expected = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
            Object result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDecodeEmptyStringList()
        {
            string test = "c0";
            string[] expected = new string[0];
            bool expectedBool = (expected == null || expected.Length == 0);
            Object[] result = (Object[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded();
            bool resultBool = result.ContainsOnlyEmpty();
            Assert.AreEqual(expectedBool, resultBool);
        }

        [TestMethod]
        public void TestDecodeShortStringList()
        {
            string test = "cc83646f6783676f6483636174";
            string[] expected = new string[] { "dog", "god", "cat" };
            Object[] result = (Object[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded();
            Assert.AreEqual(expected[0], Encoding.ASCII.GetString((byte[])result[0]));
            Assert.AreEqual(expected[1], Encoding.ASCII.GetString((byte[])result[1]));
            Assert.AreEqual(expected[2], Encoding.ASCII.GetString((byte[])result[2]));
        }

        [TestMethod]
        public void TestDecodeLongStringList() // fails
        {
            string test = "f83e83636174b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string[] expected = new string[] { "cat", "Lorem ipsum dolor sit amet, consectetur adipisicing elit" };
            Object[] result = (Object[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded();
            Assert.AreEqual(expected[0], Encoding.ASCII.GetString((byte[])result[0]));
            Assert.AreEqual(expected[1], Encoding.ASCII.GetString((byte[])result[1]));
        }

        [TestMethod]
        public void TestDecodeMultiList() // fails
        {
            string test = "cc01c48363617483646f67c102";
            Object[] expected = new Object[] { (UInt64)1, new Object[] { "cat" }, "dog", new Object[] { (UInt64)2 } };
            Object[] result = (Object[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded();
            Assert.AreEqual(expected[0], Converter.ConvertByteArrayToUInt64((byte[])result[0]));
            Assert.AreEqual(expected[1].ToString(), result[1].ToString());
            Assert.AreEqual(expected[2], Encoding.ASCII.GetString((byte[])result[2]));
            Assert.AreEqual(((Object[])expected[3])[0], Converter.ConvertByteArrayToUInt64((byte[])((Object[])result[3])[0]));
        }

        [TestMethod]
        public void TestDecodeListOfEmptyLists() // fails
        {
            string test = "c4c2c0c0c0";
            Object[] expected = new Object[] { new Object[] { new Object[] { }, new Object[] { } }, new Object[] { } };
            Object[] result = (Object[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded();
            Assert.AreEqual(expected.ContainsOnlyEmpty(), result.ContainsOnlyEmpty());
        }

        [TestMethod]
        public void TestDecodeTwoListsOfEmptyLists() // fails
        {
            string test = "c7c0c1c0c3c0c1c0";
            Object[] expected = new Object[] { new Object[] { }, new Object[] { new Object[] { } }, new Object[] { new Object[] { }, new Object[] { new Object[] { } } } };
            Object[] result = (Object[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(test), UInt64.MinValue).GetDecoded();
            Assert.AreEqual(expected.ContainsOnlyEmpty(), result.ContainsOnlyEmpty());
        }
    }
}

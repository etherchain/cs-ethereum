using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etherchain.Ethereum.Utilities;
using System.Text;

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
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeSingleString()
        {
            string Test = "83646f67";
            string Expected = "dog";
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeEmptyString()
        {
            string Test = "80";
            string Expected = "";
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeArrayOfEmptyStrings()
        {
            string Test = "c0";
            string[] Expected = new string[] { };
            bool ExpectedBool = (Expected == null || Expected.Length == 0);
            Object Result = RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test));
            string[] ResultAsString = (string[])Result;
            bool ResultBool = (ResultAsString == null || ResultAsString.Length == 0);
            Assert.AreEqual(ExpectedBool, ResultBool);
        }

        [TestMethod]
        public void TestDecodeZero()
        {
            string Test = "80";
            UInt64 Expected = 0;
            Object Result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeLowInteger()
        {
            string Test = "0f";
            UInt64 Expected = 15;
            Object Result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeMediumInteger()
        {
            string Test = "820400";
            UInt64 Expected = 1024;
            Object Result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeBigInteger()
        {
            string Test = "88ffffffffffffffff";
            UInt64 Expected = 18446744073709551615;
            Object Result = Converter.ConvertByteArrayToUInt64((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeLongString()
        {
            string Test = "b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string Expected = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeEmptyStringList()
        {
            string Test = "c0";
            string[] Expected = new string[0];
            bool ExpectedBool = (Expected == null || Expected.Length == 0);
            Object Result = RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test));
            string[] ResultAsString = (string[])Result;
            bool ResultBool = (ResultAsString == null || ResultAsString.Length == 0);
            Assert.AreEqual(ExpectedBool, ResultBool);
        }

        [TestMethod]
        public void TestDecodeShortStringList()
        {
            string Test = "cc83646f6783676f6483636174";
            byte[,] Expected = new byte[,] { { 100, 111, 103 }, { 131, 103, 111 }, { 100, 131, 99 } };
            byte[,] Result = (byte[,])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test));
            Assert.AreEqual(Expected[0, 0], Result[0, 0]);
            Assert.AreEqual(Expected[0, 1], Result[0, 1]);
            Assert.AreEqual(Expected[0, 2], Result[0, 2]);
            Assert.AreEqual(Expected[1, 0], Result[1, 0]);
            Assert.AreEqual(Expected[1, 1], Result[1, 1]);
            Assert.AreEqual(Expected[1, 2], Result[1, 2]);
            Assert.AreEqual(Expected[2, 0], Result[2, 0]);
            Assert.AreEqual(Expected[2, 1], Result[2, 1]);
            Assert.AreEqual(Expected[2, 2], Result[2, 2]);
        }

        [TestMethod]
        public void TestDecodeLongStringList() // fails
        {
            string Test = "f83e83636174b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string[] Expected = new string[] { "cat", "Lorem ipsum dolor sit amet, consectetur adipisicing elit" };
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeMultiList() // fails
        {
            string Test = "cc01c48363617483646f67c102";
            Object[] Expected = new Object[] { 1, new Object[] { "cat" }, "dog", new Object[] { 2 } };
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeListOfEmptyLists() // fails
        {
            string Test = "c4c2c0c0c0";
            Object[] Expected = new Object[] { new Object[] { new Object[] { }, new Object[] { } }, new Object[] { } };
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestDecodeTwoListsOfEmptyLists() // fails
        {
            string Test = "c7c0c1c0c3c0c1c0";
            Object[] Expected = new Object[] { new Object[] { }, new Object[] { new Object[] { } }, new Object[] { new Object[] { }, new Object[] { new Object[] { } } } };
            Object Result = Encoding.ASCII.GetString((byte[])RLPDecoder.Decode(RLPDecoder.StringToByteArray(Test)));
            Assert.AreEqual(Expected, Result);
        }
    }
}

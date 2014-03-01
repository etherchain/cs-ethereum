using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etherchain.Ethereum.Utilities;
using System.Text;

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
        public void TestEncodeSingleCharacter()
        {
            string Test = "d";
            string Expected = "64";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeSingleString()
        {
            string Test = "dog";
            string Expected = "83646f67";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeEmptyString()
        {
            string Test = "";
            string Expected = "80";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeArrayOfEmptyStrings()
        {
            string[] Test = new string[] { };
            string Expected = "c0";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeZero()
        {
            int Test = 0;
            string Expected = "80";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeLowInteger()
        {
            int Test = 15;
            string Expected = "0f";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeMediumInteger()
        {
            int Test = 1024;
            string Expected = "820400";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeBigInteger()
        {
            UInt64 Test = 18446744073709551615;
            string Expected = "88ffffffffffffffff";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeLongString()
        {
            string Test = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
            string Expected = "b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeEmptyStringList()
        {
            string[] Test = new string[0];
            string Expected = "c0";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeShortStringList()
        {
            string[] Test = new string[] { "dog", "god", "cat" };
            string Expected = "cc83646f6783676f6483636174";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeLongStringList()
        {
            string[] Test = new string[] { "cat", "Lorem ipsum dolor sit amet, consectetur adipisicing elit" };
            string Expected = "f83e83636174b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeMultiList()
        {
            Object[] Test = new Object[] { 1, new Object[] { "cat" }, "dog", new Object[] { 2 } };
            string Expected = "cc01c48363617483646f67c102";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeListOfEmptyLists()
        {
            Object[] Test = new Object[] { new Object[] { new Object[] { }, new Object[] { } }, new Object[] { } };
            string Expected = "c4c2c0c0c0";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }
        
        [TestMethod]
        public void TestEncodeTwoListsOfEmptyLists()
        {
            Object[] Test = new Object[] { new Object[] { }, new Object[] { new Object[] { } }, new Object[] { new Object[] { }, new Object[] { new Object[] { } } } };
            string Expected = "c7c0c1c0c3c0c1c0";
            string Result = RLPEncoder.Encode(Test).ToHex();
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod]
        public void TestEncodeLengthLowerThan56() 
        {
            int Length = 1;
            int Offset = 128;
            byte[] Expected = new byte[] { (byte)0x81 }; 
            byte[] EncodedLength = RLPEncoder.EncodeLength(Length, Offset);
            Assert.AreEqual(Expected.ToHex(), EncodedLength.ToHex());
        }

        [TestMethod]
        public void TestEncodeLengthHigherThan55()
        {
            int Length = 56;
            int Offset = 192;
            byte[] Expected = new byte[] { 0xf8, 0x38 };
            byte[] EncodedLength = RLPEncoder.EncodeLength(Length, Offset);
            Assert.AreEqual(Expected.ToHex(), EncodedLength.ToHex());
        }
    }
}

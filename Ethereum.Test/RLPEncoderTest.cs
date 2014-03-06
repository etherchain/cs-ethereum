using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ethereum.Utilities;
using System.Text;

namespace Ethereum.Test.Utilities
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
            string test = "d";
            string expected = "64";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeSingleString()
        {
            string test = "dog";
            string expected = "83646f67";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeEmptyString()
        {
            string test = "";
            string expected = "80";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeArrayOfEmptyStrings()
        {
            string[] test = new string[] { };
            string expected = "c0";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeZero()
        {
            int test = 0;
            string expected = "80";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeLowInteger()
        {
            int test = 15;
            string expected = "0f";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeMediumInteger()
        {
            int test = 1024;
            string expected = "820400";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeBigInteger()
        {
            UInt64 test = 18446744073709551615;
            string expected = "88ffffffffffffffff";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeLongString()
        {
            string test = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
            string expected = "b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeEmptyStringList()
        {
            string[] test = new string[0];
            string expected = "c0";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeShortStringList()
        {
            string[] test = new string[] { "dog", "god", "cat" };
            string expected = "cc83646f6783676f6483636174";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeLongStringList()
        {
            string[] test = new string[] { "cat", "Lorem ipsum dolor sit amet, consectetur adipisicing elit" };
            string expected = "f83e83636174b8384c6f72656d20697073756d20646f6c6f722073697420616d65742c20636f6e7365637465747572206164697069736963696e6720656c6974";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeMultiList()
        {
            Object[] test = new Object[] { 1, new Object[] { "cat" }, "dog", new Object[] { 2 } };
            string expected = "cc01c48363617483646f67c102";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeListOfEmptyLists()
        {
            Object[] test = new Object[] { new Object[] { new Object[] { }, new Object[] { } }, new Object[] { } };
            string expected = "c4c2c0c0c0";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }
        
        [TestMethod]
        public void TestEncodeTwoListsOfEmptyLists()
        {
            Object[] test = new Object[] { new Object[] { }, new Object[] { new Object[] { } }, new Object[] { new Object[] { }, new Object[] { new Object[] { } } } };
            string expected = "c7c0c1c0c3c0c1c0";
            string result = RLPEncoder.Encode(test).ToHex();
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEncodeLengthLowerThan56() 
        {
            int Length = 1;
            int Offset = 128;
            byte[] expected = new byte[] { (byte)0x81 }; 
            byte[] EncodedLength = RLPEncoder.EncodeLength(Length, Offset);
            Assert.AreEqual(expected.ToHex(), EncodedLength.ToHex());
        }

        [TestMethod]
        public void TestEncodeLengthHigherThan55()
        {
            int Length = 56;
            int Offset = 192;
            byte[] expected = new byte[] { 0xf8, 0x38 };
            byte[] EncodedLength = RLPEncoder.EncodeLength(Length, Offset);
            Assert.AreEqual(expected.ToHex(), EncodedLength.ToHex());
        }
    }
}

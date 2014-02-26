﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etherchain.Ethereum.Utilities;

namespace Etherchain.Ethereum.Test.Utilities
{
    [TestClass]
    class RLPEncoderTest
    {
        [TestMethod]
        public void TestEncodeSingleString()
        {
            Assert.AreEqual(RLPEncoder.Encode("dog"), new object[] { 0x83, 'd', 'o', 'g' });
        }

        [TestMethod]
        public void TestEncodeEmptyString()
        {
            Assert.AreEqual(RLPEncoder.Encode(""), new object[] { 0x80 });
        }

        [TestMethod]
        public void TestEncodeArrayOfEmptyStrings()
        {
            Assert.AreEqual(RLPEncoder.Encode(new string[] { }), new object[] { 0xc0 });
        }

        [TestMethod]
        public void TestEncodeLowInteger()
        {
            Assert.AreEqual(RLPEncoder.Encode(15), new object[] { 0x0f });
        }

        [TestMethod]
        public void TestEncodeHighInteger()
        {
            Assert.AreEqual(RLPEncoder.Encode(1024), new object[] { 0x82, 0x04, 0x00 });
        }

        [TestMethod]
        public void TestEncodeLongString()
        {
            Assert.AreEqual(RLPEncoder.Encode("Lorem ipsum dolor sit amet, consectetur adipisicing elit"), new object[] { 0xb8, 0x38, 'L', 'o', 'r', 'e', 'm', ' ', 'i', 'p', 's', 'u', 'm', ' ', 'd', 'o', 'l', 'o', 'r', ' ', 's', 'i', 't', ' ', 'a', 'm', 'e', 't', ',', ' ', 'c', 'o', 'n', 's', 'e', 'c', 't', 'e', 't', 'u', 'r', ' ', 'a', 'd', 'i', 'p', 'i', 's', 'i', 'c', 'i', 'n', 'g', ' ', 'e', 'l', 'i', 't' });
        }
    }
}

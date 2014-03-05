using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ethereum;

namespace Ethereum.Test
{
    [TestClass]
    public class DiscReasonTest
    {
        [TestMethod]
        public void TestGetDiscReason()
        {
            Assert.AreEqual(DiscReason.GetDiscReason(0),"Disconnect requested");
            Assert.AreEqual(DiscReason.GetDiscReason(1), "Disconnect TCP sys error");
            Assert.AreEqual(DiscReason.GetDiscReason(6), "Disconnect wrong genesis block");
            Assert.AreEqual(DiscReason.GetDiscReason(7), "Disconnect incompatible network");
            Assert.AreEqual(DiscReason.GetDiscReason(8), "Unknown");
            Assert.AreEqual(DiscReason.GetDiscReason(9), "Unknown");
            Assert.AreEqual(DiscReason.GetDiscReason(100), "Unknown");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etherchain.Ethereum
{
    public static class DiscReason
    {
        private const string DiscReRequested = "0x00";
        private const string DiscReTcpSysErr = "0x01";
        private const string DiscBadProto = "0x02";
        private const string DiscBadPeer = "0x03";
        private const string DiscTooManyPeers = "0x04";
        private const string DiscConnDup = "0x05";
        private const string DiscGenesisErr = "0x06";
        private const string DiscProtoErr = "0x07";

        private static string[] DiscReasonToString = 
        {
            "Disconnect requested",
            "Disconnect TCP sys error",
	        "Disconnect bad protocol",
	        "Disconnect useless peer",
	        "Disconnect too many peers",
	        "Disconnect already connected",
	        "Disconnect wrong genesis block",
	        "Disconnect incompatible network"
        };

        public static string GetDiscReason(uint discReason)
        {
            if (discReason >= DiscReasonToString.Length)
            {
                return "Unknown";
            }
            return DiscReasonToString[discReason];
        }
    }
}

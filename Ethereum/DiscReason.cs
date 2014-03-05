using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum
{
    public static class DiscReason
    {
        private const int DiscReRequested = 0x00;
        private const int DiscReTcpSysErr = 0x01;
        private const int DiscBadProto = 0x02;
        private const int DiscBadPeer = 0x03;
        private const int DiscTooManyPeers = 0x04;
        private const int DiscConnDup = 0x05;
        private const int DiscGenesisErr = 0x06;
        private const int DiscProtoErr = 0x07;

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

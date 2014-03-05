using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum
{
    public class Caps
    {
        private const int CapPeerDiscTy = 0x01;
        private const int CapTxTy = 0x02;
        private const int CapChainTy = 0x04;

        private string[] CapsToString = 
        {
            "Peer discovery",
            "Transaction relaying",
	        "Block chain relaying"
        };

        public bool IsCap(byte capOne, byte capTwo)
        {
            return (capOne & capTwo) > 0;
        }
    }
}

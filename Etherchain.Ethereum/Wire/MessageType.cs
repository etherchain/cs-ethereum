using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Wire
{
    class MessageType
    {
        private readonly byte[] MagicToken = { 34, 64, 8, 145 };
        
        public Dictionary<string, string> MessageTypeToString = new Dictionary<string, string>
        {
            { "MessageHandshakeTy", "Handshake" },
            { "MessageDiscTy", "Disconnect" },
            { "MessagePingTy", "Ping" },
            { "MessagePongTy", "Pong" },
            { "MessageGetPeersTy", "Get peers" },
            { "MessagePeersTy", "Peers" },
            { "MessageTxTy", "Transactions" },
            { "MessageBlockTy", "Blocks" },
            { "MessageGetChainTy", "Get chain" },
            { "MessageNotInChainTy", "Not in chain" }
        };
    }
}

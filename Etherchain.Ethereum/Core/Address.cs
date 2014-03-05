using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Ethereum.Core
{
    class Address
    {
        public BigInteger Amount;
        public UInt64 Nonce;

        public Address(BigInteger amount)
        {
            this.Amount = amount;
        }

        public Address(BigInteger amount, UInt64 nonce)
        {
            this.Amount = amount;
            this.Nonce = nonce;
        }

        public Address(byte[] data)
        {
            this.RlpDecode(data);
        }

        public void AddFee(BigInteger fee)
        {
            this.Amount += fee;
        }

        //public byte[] RlpEncode()
        //{
        //    return RlpEncoder.Encode();
        //}

        public void RlpDecode(byte[] data)
        {

        }
    }
}

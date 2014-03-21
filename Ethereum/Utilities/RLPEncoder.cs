using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Utilities
{
    public static class RLPEncoder
    {
        private static readonly UInt64 MaxItemLength = UInt64.MaxValue;
        private static readonly int SizeThreshold = 56;
        private static readonly int OffsetShortItem = 0x80;
        private static readonly int OffsetShortList = 0xc0;
        
        public static byte[] Encode(Object input)
        {
            if (input is Array)
            {
                Object[] inputArray = (Object[])input;
                if (inputArray.Length == 0)
                {
                    return EncodeLength(inputArray.Length, OffsetShortList);
                }
                byte[] output = new byte[0];
                foreach (Object arrayObject in inputArray)
                {
                    output = Encoder.ConcatenateByteArrays(output, Encode(arrayObject));
                }
                byte[] prefix = EncodeLength(output.Length, OffsetShortList);
                return Encoder.ConcatenateByteArrays(prefix, output);
            }
            else
            {
                if (input == null)
                {
                    throw new ArgumentNullException("The input is null");
                }
                byte[] inputAsHex = Encoder.ToHex(input);
                if (inputAsHex.Length == 1)
                {
                    return inputAsHex;
                }
                else
                {
                    byte[] firstByte = EncodeLength(inputAsHex.Length, OffsetShortItem);
                    return Encoder.ConcatenateByteArrays(firstByte, inputAsHex);
                }
            }
        }

        public static byte[] EncodeLength(int length, int offset)
        {
            if (length < SizeThreshold)
            {
                byte firstByte = (byte)(length + offset);
                return new byte[] { firstByte };
            }
            else if ((UInt64)length < MaxItemLength)
            {
                byte[] binaryLength = Encoder.ConvertUInt64ToByteArray((UInt64)length);
                byte firstByte = (byte)(binaryLength.Length + offset + SizeThreshold - 1);
                return Encoder.ConcatenateByteArrays(new byte[] { firstByte }, binaryLength);
            }
            throw new Exception("Input too long");
        }
    }
}

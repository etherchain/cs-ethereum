using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etherchain.Ethereum.Utilities
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
                    output = ConcatenateByteArrays(output, Encode(arrayObject));
                }
                byte[] prefix = EncodeLength(output.Length, OffsetShortList);
                return ConcatenateByteArrays(prefix, output);
            }
            else
            {
                if (input == null)
                {
                    throw new ArgumentNullException("The input is null");
                }
                byte[] inputAsHex = ToHex(input);
                if (inputAsHex.Length == 1)
                {
                    return inputAsHex;
                }
                else
                {
                    byte[] firstByte = EncodeLength(inputAsHex.Length, OffsetShortItem);
                    return ConcatenateByteArrays(firstByte, inputAsHex);
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
                byte[] binaryLength = ConvertUInt64ToByteArray((UInt64)length);
                byte firstByte = (byte)(binaryLength.Length + offset + SizeThreshold - 1);
                return ConcatenateByteArrays(new byte[] { firstByte }, binaryLength);
            }
            throw new Exception("Input too long");
        }

        private static byte[] ToHex(Object input) {
            UInt64 inputInt;
            bool isNum = UInt64.TryParse(input.ToString(), out inputInt);
            if (input is string) 
            {
                string inputString = (string) input;
                return Encoding.ASCII.GetBytes(inputString.ToCharArray());
            }
            else if (isNum) 
            {
                return (inputInt == 0) ? new byte[0] : ConvertUInt64ToByteArray(inputInt);
            }
            throw new Exception("Unsupported type: Only accepting String, Integer and BigInteger for now");
        }

        private static byte[] ConcatenateByteArrays(byte[] inputArrayOne, byte[] inputArrayTwo)
        {
            byte[] result = new byte[inputArrayOne.Length + inputArrayTwo.Length];
            Array.Copy(inputArrayOne, 0, result, 0, inputArrayOne.Length);
            Array.Copy(inputArrayTwo, 0, result, inputArrayOne.Length, inputArrayTwo.Length);
            return result;
        }

        private static byte[] ConvertUInt64ToByteArray(UInt64 input)
        {
            byte[] uInt64AsByteArray = BitConverter.GetBytes(input);
            Array.Reverse(uInt64AsByteArray);
            var i = 0;
            while (uInt64AsByteArray[i] == 0)
            {
                ++i;
            }
            byte[] result = new byte[uInt64AsByteArray.Length - i];
            Array.Copy(uInt64AsByteArray, i, result, 0, uInt64AsByteArray.Length - i);
            return result;
        }
    }
}

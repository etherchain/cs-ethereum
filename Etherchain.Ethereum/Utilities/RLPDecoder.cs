using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etherchain.Ethereum.Utilities
{
    public static class RLPDecoder
    {
        public static Object DecodeWithReader(BufferedStream reader)
        {
            return null;
        }

        public static Object Decode(byte[] input)
        {
            if (input[0] < 0x7f)
            {
                return input;
            }

            if (input[0] <= 0xb7)
            {
                return RemoveFirstXBytes(input, 1);
            }

            if (input[0] <= 0xbf)
            {
                int firstByte = (int)input[0];
                return RemoveFirstXBytes(input, firstByte - 182);
            }

            if (input[0] <= 0xf7)
            {
                if (input[0] == 0xc0)
                {
                    return new string[] { };
                }
                byte[] list = RemoveFirstXBytes(input, 1);
                return SplitListIntoMultipleByteArrays(list);
            }

            if (input[0] <= 0xff)
            {
                byte[] list = RemoveFirstXBytes(input, 2);
                return SplitListIntoMultipleByteArrays(list);
            }
            return null;
        }

        private static byte[] RemoveFirstXBytes(byte[] inputArray, int amount)
        {
            byte[] result = new byte[inputArray.Length - amount];
            Array.Copy(inputArray, amount, result, 0, inputArray.Length - amount);
            return result;
        }

        private static byte[][] SplitListIntoMultipleByteArrays(byte[] inputArray)
        {
            List<byte[]> byteArrayList = new List<byte[]>();
            int counter = 0;
            foreach (byte value in inputArray)
            {
                if ((int)value > 127)
                {
                    int stringLength = value - 127;
                    if (stringLength > 0)
                    {
                        if ((int)value > 183)
                        {
                            stringLength += value - 183;
                            // todo: return empty lists
                            //if ((int)value > 191)
                            //{
                            //    stringLength = value - 191;
                            //}
                            byte[] tempArray = new byte[stringLength];
                            Array.Copy(inputArray, counter, tempArray, 0, stringLength);
                            counter += stringLength;
                            byteArrayList.Add((byte[])Decode(tempArray));
                        }
                        else
                        {
                            byte[] tempArray = new byte[stringLength];
                            Array.Copy(inputArray, counter, tempArray, 0, stringLength);
                            counter += stringLength;
                            byteArrayList.Add((byte[])Decode(tempArray));
                        }
                    }
                }
            }
            byte[][] result = byteArrayList.ToArray();
            return result;
        }

        // Code from: http://stackoverflow.com/questions/321370/convert-hex-string-to-byte-array
        public static byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        private static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : 87);
        }
    }
}

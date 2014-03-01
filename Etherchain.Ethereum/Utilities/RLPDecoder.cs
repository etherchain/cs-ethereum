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

        public static Object Decode(string input)
        {
            if (input.Length > 1)
            {
                byte[] inputAsArray = StringToByteArray(input);
                if (inputAsArray[0] < 0x7f)
                {
                    return Encoding.ASCII.GetString(inputAsArray);
                }
                else if (inputAsArray[0] >= 0x80 && inputAsArray[0] <= 0xb7)
                {
                    return Encoding.ASCII.GetString(RemoveFirstXBytes(inputAsArray, 1));
                }
                else if (inputAsArray[0] >= 0xb8 && inputAsArray[0] <= 0xbf)
                {

                }
                else if (inputAsArray[0] >= 0xc0 && inputAsArray[0] <= 0xf7)
                {

                }
                else if (inputAsArray[0] >= 0xf8 && inputAsArray[0] <= 0xff)
                {

                }
            }
            return null;
        }

        private static byte[] RemoveFirstXBytes(byte[] inputArray, int amount)
        {
            byte[] result = new byte[inputArray.Length - amount];
            Array.Copy(inputArray, amount, result, 0, inputArray.Length - 1);
            return result;
        }

        // Code from http://stackoverflow.com/questions/321370/convert-hex-string-to-byte-array
        private static byte[] StringToByteArray(string hex)
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

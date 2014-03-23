using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Utilities
{
    public static class CompactEncoder
    {
        private static byte TERMINATOR = 16;

        /*public static string CompactEncode(byte[] key)
        {
            bool keyHasTerminator;

            if (key[key.Length - 1] == TERMINATOR)
            {
                keyHasTerminator = true;
                key = key.Slice(0, BitConverter.ToUInt64(BitConverter.GetBytes(key.LongLength), 0) - 1);
            }

            UInt64 oddlen = BitConverter.ToUInt64(BitConverter.GetBytes(key.LongLength), 0) % 2;
            int flag = 2 * keyHasTerminator + oddlen;
            if (oddlen != 0)
            {
                int[] flags = new int[] { flag };
                key = concatenate(flags, key);
            }
            else
            {
                int[] flags = new int[] { flag, 0 };
                key = concatenate(flags, key);
            }

            OutputStream buffer = new ByteArrayOutputStream();
            for (int i = 0; i < key.Length; i += 2)
            {
                try
                {
                    buffer.write((16 * key[i] + key[i + 1]));
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }
            }
            return buffer.toString();
        }*/

        public static byte[] CompactDecode(byte[] str)
        {
            byte[] result = CompactHexDecode(str);
            result = Encoder.RemoveLastXBytes(result, 1);
            if (result[0] >= 2)
            {
                result = Encoder.AppendByteToArray(result, TERMINATOR);
            }
            if (result[0] % 2 == 1)
            {
                result = Encoder.RemoveFirstXBytes(result, 1);
            }
            else
            {
                result = Encoder.RemoveFirstXBytes(result, 2);
            }
            return result;
        }

        public static byte[] CompactHexDecode(byte[] hexEncoded) 
        {
            char[] charArray = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            byte[] hexSlice = new byte[0];
 		    foreach (byte number in hexEncoded) 
            {
                string hexValue = number.ToHex();
                char[] hexValueSplitted = hexValue.ToCharArray();
                hexSlice = Encoder.AppendByteToArray(hexSlice, (byte)Array.IndexOf(charArray, hexValueSplitted[0]));
                hexSlice = Encoder.AppendByteToArray(hexSlice, (byte)Array.IndexOf(charArray, hexValueSplitted[1]));
 		    }
            hexSlice = Encoder.AppendByteToArray(hexSlice, TERMINATOR);
 		    return hexSlice;
        }
    }
}

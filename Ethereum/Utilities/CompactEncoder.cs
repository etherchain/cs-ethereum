using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ethereum.Utilities
{
    public static class CompactEncoder
    {
        private static byte TERMINATOR = 16;

        public static byte[] CompactEncode(byte[] hexSlice)
        {
            byte terminator = 0;

            if (hexSlice[hexSlice.Length - 1] == TERMINATOR)
            {
                terminator = 1;
                hexSlice = Encoder.RemoveLastXBytes(hexSlice, 1);
            }

            int oddlen = hexSlice.Length % 2;
            int flag = 2 * terminator + oddlen;
            if (oddlen != 0)
            {
                byte[] flags = new byte[] { (byte)flag };
                hexSlice = Encoder.ConcatenateByteArrays(flags, hexSlice);
            }
            else
            {
                byte[] flags = new byte[] { (byte)flag, 0 };
                hexSlice = Encoder.ConcatenateByteArrays(flags, hexSlice);
            }

            MemoryStream buffer = new MemoryStream();
            for (int i = 0; i < hexSlice.Length; i += 2)
            {
                buffer.WriteByte((byte)(16 * hexSlice[i] + hexSlice[i + 1]));
            }
            return buffer.ToArray();
        }

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

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
        }

        public static int[] compactDecode(String str)
        {
            int[] Base = compactHexDecode(str);
            Base = Arrays.copyOf(Base, Base.Length - 1);
            if (Base[0] >= 2)
            {
                Base = concatenate(Base, TERMINATOR);
            }
            if (Base[0] % 2 == 1)
            {
                Base = Arrays.copyOfRange(Base, 1, Base.Length);
            }
            else
            {
                Base = Arrays.copyOfRange(Base, 2, Base.Length);
            }
            return Base;
        }*/

        public static byte[] CompactHexDecode(string str) 
        {
            char[] charArray = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            byte[] hexSlice = new byte[0];
            byte[] hexEncoded = Encoder.ToHex(str);
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

        /**
         * Cast hex encoded value from byte[] to int
         * 
         * Limited to Integer.MAX_VALUE: 2^32-1
         * 
         * @param b array contains the hex values
         * @return int value of all hex values together. 
         */
        /*public static int toInt(byte[] b)
        {
            if (b == null || b.Length == 0)
            {
                return 0;
            }
            return new BigInteger(b).intValue();
        }

        public static byte[] concatenate(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.arraycopy(a, 0, c, 0, a.Length);
            System.arraycopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        public static int[] concatenate(int[] a, int[] b)
        {
            int[] c = new int[a.Length + b.Length];
            System.arraycopy(a, 0, c, 0, a.Length);
            System.arraycopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        public static int[] concatenate(int[] a, int b)
        {
            int[] c = new int[a.Length + 1];
            System.arraycopy(a, 0, c, 0, a.Length);
            c[c.Length] = b;
            return c;
        }*/

        /// Code from http://blogs.msdn.com/b/blambert/archive/2009/02/22/blambert-codesnip-fast-byte-array-to-hex-string-conversion.aspx

        private static readonly string[] HexStringTable = new string[]
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0a", "0b", "0c", "0d", "0e", "0f",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1a", "1b", "1c", "1d", "1e", "1f",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2a", "2b", "2c", "2d", "2e", "2f",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3a", "3b", "3c", "3d", "3e", "3f",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4a", "4b", "4c", "4d", "4e", "4f",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5a", "5b", "5c", "5d", "5e", "5f",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6a", "6b", "6c", "6d", "6e", "6f",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7a", "7b", "7c", "7d", "7e", "7f",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8a", "8b", "8c", "8d", "8e", "8f",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9a", "9b", "9c", "9d", "9e", "9f",
            "a0", "a1", "a2", "a3", "a4", "a5", "a6", "a7", "a8", "a9", "aa", "ab", "ac", "ad", "ae", "af",
            "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8", "b9", "ba", "bb", "bc", "bd", "be", "bf",
            "c0", "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "ca", "cb", "cc", "cd", "ce", "cf",
            "d0", "d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8", "d9", "da", "db", "dc", "dd", "de", "df",
            "e0", "e1", "e2", "e3", "e4", "e5", "e6", "e7", "e8", "e9", "ea", "eb", "ec", "ed", "ee", "ef",
            "f0", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "fa", "fb", "fc", "fd", "fe", "ff"
        };

        public static string ToHex(this byte value)
        {
            return HexStringTable[value];
        }
    }
}

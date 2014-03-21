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
    }
}

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
        private static int RlpEmptyList = 0x80;
        private static int RlpEmptyStr = 0x40;

        private static int DirectRlp = 0x7f;
        private static int NumberRlp = 0xb7;
        private static int ZeroRlp = 0x0;

        public static int Character(byte[] character)
        {
            if (character.Length > 0)
            {
                return (int)character[0];
            }
            return 0;
        }

        public static Object decodeWithReader(BufferedStream reader) 
        {
            return null;
        }
        
        public static Object decode(byte[] data, long pos) 
        {
		    return null;
        }

        public static byte[] Encode(object objectToEncode)
        {
            if (objectToEncode is string)
            {
                string objectAsString = objectToEncode.ToString();

                if (String.IsNullOrEmpty(objectAsString))
                {
                    return new byte[] { 0x80 };
                }

                if (objectAsString.Length == 1)
                {
                    byte[] encodedObject = Encoding.ASCII.GetBytes(objectAsString.ToCharArray());
                    if (encodedObject[0] < 128)
                    {
                        return new byte[] { encodedObject[0] };
                    }
                }
                else
                {
                    byte[] returnArray = new byte[objectAsString.Length + 1];
                    returnArray[0] = EncodeLength(objectAsString.Length, 128);
                    byte[] arrayAsBytes = Encoding.ASCII.GetBytes(objectAsString.ToCharArray());
                    Array.Copy(arrayAsBytes, 0, returnArray, 1, objectAsString.Length);
                    return returnArray;
                }
            }
            if (objectToEncode is int)
            {
                if ((int)objectToEncode < 56)
                {
                    return new byte[] { Convert.ToByte(objectToEncode) };
                }
                else if ((int)objectToEncode < Math.Pow(256, 8))
                {

                }
            }
            return null;
        }

        public static object[] Encode(object[] objectToEncode)
        {
            if (objectToEncode.ToString().Length == 1 && File.ReadAllBytes(objectToEncode.ToString())[0] < 128)
            {
                return objectToEncode;
            }
		    return null;
        }

        private static byte EncodeLength(int length, int offset)
        {
            if (length < 56)
            {
                return (byte)(length + offset) ;
            }
            return 0;
        }
    }
}

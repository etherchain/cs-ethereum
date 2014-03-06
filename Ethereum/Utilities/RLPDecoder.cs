using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Utilities
{
    public static class RLPDecoder
    {
        /** RLP encoding rules are defined as follows: */

        /*
         * For a single byte whose value is in the [0x00, 0x7f] range, that byte is
         * its own RLP encoding.
         */

        /*
         * If a string is 0-55 bytes long, the RLP encoding consists of a single
         * byte with value 0x80 plus the length of the string followed by the
         * string. The range of the first byte is thus [0x80, 0xb7].
         */
        private static readonly UInt64 OffsetShortItem = 0x80;

        /*
         * If a string is more than 55 bytes long, the RLP encoding consists of a
         * single byte with value 0xb7 plus the length of the length of the string
         * in binary form, followed by the length of the string, followed by the
         * string. For example, a length-1024 string would be encoded as
         * \xb9\x04\x00 followed by the string. The range of the first byte is thus
         * [0xb8, 0xbf].
         */
        private static readonly UInt64 OffsetLongItem = 0xb8;

        /*
         * If the total payload of a list (i.e. the combined length of all its
         * items) is 0-55 bytes long, the RLP encoding consists of a single byte
         * with value 0xc0 plus the length of the list followed by the concatenation
         * of the RLP encodings of the items. The range of the first byte is thus
         * [0xc0, 0xf7].
         */
        private static readonly UInt64 OffsetShortList = 0xc0;

        /*
         * If the total payload of a list is more than 55 bytes long, the RLP
         * encoding consists of a single byte with value 0xf7 plus the length of the
         * length of the list in binary form, followed by the length of the list,
         * followed by the concatenation of the RLP encodings of the items. The
         * range of the first byte is thus [0xf8, 0xff].
         */
        private static readonly UInt64 OffsetLongList = 0xf8;
        private static readonly UInt64 MaxPrefix = 0xff;

        public static DecodeResult Decode(byte[] data, UInt64 pos)
        {
            if (data == null || data.Length < 1)
            {
                return null;
            }

            UInt64 prefix = data[pos] & MaxPrefix;
            if (prefix == OffsetShortItem)
            {
                return new DecodeResult(pos + 1, new byte[0]); // means no length or 0
            }
            else if (prefix < OffsetShortItem)
            {
                return new DecodeResult(pos + 1, new byte[] { data[pos] }); // byte is its own RLP encoding
            }
            else if (prefix < OffsetLongItem)
            {
                UInt64 len = prefix - OffsetShortItem; // length of the encoded bytes
                return new DecodeResult(pos + 1 + len, data.Slice(pos + 1, pos + 1 + len));
            }
            else if (prefix < OffsetShortList)
            {
                UInt64 lenlen = prefix - OffsetLongItem + 1; // length of length the encoded bytes
                UInt64 lenbytes = Converter.ConvertByteArrayToUInt64(data.Slice(pos + 1, pos + 1 + lenlen)); // length of encoded bytes
                return new DecodeResult(pos + 1 + lenlen + lenbytes, data.Slice(pos + 1 + lenlen, pos + 1 + lenlen + lenbytes));
            }
            else if (prefix < OffsetLongList)
            {
                UInt64 len = prefix - OffsetShortList; // length of the encoded list
                UInt64 prevPos = pos; pos++;
                return DecodeList(data, pos, prevPos, len);
            }
            else if (prefix < MaxPrefix)
            {
                UInt64 lenlen = prefix - OffsetLongList + 1; // length of length the encoded list
                UInt64 lenlist = Converter.ConvertByteArrayToUInt64(data.Slice(pos + 1, pos + 1 + lenlen)); // length of encoded bytes
                pos = pos + lenlen + 1;
                UInt64 prevPos = lenlist;
                return DecodeList(data, pos, prevPos, lenlist);
            }
            else
            {
                throw new Exception("Only byte values between 0x00 and 0xFF are supported, but got: " + prefix);
            }
        }

        private static DecodeResult DecodeList(byte[] data, UInt64 pos, UInt64 prevPos, UInt64 len)
        {
            List<Object> slice = new List<Object>(); // temporary list for easily adding bytes arrays
            for (UInt64 i = 0; i < len; )
            {
                // Get the next item in the data list and append it
                DecodeResult result = Decode(data, pos);
                slice.Add(result.GetDecoded());
                // Increment pos by the amount bytes in the previous read
                prevPos = result.GetPosition();
                i += (prevPos - pos);
                pos = prevPos;
            }
            return new DecodeResult(pos, slice.ToArray());
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

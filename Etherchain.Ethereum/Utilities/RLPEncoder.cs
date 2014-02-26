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
            //Object[] slice;
            
            //// Read the next byte
            //char character = char(reader.BeginRead());
            
            //if (character == 0)
            //    return null;
            //else if(character <= 0x7c)
            //    return character;
            //else if (character <= 0xb7)
            //    return reader.Next((int)(character - 0x80));
            //else if (character <= 0xbf) 
            //{
            //    buff = bytes.NewReader(reader.Next((int)(character - 0xb8)));
            //    length = ReadVarint(buff);
            //    return reader.Next((int)length);
            //}
            //else if(character <= 0xf7) 
            //{
            //    length = (int)(character - 0xc0);
            //    for (int i = 0; i < length; i++) 
            //    {
            //        obj = decodeWithReader(reader);
            //        if (obj != null) 
            //        {
            //            slice = append(slice, obj);
            //        } 
            //        else 
            //        {
            //            break;
            //        }
            //    }
            //    return slice;
            //}
            //return slice;
            return null;
        }
        
        // TODO Use a bytes.Buffer instead of a raw byte slice.
        // Cleaner code, and use draining instead of seeking the next bytes to read
        
        public static Object decode(byte[] data, long pos) 
        {
		// return (Object, uint64)
		/*
			if pos > uint64(len(data)-1) {
				log.Println(data)
				log.Panicf("index out of range %d for data %q, l = %d", pos, data, len(data))
			}
		*/

//		Object[] slice;
//		char character = (int) data[pos];
//		if (character <= 0x7f)
//			return data[pos], pos + 1;
//		else if (character <= 0xb7) {
//			b = uint64(data[pos]) - 0x80;
//			return data[pos+1 : pos+1+b], pos + 1 + b;
//		}
//		else if (character <= 0xbf) {
//			b = uint64(data[pos]) - 0xb7;
//			b2 = ReadVarint(bytes.NewReader(data[pos+1 : pos+1+b]));
//			return data[pos+1+b : pos+1+b+b2], pos + 1 + b + b2;
//		}
//		else if (character <= 0xf7) {
//			b = uint64(data[pos]) - 0xc0;
//			prevPos = pos;
//			pos++;
//			for (int i = uint64(0); i < b;) {
//				Object obj;
//				// Get the next item in the data list and append it
//				obj, prevPos = Decode(data, pos);
//				slice = append(slice, obj);
//	
//				// Increment i by the amount bytes read in the previous
//				// read
//				i += (prevPos - pos);
//				pos = prevPos;
//			}
//			return slice, pos;
//		}
//		else if (character <= 0xff) {
//			l = uint64(data[pos]) - 0xf7;
//			//b := BigD(data[pos+1 : pos+1+l]).Uint64()
//			b = ReadVarint(bytes.NewReader(data[pos+1 : pos+1+l]));
//	
//			pos = pos + l + 1;
//	
//			prevPos = b;
//			for (int i = uint64(0); i < uint64(b);) {
//				Object obj;
//	
//				obj, prevPos = Decode(data, pos);
//				slice = append(slice, obj);
//	
//				i += (prevPos - pos);
//				pos = prevPos;
//			}
//			return slice, pos;
//		}
//		else {
//			panic(fmt.Sprintf("byte not supported: %q", char));
//		}
//		return slice, 0;
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
                    byte[] encodedObject = File.ReadAllBytes(objectAsString);
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

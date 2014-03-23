using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Utilities
{
    public static class Encoder
    {
        public static byte[] ToHex(Object input)
        {
            UInt64 inputInt;
            bool isNum = UInt64.TryParse(input.ToString(), out inputInt);
            if (isNum)
            {
                return (inputInt == 0) ? new byte[0] : ConvertUInt64ToByteArray(inputInt);
            }
            else if (input is string)
            {
                string inputString = (string)input;
                return Encoding.ASCII.GetBytes(inputString.ToCharArray());
            }
            throw new Exception("Unsupported type: Only accepting String, Integer and BigInteger for now");
        }

        public static byte[] ConvertUInt64ToByteArray(UInt64 input)
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

        public static byte[] ConcatenateByteArrays(byte[] inputArrayOne, byte[] inputArrayTwo)
        {
            byte[] result = new byte[inputArrayOne.Length + inputArrayTwo.Length];
            Array.Copy(inputArrayOne, 0, result, 0, inputArrayOne.Length);
            Array.Copy(inputArrayTwo, 0, result, inputArrayOne.Length, inputArrayTwo.Length);
            return result;
        }

        public static byte[] AppendByteToArray(byte[] inputArray, byte inputByte)
        {
            byte[] result = new byte[inputArray.Length + 1];
            Array.Copy(inputArray, 0, result, 0, inputArray.Length);
            result[result.Length - 1] = inputByte;
            return result;
        }

        public static byte[] RemoveLastXBytes(byte[] inputArray, int amountOfBytes)
        {
            byte[] result = new byte[inputArray.Length - amountOfBytes];
            Array.Copy(inputArray, 0, result, 0, inputArray.Length - amountOfBytes);
            return result;
        }

        public static byte[] RemoveFirstXBytes(byte[] inputArray, int amountOfBytes)
        {
            byte[] result = new byte[inputArray.Length - amountOfBytes];
            Array.Copy(inputArray, amountOfBytes, result, 0, inputArray.Length - amountOfBytes);
            return result;
        }
    }
}

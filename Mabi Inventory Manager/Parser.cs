using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mabi_Inventory_Manager {
    static class Parser {

        private static byte[] emptyString = { 0x00 };

        /// <summary>
        /// Read packet data starting for a given point.
        /// First byte is the data type of the following data.
        /// 0 - None
        /// 1 - Byte
        /// 2 - Short
        /// 3 - Int
        /// 4 - Long
        /// 5 - Float
        /// 6 - String
        /// 7 - Binary
        /// For string and binary data types, the next two bytes are the size of string or binary data
        /// start is moved to end of the read data
        /// 
        /// </summary>
        /// <param name="data">packet data</param>
        /// <param name="start">reading point</param>
        /// <returns>the binary data snippet</returns>
        public static byte[] GetNext(byte[] data, ref int start) {
            var infoType = data[start];
            byte[] info = new byte[1];
            var infoStart = start + 1;
            var infoLength = 1;
            byte[] size = new byte[2];
            switch (infoType) {
                case 2:
                    infoLength = 2;
                    break;
                case 3:
                    infoLength = 4;
                    break;
                case 4:
                    infoLength = 8;
                    break;
                case 5:
                    infoLength = 4;
                    break;
                case 6:
                    infoStart = start + 3;
                    size[0] = data[start + 2];
                    size[1] = data[start + 1];
                    infoLength = BitConverter.ToInt16(size, 0);
                    break;
                case 7:
                    infoStart = start + 3;
                    size[0] = data[start + 2];
                    size[1] = data[start + 1];
                    infoLength = BitConverter.ToInt16(size, 0);
                    break;
            }
            Array.Resize(ref info, infoLength);
            Array.Copy(data, infoStart, info, 0, infoLength);
            start = infoStart + infoLength;
            // if the data type is binary, do not reverse
            if (infoType == 7)
            {
                return info;
            }
            else
            {
                return info.Reverse().ToArray();
            }
            
        }
        /// <summary>
        /// Read binary data from a given point of a given length.
        /// start is moved to end of the read data
        /// </summary>
        /// <param name="data">packet data</param>
        /// <param name="start">reading point</param>
        /// <param name="length">reading length</param>
        /// <returns>the binary data snippet</returns>
        public static byte[] GetNextRev(byte[] data, ref int start, int length)
        {
            var a = data.Skip(start).Take(length).ToArray();
            start += length;
            return a;
        }
        /// <summary>
        /// Converts binary data to its corresponding byte value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>byte value</returns>
        public static byte ConvertByte(byte[] data)
        {
            return data[0];
        }

        /// <summary>
        /// Converts binary data to its corresponding sbyte value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>sbyte value</returns>
        public static sbyte ConvertSByte(byte[] data)
        {
            return unchecked((sbyte)data[0]);
        }

        /// <summary>
        /// Converts binary data to its corresponding short value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>short value</returns>
        public static short ConvertShort(byte[] data)
        {
                return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// Converts binary data to its corresponding ushort value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>ushort value</returns>
        public static ushort ConvertUShort(byte[] data)
        {
            return BitConverter.ToUInt16(data, 0);
        }

        /// <summary>
        /// Converts binary data to its corresponding int value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>int value</returns>
        public static int ConvertInt(byte[] data)
        {
                return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Converts binary data to its corresponding uint value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>uint value</returns>
        public static uint ConvertUInt(byte[] data)
        {
            return BitConverter.ToUInt32(data, 0);
        }

        /// <summary>
        /// Converts binary data to its corresponding long value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>long value</returns>
        public static long ConvertLong(byte[] data)
        {
            return BitConverter.ToInt64(data, 0);
        }

        /// <summary>
        /// Converts binary data to its corresponding string value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>string value</returns>
        public static string ConvertString(byte[] data)
        {
            /*
            var str = Encoding.Unicode.GetString(data);
            if (str.Length == 1 && (int) str[0] == 0)
            {
                return " ";
            }
            else
            {
                return str;
            }
            */
            return Encoding.Unicode.GetString(data);
        }

        /// <summary>
        /// Converts binary data to its corresponding color value.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns>color value</returns>
        public static Color ConvertColor(byte[] data)
        {
            var colorInt = BitConverter.ToUInt32(data, 0);
            var blue = colorInt & 255;
            var green = (colorInt >> 8) & 255;
            var red = (colorInt >> 16) & 255;
            var alpha = (colorInt >> 24) & 255;
            var c = Color.FromRgb((byte) red, (byte) green, (byte) blue);
            return c;
        }

    }
}

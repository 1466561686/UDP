using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP.Tools
{
    public class ConvertHelper
    {
        /// <summary>
        /// 将16进制转换为有符号的10进制
        /// </summary>
        /// <param name="hexstr"></param>
        /// <returns></returns>
        public static string ConvertHexToSIntStr(string hexstr)
        {

            if (hexstr.StartsWith("0x"))
            {
                hexstr = hexstr.Substring(2);
            }

            //如果不是有效的16进制字符串或者字符串长度大于16或者是空，均返回NULL

            if (!IsHexadecimal(hexstr) || hexstr.Length > 16 || string.IsNullOrEmpty(hexstr))
            {
                return null;
            }
            if (hexstr.Length > 8)
            {
                return Convert.ToInt64(hexstr, 16).ToString();
            }
            else if (hexstr.Length > 4)
            {
                return Convert.ToInt32(hexstr, 16).ToString();
            }
            else if (hexstr.Length > 2)
            {
                return Convert.ToInt16(hexstr, 16).ToString();
            }
            else
            {
                return Convert.ToSByte(hexstr, 16).ToString();
            }
        }

        /// <summary>
        /// 判断是否是十六进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHexadecimal(string str)
        {
            const string PATTERN = @"[A-Fa-f0-9]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }

        /// <summary>
        /// //16转2方法
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static string HexString2BinString(string hexString)
        {
            try
            {
                string result = string.Empty;
                foreach (char c in hexString)
                {
                    int v = Convert.ToByte(c.ToString(), 16);
                    int v2 = int.Parse(Convert.ToString(v, 2));
                    // 去掉格式串中的空格，即可去掉每个4位二进制数之间的空格，
                    result += string.Format("{0:d4}", v2);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        // 普通字符串转16进制ASCII码
        public static string toASCII(String code)
        {
            byte[] bytes = Encoding.Default.GetBytes(code);
            string Hstr = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                Hstr += bytes[i].ToString("X");
            }
            return Hstr;
        }
        // 获取标准的的高低数组
        public static byte[] GetStandardBytes(byte[] data, string type, string sortorder)
        {
            byte[] result;
            int len = 0;
            switch (type)
            {
                case "bool":
                    len = 1;
                    break;
                case "byte":
                    len = 1;
                    break;
                case "short":
                    len = 2;
                    break;
                case "int":
                    len = 4;
                    break;
                case "long":
                    len = 8;
                    break;
                case "float":
                    len = 4;
                    break;
                case "double":
                    len = 8;
                    break;
                default:
                    break;
            }
            result = new byte[len];
            int dataLength = data.Length;
            if (sortorder == ESortOrder.高低.ToString())
            {
                for (int i = 0; i < len; i++)
                {
                    if (i < dataLength)
                    {
                        result[i] = data[i];
                    }
                    else
                    {
                        result[i] = 0x00;
                    }
                }
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    if (i < data.Length)
                    {
                        result[i] = data[data.Length - 1 - i];
                    }
                    else
                    {
                        result[i] = 0x00;
                    }
                }
            }
            return result;
        }

        public static string GetValueToBytes(byte[] datas, string datatype, string Symbol, string sortorder)
        {
            byte[] data = GetStandardBytes(datas, datatype, sortorder);
            double result = 0;
            switch (datatype)
            {
                case "byte":
                    result = data[0];
                    if (Symbol == ESymbol.有.ToString())
                    {
                        sbyte b = (sbyte)result;
                        result = b;
                    }
                    break;
                case "short":
                    result = BitConverter.ToUInt16(data, 0);
                    if (Symbol == ESymbol.有.ToString())
                    {
                        short b = (short)result;
                        result = b;
                    }
                    break;
                case "int":
                    result = BitConverter.ToUInt32(data, 0);
                    if (Symbol == ESymbol.有.ToString())
                    {
                        int b = (int)result;
                        result = b;
                    }
                    break;
                case "long":
                    result = BitConverter.ToUInt64(data, 0);
                    if (Symbol == ESymbol.有.ToString())
                    {
                        long b = (long)result;
                        result = b;
                    }
                    break;
                case "float":
                    result = BitConverter.ToSingle(data, 0);
                    break;
                case "double":
                    result = BitConverter.ToDouble(data, 0);
                    break;
                default:
                    break;
            }
            return result.ToString();
        }

        public static byte[] GetBytesToValue(string value, string datatype, string symbol)
        {
            byte[] result = null;
            switch (datatype)
            {
                case "bool":
                    if (value == "1")
                    {
                        value = "true";
                    }
                    else
                    {
                        value = "false";
                    }
                    bool o_bool;
                    if (bool.TryParse(value, out o_bool))
                    {
                        result = BitConverter.GetBytes(o_bool);
                    }
                    break;
                case "byte":
                    if (symbol == ESymbol.有.ToString())
                    {
                        sbyte o_sbyte;
                        if (sbyte.TryParse(value, out o_sbyte))
                        {
                            result = new byte[] { Convert.ToByte(o_sbyte) };
                        }
                    }
                    else
                    {
                        byte o_byte;
                        if (byte.TryParse(value, out o_byte))
                        {
                            result = new byte[] { o_byte };
                        }
                    }
                    break;
                case "short":

                    if (symbol == ESymbol.有.ToString())
                    {
                        short o_short;
                        if (short.TryParse(value, out o_short))
                        {
                            result = BitConverter.GetBytes(o_short);
                        }
                    }
                    else
                    {
                        ushort o_ushort;
                        if (ushort.TryParse(value, out o_ushort))
                        {
                            result = BitConverter.GetBytes(o_ushort);
                        }
                    }
                    break;
                case "int":
                    if (symbol == ESymbol.有.ToString())
                    {
                        int o_int;
                        if (int.TryParse(value, out o_int))
                        {
                            result = BitConverter.GetBytes(o_int);
                        }
                    }
                    else
                    {
                        uint o_uint;
                        if (uint.TryParse(value, out o_uint))
                        {
                            result = BitConverter.GetBytes(o_uint);
                        }
                    }
                    break;
                case "long":
                    if (symbol == ESymbol.有.ToString())
                    {
                        long o_long;
                        if (long.TryParse(value, out o_long))
                        {
                            result = BitConverter.GetBytes(o_long);
                        }
                    }
                    else
                    {
                        ulong o_ulong;
                        if (ulong.TryParse(value, out o_ulong))
                        {
                            result = BitConverter.GetBytes(o_ulong);
                        }
                    }
                    break;
                case "float":
                    float o_float;
                    if (float.TryParse(value, out o_float))
                    {
                        result = BitConverter.GetBytes(o_float);
                    }
                    break;
                case "double":
                    double o_double;
                    if (double.TryParse(value, out o_double))
                    {
                        result = BitConverter.GetBytes(o_double);
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

    }
}

using UDP.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UDP.Interface;
using UDP.Cache;

namespace UDP.Analysis
{
    public class ICDAnalysis
    {

        //把要发送的数据按ICD解析成源码
        public static byte[] analysisSendValue_BUS(string sendValue, Signal signal)
        {
            string datacode = signal.DataCode;
            string datatype = signal.DataType;
            string scope = signal.Scope;
            string plottingScale = signal.PlottingScale;
            string sortorder = signal.SortOrder;
            string symbol = signal.Symbol;
            int lenght = int.Parse(signal.Length);
            string value = "";
            if (EmptyHelper.isEmpty(sendValue))
            {
                return null;
            }
            try
            {
                if (datacode == "十六进制")
                {
                    value = Convert.ToUInt64(sendValue, 16).ToString();
                }
                else if (datacode == "二进制")
                {
                    value = Convert.ToUInt64(sendValue, 2).ToString();
                }
                else
                {
                    value = sendValue;
                }

                Decimal dvalue = Decimal.Parse(value);
                //if (!EmptyHelper.isEmpty(scope))
                //{
                //    string[] fws = scope.Split('～');
                //    Decimal min = Decimal.Parse(fws[0]);
                //    Decimal max = Decimal.Parse(fws[1]);
                //    if (Decimal.Compare(dvalue, max) > 0 || Decimal.Compare(dvalue, min) < 0) // 如果不在范围内，跳过不做修改
                //    {
                //        return null;
                //    }
                //}
                if (!EmptyHelper.isEmpty(plottingScale))
                {
                    string ps = signal.PlottingScale;
                    string[] pss = ps.Split(';');
                    Array.Reverse(pss); // 发送数据解析要反过来
                    foreach (string s in pss)
                    {
                        if (s.Length > 1)
                        {
                            Decimal _out = 0;
                            if (Decimal.TryParse(s.Substring(1, s.Length - 1), out _out))
                            {
                                Decimal blc = _out;
                                if (s.StartsWith("*"))
                                {
                                    dvalue = Decimal.Divide(dvalue, blc);
                                }
                                else if (s.StartsWith("/"))
                                {
                                    dvalue = Decimal.Multiply(dvalue, blc);
                                }
                                else if (s.StartsWith("+"))
                                {
                                    dvalue = Decimal.Subtract(dvalue, blc);
                                }
                                else if (s.StartsWith("-"))
                                {
                                    dvalue = Decimal.Add(dvalue, blc);
                                }
                            }
                        }
                    }
                }
                value = dvalue.ToString();
                byte[] data = ConvertHelper.GetBytesToValue(value, datatype, symbol);
                if (data == null)
                {
                    return null;
                }
                byte[] result = new byte[lenght];
                if (sortorder == ESortOrder.高低.ToString())
                {
                    for (int i = 0; i < lenght; i++)
                    {
                        if (i < data.Length)
                        {
                            result[lenght - 1 - i] = data[i];
                        }
                        else
                        {
                            result[lenght - 1 - i] = 0x00;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lenght; i++)
                    {
                        if (i < data.Length)
                        {
                            result[i] = data[i];
                        }
                        else
                        {
                            result[i] = 0x00;
                        }
                    }
                }
                return result;
            }
            catch
            {
                return null;
            }

        }

        //把要接收的数据按ICD解析成物理量
        public static DataModel analysisRecvValue_BUS(byte[] datas, Signal signal)
        {
            string datacode = signal.DataCode;
            string datatype = signal.DataType;
            string scope = signal.Scope;
            string plottingScale = signal.PlottingScale;
            string sortorder = signal.SortOrder;
            int lenght = int.Parse(signal.Length);

            string sourceCode = "";
            string analysisValue = "";

            byte[] data = new byte[lenght];

            for (int i = 0; i < lenght; i++)
            {
                data[i] = datas[signal.Offset + i];
            }

            if (sortorder == ESortOrder.低高.ToString())
            {
                Array.Reverse(data);
            }

            for (int i = 0; i < data.Length; i++)
            {
                string str16 = Convert.ToString(data[i], 16).ToUpper();
                while (str16.Length < 2)
                {
                    str16 = "0" + str16;
                }
                sourceCode += str16; // 转成16进制字符串     源码
            }

            if (datacode == EDataCode.十六进制.ToString())   // 十六进制
            {
                for (int i = 0; i < data.Length; i++)
                {
                    string str16 = Convert.ToString(data[i], 16).ToUpper();
                    while (str16.Length < 2)
                    {
                        str16 = "0" + str16;
                    }
                    analysisValue += str16; // 转成16进制字符串
                }
            }
            else if (datacode == EDataCode.十进制.ToString())   // 十进制
            {
                analysisValue = ConvertHelper.GetValueToBytes(data, signal.DataType, signal.Symbol, signal.SortOrder);
                if (signal.PlottingScale != "")
                {
                    string ps = signal.PlottingScale;
                    string[] pss = ps.Split(';');
                    Decimal dvalue = Decimal.Parse(analysisValue);
                    foreach (string s in pss)
                    {
                        Decimal blc = 0;
                        if (s.StartsWith("*"))
                        {
                            blc = Decimal.Parse(s.Substring(1, s.Length - 1));
                            dvalue = Decimal.Multiply(dvalue, blc);
                        }
                        else if (s.StartsWith("/"))
                        {
                            blc = Decimal.Parse(s.Substring(1, s.Length - 1));
                            dvalue = Decimal.Divide(dvalue, blc);
                        }
                        else if (s.StartsWith("+"))
                        {
                            blc = Decimal.Parse(s.Substring(1, s.Length - 1));
                            dvalue = Decimal.Add(dvalue, blc);
                        }
                        else if (s.StartsWith("-"))
                        {
                            blc = Decimal.Parse(s.Substring(1, s.Length - 1));
                            dvalue = Decimal.Subtract(dvalue, blc);
                        }
                    }
                    analysisValue = dvalue.ToString();
                }
            }
            else
            {
                analysisValue = sourceCode;
            }
            DataModel dm = new DataModel();
            dm.SourceCode = sourceCode;
            dm.ParseValue = analysisValue;
            return dm;
        }


 
    }
}

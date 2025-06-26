using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using UDP.Interface;
using UDP.Interface.impl;

namespace UDP.Tools
{
    public class FileHelper
    {
        public static List<Channel_UDP> ReadUDPChannel(string filename)
        {
            if (EmptyHelper.isEmpty(filename))
            {
                MessageBox.Show("文件名称不能为空");
                return null;
            }
            List<List<string>> list = ReadCSVFile(filename);
            if (EmptyHelper.isEmpty(list))
            {
                return null;
            }
            List<Channel_UDP> Channels = new List<Channel_UDP>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    Channel_UDP channel = new Channel_UDP();
                    List<string> data = list[i];
                    channel.Name = data[1];
                    channel.ChanId = data[2];
                    channel.LocalIP = IPAddress.Parse(data[3]); // Fix: Convert string to IPAddress
                    channel.LocalPort = int.Parse(data[4]);
                    channel.TargetIp = IPAddress.Parse(data[5]); // Fix: Convert string to IPAddress
                    channel.TargetPort = int.Parse(data[6]);
                    Channels.Add(channel);
                }
            }
            return Channels;
        }

        public static List<Config_BUS> ReadConfig_BUS(string filename)
        {
            if (EmptyHelper.isEmpty(filename))
            {
                MessageBox.Show("文件名称不能为空");
                return null;
            }
            List<List<string>> list = ReadCSVFile(filename);
            if (EmptyHelper.isEmpty(list))
            {
                return null;
            }

            List<Config_BUS> configs = new List<Config_BUS>();
            Config_BUS config = null;
            List<string> ICDNos = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    List<string> data = list[i];
                    if (!EmptyHelper.isEmpty(data[0]))
                    {
                        if (config != null)
                        {
                            config.ICDNos = ICDNos;
                            configs.Add(config);
                        }
                        config = new Config_BUS();
                        ICDNos = new List<string>();
                        config.ChannelId = data[0];
                    }
                    ICDNos.Add(data[1]);
                    if (i == list.Count - 1)
                    {
                        config.ICDNos = ICDNos;
                        configs.Add(config);
                    }
                }
            }
            return configs;
        }

        public static List<List<string>> ReadCSVFile(string file)
        {
            List<List<string>> list = new List<List<string>>();
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                //记录每次读取的一行记录
                string strLine = null;
                //记录每行记录中的各字段内容
                string[] arrayLine = null;

                //逐行读取CSV文件
                while ((strLine = sr.ReadLine()) != null)
                {
                    strLine = strLine.Trim();
                    arrayLine = strLine.Split(',');
                    List<string> listLine = new List<string>(arrayLine);
                    list.Add(listLine);
                }
                sr.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("读取文件 " + file + " 失败");
                return null;
            }

            return list;
        }

        public static ICD_BUS ReadICDFile_BUS(string file)
        {
            if (EmptyHelper.isEmpty(file))
            {
                MessageBox.Show("文件名称不能为空");
                return null;
            }

            List<List<string>> list = ReadCSVFile(file);

            if (EmptyHelper.isEmpty(list))
            {
                return null;
            }

            ICD_BUS icd = new ICD_BUS();
            Signal currentSignal = null; // 记录当前信号
            StateWord stateword = null; // 记录状态字
            List<Signal> signals = new List<Signal>();  // 存储信号信息
            List<StateWord> stateWords = null; // 存储状态字信息

            for (int i = 0; i < list.Count; i++)
            {
                List<string> data = list[i];
                while (data.Count < 15)
                {
                    data.Add("");
                }

                if (i == 0)
                {
                    icd.Name = data[1]; // ICD名称
                    icd.Code = data[3]; // ICD编号
                    icd.Length = data[5];// 长度
                    icd.Cycle = data[7];// 周期
                    icd.ICDType = data[9];   // ICD类型
                }
                else if (i == 1)
                {
                    icd.Head = data[1];// 同步头
                    icd.FrameFlag = data[3]; // 帧标识
                    icd.CheckSumMode = data[7]; // 校验和方式
                    icd.SendType = data[5];  // 发送类型
                }
                else if (i == 2) // 同步头
                {
                    icd.FrameNumberLocal = data[1];  // 帧计数位置
                    icd.FrameFlagLocal = data[3];   // 帧标识位置
                    icd.CheckSumLocal = data[5];    // 校验和位置
                    icd.SendNumber = data[7];   // 发送次数
                }
                else if (i > 3)
                {
                    string name = data[1];
                    string datatype = data[2];
                    string length = data[3];
                    string bitlen = data[4];
                    string defaultvalue = data[5];
                    string sortorder = data[6];
                    string symbol = data[7];
                    string scope = data[8];
                    string unit = data[9];
                    string plottingScale = data[10];
                    string statements = data[11];
                    string datacode = data[12];

                    if (datatype == "bit")
                    {
                        stateword = new StateWord();
                        stateword.Name = name;
                        stateword.IcdCode = currentSignal.IcdCode;
                        stateword.SignalSN = currentSignal.SN;
                        stateword.DataType = datatype;
                        stateword.Length = bitlen;
                        stateword.SortOrder = sortorder;
                        stateword.Statements = statements;
                        stateword.DataCode = datacode;
                        stateWords.Add(stateword);
                        if (i == list.Count - 1)
                        {
                            currentSignal.StateWords = stateWords;
                        }
                    }
                    else
                    {
                        if (!EmptyHelper.isEmpty(stateWords))
                        {
                            currentSignal.StateWords = stateWords;
                        }

                        // 将状态字初始化
                        stateWords = new List<StateWord>();

                        currentSignal = new Signal();
                        currentSignal.IcdCode = icd.Code;
                        // 序号
                        // 信号名称
                        currentSignal.Name = name;
                        // 数据类型
                        currentSignal.DataType = datatype;
                        // 字节长度
                        currentSignal.Length = length;
                        // 默认值
                        currentSignal.DefaultValue = defaultvalue;
                        // 顺序
                        currentSignal.SortOrder = sortorder;
                        // 符号
                        currentSignal.Symbol = symbol;
                        // 范围
                        currentSignal.Scope = scope;
                        // 单位
                        currentSignal.Unit = unit;
                        // 比例尺
                        currentSignal.PlottingScale = plottingScale;
                        // 状态字说明
                        currentSignal.Statements = statements;
                        // 数据类型
                        currentSignal.DataCode = datacode;
                        signals.Add(currentSignal);
                    }
                }
            }
            icd.Signals = signals;
            return icd;
        }

        public static Dictionary<string, ICD_BUS> ReadICD_BUS(string dirpath)
        {
            Dictionary<string, ICD_BUS> ICDs = new Dictionary<string, ICD_BUS>();
            string[] files = Directory.GetFiles(dirpath);
            foreach (string file in files)
            {
                ICD_BUS ICD = (ICD_BUS)ReadICDFile_BUS(file);
            }
            return ICDs;
        }
    }
}
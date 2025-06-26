using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDP.Interface.impl;

namespace UDP.Interface
{
    public class ICD_BUS : IICD    // ICD
    {
        public string ICDType { set; get; }  // 类型
        public string Head { set; get; }  // 同步头
        public string SendType { set; get; }  // 发送类型
        public string SendNumber { set; get; }  // 发送次数
        public string FrameFlag { set; get; }  // 帧标识
        public string FrameFlagLocal { set; get; }  // 帧标识位置
        public string CheckSumMode { set; get; }  // 校验和方式
        public string CheckSumLocal { set; get; }  // 校验和位置
        public string FrameNumberLocal { set; get; }  // 帧计数位置
        public List<Signal> Signals { set; get; }  // 信号信息
        public List<string> RecvChannels { set; get; }
        public List<string> SendChannels { set; get; }
    }

    public class Signal
    {
        public int Id { set; get; }
        public string IcdCode { set; get; } // ICD编码  
        public int SN { set; get; }   // 序号 
        public string Name { set; get; } // 信号名称
        public string DataType { set; get; }  // 数据类型
        public string Length { set; get; }  // 字节长度
        public string DefaultValue { set; get; } // 默认值  
        public string SortOrder { set; get; }   // 字节顺序  0: 高低  1：低高
        public string Symbol { set; get; }   // 符号 
        public string Scope { set; get; }  // 信号范围
        public string Unit { set; get; }  // 单位
        public string PlottingScale { set; get; }   // 比例尺
        public string Statements { set; get; }  // 状态说明
        public string DataCode { set; get; }   // 数据编码  0:十进制；1:二进制；2:十六进制
        public int Offset { set; get; }   // 偏离量
        public List<StateWord> StateWords { set; get; }
    }

    public class StateWord
    {
        public int Id { set; get; }
        public int SN { set; get; } // 序号
        public string Name { set; get; } // 状态字名称
        public string IcdCode { set; get; }   // 对应的ICD编号
        public int SignalSN { set; get; }   // 对应的父序号
        public string DataType { set; get; } // 
        public string Length { set; get; }  // 长度
        public string SortOrder { set; get; }   // 字节顺序  0: 高低  1：低高
        public string Statements { set; get; }  // 状态说明
        public string DataCode { set; get; }   // 数据编码  0:十进制；1:二进制；2:十六进制
        public int Offset { set; get; }  // 偏离量
    }
}

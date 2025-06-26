using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UDP.Interface;
using UDP.Interface.impl;
using UDP.Model;
using UDP.Tools;

namespace UDP
{
    internal class LoadConfig
    {
        public static Dictionary<string, IChannel> Channels = new Dictionary<string, IChannel>();
        public static List<Channel_UDP> Chs_UDP = new List<Channel_UDP>();

        // 统一加载配置的方法
        public static void LoadAllConfigs()
        {
            ClearAllConfigs();
            LoadUDP();
            ConfigBUS();
        }

        public static void LoadUDP()
        {
            string FilePath_Config_UDP = @"C:\Users\Administrator\Desktop\UDP\bin\Debug\ConfigFile\BUS\Device\UDP\UDPChannelConfig.csv";
            Chs_UDP = FileHelper.ReadUDPChannel(FilePath_Config_UDP);
            if (!EmptyHelper.isEmpty(Chs_UDP))
            {
                foreach (Channel_UDP ch in Chs_UDP)
                {
                    ch.DeviceType = EDeviceType.UDP;
                    Channels.Add(ch.ChanId, ch);
                }
            }
        }

        // 加载UDP通道配置
        public static void LoadUDP(int selectedChannel)
        {
            string FilePath_Config_UDP = @"C:\Users\Administrator\Desktop\UDP\bin\Debug\ConfigFile\BUS\Device\UDP\UDPChannelConfig.csv";
            Chs_UDP = FileHelper.ReadUDPChannel(FilePath_Config_UDP);
            if (!EmptyHelper.isEmpty(Chs_UDP))
            {
                foreach (Channel_UDP ch in Chs_UDP)
                {
                    ch.DeviceType = EDeviceType.UDP;
                    Channels.Add(ch.ChanId, ch);
                }
            }
        }

        // 清理所有配置
        private static void ClearAllConfigs()
        {
            Chs_UDP.Clear();
            Channels.Clear();
        }

        // 初始化并测试所有通道
        public static void InitAndTestChannels()
        {
            try
            {
                // 开启所有通道
                TestAllChannels();
            }
            catch (Exception ex)
            {
                // 出错时关闭所有已打开的通道
                Close();
                throw new Exception("通道初始化和测试失败: " + ex.Message, ex);
            }
        }

        // 打开所有通道
        public static void Init()
        {
            // 初始化前先关闭所有通道
            Close();

            // BstCard.CardOpen();
            if (!EmptyHelper.isEmpty(Chs_UDP))
            {
                foreach (Channel_UDP chan in Chs_UDP)
                {
                    chan.Init();
                }
            }
        }

        // 测试所有通道
        private static void TestAllChannels()
        {
            foreach (var chan in Chs_UDP)
            {
                try
                {
                    if (!chan.IsOpen)
                    {
                        throw new Exception("通道未成功打开");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"测试通道 {chan.Name} 失败: {ex.Message}", ex);
                }
            }
        }

        // 配置BUS
        public static void ConfigBUS()
        {
            // 确保通道已加载
            if (Chs_UDP.Count == 0)
            {
                throw new InvalidOperationException("请先加载通道配置");
            }

            // 创建接收ICD配置
            var recvICD = new ICD_BUS
            {
                Name = "默认UDP接收通道",
                Code = "UDP_RECV_01",
                RecvChannels = new List<string> { "UDP_CH_01", "TEST_UDP_CH" }
            };

            // 创建发送ICD配置
            var sendICD = new ICD_BUS
            {
                Name = "默认UDP发送通道",
                Code = "UDP_SEND_01",
                SendChannels = new List<string> { "UDP_CH_01", "TEST_UDP_CH" }
            };
        }

        // 关闭所有通道
        public static void Close()
        {
            foreach (var chan in Chs_UDP)
            {
                try
                {
                    chan.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"关闭通道 {chan.Name} 时出错: {ex.Message}");
                    // 继续关闭其他通道
                }
            }
        }
    }
}
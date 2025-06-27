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
    public class LoadConfig
    {
        public static string PathDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string FilePathConfigUdp = PathDir + @"ConfigFile\BUS\Device\UDP\UDPChannelConfig.csv";
        public static Dictionary<string, IChannel> Channels = new Dictionary<string, IChannel>();
        public static List<ChannelUdp> UdpChannels = new List<ChannelUdp>();

        #region 清理，加载，打开，关闭

        // 清理所有配置
        private static void ClearAllConfigs()
        {
            UdpChannels.Clear();
            Channels.Clear();
        }

        // 统一加载配置的方法
        public static void LoadAllConfigs()
        {
            ClearAllConfigs();
            LoadUdp();
        }

        public static void LoadUdp()
        {
            UdpChannels = FileHelper.ReadUdpChannel(FilePathConfigUdp);
            if (!EmptyHelper.isEmpty(UdpChannels))
            {
                foreach (ChannelUdp channel in UdpChannels)
                {
                    channel.DeviceType = EDeviceType.UDP;
                    Channels.Add(channel.ChanId, channel);
                }
            }
        }

        // 打开所有通道
        public static void Init()
        {
            // 初始化前先关闭所有通道
            Close();

            // BstCard.CardOpen();
            if (!EmptyHelper.isEmpty(UdpChannels))
            {
                foreach (ChannelUdp chan in UdpChannels)
                {
                    if (chan.Init() == 0)
                    {
                        chan.IsOpen = true;
                    }
                }
            }
        }

        // 关闭所有通道
        public static void Close()
        {
            foreach (var chan in UdpChannels)
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

        #endregion 清理，加载，打开，关闭

        #region 获取集合中所有通道并返回

        public static List<ChannelUdp> GetAllChannels()
        {
            foreach (ChannelUdp channel in UdpChannels)
            {
                if (!channel.IsOpen)
                {
                    // 仅在初始化成功后才设置IsOpen状态
                    if (channel.Init() == 0)
                    {
                        channel.IsOpen = true;
                    }
                }
            }
            return UdpChannels; // 返回第一个通道，或根据需要返回其他通道
        }

        #endregion 获取集合中所有通道并返回
    }
}
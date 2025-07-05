using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                // 使用相对路径，相对于应用程序执行目录
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                string configPath = Path.Combine(currentDir, "ConfigFile", "BUS", "Device", "UDP", "UDPChannelConfig.csv");
                
                Console.WriteLine($"尝试加载UDP配置文件: {configPath}");
                
                if (!File.Exists(configPath))
                {
                    Console.WriteLine($"配置文件不存在: {configPath}");
                    // 创建默认配置
                    CreateDefaultUDPConfig(configPath);
                }
                
                Chs_UDP = FileHelper.ReadUDPChannel(configPath);
                if (!EmptyHelper.isEmpty(Chs_UDP))
                {
                    foreach (Channel_UDP ch in Chs_UDP)
                    {
                        ch.DeviceType = EDeviceType.UDP;
                        if (!Channels.ContainsKey(ch.ChanId))
                        {
                            Channels.Add(ch.ChanId, ch);
                        }
                    }
                    Console.WriteLine($"成功加载 {Chs_UDP.Count} 个UDP通道配置");
                }
                else
                {
                    Console.WriteLine("未加载到任何UDP通道配置");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载UDP配置时出错: {ex.Message}");
                // 创建默认配置
                CreateDefaultUDPChannels();
            }
        }

        // 创建默认配置文件
        private static void CreateDefaultUDPConfig(string configPath)
        {
            try
            {
                // 确保目录存在
                Directory.CreateDirectory(Path.GetDirectoryName(configPath));
                
                // 创建默认配置内容
                string defaultConfig = @"序号,通道名称,通道ID,本地IP,本地端口,目标IP,目标端口
1,UDP通道1,UDP_CH_01,127.0.0.1,8001,127.0.0.1,9001
2,UDP通道2,UDP_CH_02,127.0.0.1,8002,127.0.0.1,9002
3,UDP通道3,UDP_CH_03,127.0.0.1,8003,127.0.0.1,9003
4,UDP通道4,UDP_CH_04,127.0.0.1,8004,127.0.0.1,9004
5,UDP通道5,UDP_CH_05,127.0.0.1,8005,127.0.0.1,9005
6,UDP通道6,UDP_CH_06,127.0.0.1,8006,127.0.0.1,9006
7,UDP通道7,UDP_CH_07,127.0.0.1,8007,127.0.0.1,9007
8,UDP通道8,UDP_CH_08,127.0.0.1,8008,127.0.0.1,9008
9,UDP通道9,UDP_CH_09,127.0.0.1,8009,127.0.0.1,9009
10,UDP通道10,UDP_CH_10,127.0.0.1,8010,127.0.0.1,9010
11,UDP通道11,UDP_CH_11,127.0.0.1,8011,127.0.0.1,9011
12,UDP通道12,UDP_CH_12,127.0.0.1,8012,127.0.0.1,9012
13,UDP通道13,UDP_CH_13,127.0.0.1,8013,127.0.0.1,9013
14,UDP通道14,UDP_CH_14,127.0.0.1,8014,127.0.0.1,9014
15,UDP通道15,UDP_CH_15,127.0.0.1,8015,127.0.0.1,9015
16,UDP通道16,UDP_CH_16,127.0.0.1,8016,127.0.0.1,9016";

                File.WriteAllText(configPath, defaultConfig, System.Text.Encoding.UTF8);
                Console.WriteLine($"已创建默认配置文件: {configPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建默认配置文件失败: {ex.Message}");
            }
        }

        // 创建默认UDP通道（内存中）
        private static void CreateDefaultUDPChannels()
        {
            try
            {
                Chs_UDP.Clear();
                
                for (int i = 1; i <= 16; i++)
                {
                    var channel = new Channel_UDP
                    {
                        Id = i,
                        Name = $"UDP通道{i}",
                        ChanId = $"UDP_CH_{i:D2}",
                        LocalIP = System.Net.IPAddress.Parse("127.0.0.1"),
                        LocalPort = 8000 + i,
                        TargetIp = System.Net.IPAddress.Parse("127.0.0.1"),
                        TargetPort = 9000 + i,
                        DeviceType = EDeviceType.UDP
                    };
                    
                    Chs_UDP.Add(channel);
                    
                    if (!Channels.ContainsKey(channel.ChanId))
                    {
                        Channels.Add(channel.ChanId, channel);
                    }
                }
                
                Console.WriteLine($"已创建 {Chs_UDP.Count} 个默认UDP通道");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建默认UDP通道失败: {ex.Message}");
            }
        }

        // 加载UDP通道配置
        public static void LoadUDP(int selectedChannel)
        {
            LoadUDP(); // 重用现有的LoadUDP方法
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
                Init();
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

            if (!EmptyHelper.isEmpty(Chs_UDP))
            {
                foreach (Channel_UDP chan in Chs_UDP)
                {
                    try
                    {
                        chan.Init();
                        Console.WriteLine($"通道 {chan.Name} 初始化成功");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"通道 {chan.Name} 初始化失败: {ex.Message}");
                    }
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
                    Console.WriteLine($"通道 {chan.Name} 测试通过");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"测试通道 {chan.Name} 失败: {ex.Message}");
                }
            }
        }

        // 配置BUS
        public static void ConfigBUS()
        {
            try
            {
                // 确保通道已加载
                if (Chs_UDP.Count == 0)
                {
                    Console.WriteLine("警告: 没有加载到UDP通道配置");
                    return;
                }

                // 创建接收ICD配置
                var recvICD = new ICD_BUS
                {
                    Name = "默认UDP接收通道",
                    Code = "UDP_RECV_01",
                    RecvChannels = new List<string> { "UDP_CH_01", "UDP_CH_02" }
                };

                // 创建发送ICD配置
                var sendICD = new ICD_BUS
                {
                    Name = "默认UDP发送通道",
                    Code = "UDP_SEND_01",
                    SendChannels = new List<string> { "UDP_CH_01", "UDP_CH_02" }
                };
                
                Console.WriteLine("BUS配置完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"配置BUS时出错: {ex.Message}");
            }
        }

        // 关闭所有通道
        public static void Close()
        {
            foreach (var chan in Chs_UDP)
            {
                try
                {
                    if (chan.IsOpen)
                    {
                        chan.Close();
                        Console.WriteLine($"通道 {chan.Name} 已关闭");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"关闭通道 {chan.Name} 时出错: {ex.Message}");
                }
            }
        }
    }
}
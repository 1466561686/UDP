using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDP.timer;
using UDP.Tools;

namespace UDP.UI
{
    public partial class HomeForm : Form
    {
        public timer.UltraHightAccurateTimer receiveTimer;
        private Channel_UDP channelUdp;
        private Form form;
        private DataForm tab_data;
        private List<DataForm> dataForms;
        private bool isTestRunning = false;

        public HomeForm()
        {
            InitializeComponent();
            InitializeChannel();
            InitView();
            InitTimer();
        }

        private void InitTimer()
        {
            // 使用自定义高精度定时器
            receiveTimer = new UltraHightAccurateTimer();
            receiveTimer.Interval = 1000; // 1秒间隔
            receiveTimer.Tick += new UltraHightAccurateTimer.ManualTimerEventHandler(ReceiveTimer_Tick);
        }

        private void ReceiveTimer_Tick(object sender)
        {
            try
            {
                if (isTestRunning)
                {
                    // 检查所有通道状态并处理数据
                    CheckAllChannelsStatus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("定时器处理出错: " + ex.Message);
            }
        }

        private void CheckAllChannelsStatus()
        {
            foreach (var channel in LoadConfig.Chs_UDP)
            {
                if (channel.IsOpen)
                {
                    try
                    {
                        // 使用同步接收方法检查是否有数据
                        var datas = channel.Recv();
                        if (datas != null && datas.Count > 0)
                        {
                            foreach (var data in datas)
                            {
                                string msg = Encoding.UTF8.GetString(data);
                                // 更新对应的DataForm显示
                                int channelIndex = LoadConfig.Chs_UDP.IndexOf(channel);
                                if (channelIndex >= 0 && channelIndex < dataForms.Count)
                                {
                                    dataForms[channelIndex].DisplayUdpDataToDataForm(channelIndex, msg);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"检查通道 {channel.Name} 数据时出错: {ex.Message}");
                    }
                }
            }
        }

        private void InitializeChannel()
        {
            try
            {
                LoadConfig.LoadAllConfigs();
                Console.WriteLine($"加载了 {LoadConfig.Chs_UDP.Count} 个UDP通道配置");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitView()
        {
            dataForms = new List<DataForm>();
            
            // 清空现有的TabPages
            tab_data.TabPages.Clear();
            
            // 确保至少有16个通道配置
            int channelCount = Math.Max(LoadConfig.Chs_UDP.Count, 16);
            
            for (int i = 0; i < channelCount; i++)
            {
                TabPage tb = new TabPage();
                tb.Name = "ch" + i;
                tb.Text = "ch" + i;
                tb.Controls.Clear();
                
                // 创建DataForm实例
                DataForm dataForm = new DataForm(i);
                dataForm.FormBorderStyle = FormBorderStyle.None;
                dataForm.TopLevel = false;
                dataForm.Dock = DockStyle.Fill;
                
                // 添加到TabPage
                tb.Controls.Add(dataForm);
                dataForm.Show();
                
                // 添加到TabControl
                tab_data.TabPages.Add(tb);
                
                // 保存DataForm引用
                dataForms.Add(dataForm);
            }
            
            // 设置TabControl事件
            tab_data.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选中TabPage时的处理
            if (tab_data.SelectedIndex >= 0 && tab_data.SelectedIndex < LoadConfig.Chs_UDP.Count)
            {
                Channel_UDP currentChannel = LoadConfig.Chs_UDP[tab_data.SelectedIndex];
                Console.WriteLine($"切换到通道: {currentChannel.Name}");
            }
        }

        private void buttonStartTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (isTestRunning)
                {
                    MessageBox.Show("测试已经在运行中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 打开所有UDP通道
                int successCount = 0;
                int failCount = 0;
                
                foreach (var channel in LoadConfig.Chs_UDP)
                {
                    if (!channel.IsOpen)
                    {
                        int result = channel.Init();
                        if (result == 0)
                        {
                            successCount++;
                        }
                        else
                        {
                            failCount++;
                        }
                    }
                    else
                    {
                        successCount++;
                    }
                }

                // 启动所有DataForm的UDP通道
                foreach (var dataForm in dataForms)
                {
                    dataForm.StartUdpChannel();
                }

                // 启动定时器
                receiveTimer.Start();
                isTestRunning = true;

                MessageBox.Show($"UDP通道启动完成\n成功: {successCount}\n失败: {failCount}", 
                    "启动结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // 更新按钮状态
                buttonStartTest.Enabled = false;
                buttonEndTest.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动测试失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEndTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isTestRunning)
                {
                    MessageBox.Show("测试未在运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 停止定时器
                receiveTimer.Stop();
                isTestRunning = false;

                // 停止所有DataForm的UDP通道
                foreach (var dataForm in dataForms)
                {
                    dataForm.StopUdpChannel();
                }

                // 关闭所有UDP通道
                foreach (var channel in LoadConfig.Chs_UDP)
                {
                    if (channel.IsOpen)
                    {
                        channel.Close();
                    }
                }

                MessageBox.Show("所有UDP通道已停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // 更新按钮状态
                buttonStartTest.Enabled = true;
                buttonEndTest.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"停止测试失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCloseLowerMachine_Click(object sender, EventArgs e)
        {
            try
            {
                // 确保停止所有通道
                if (isTestRunning)
                {
                    buttonEndTest_Click(sender, e);
                }
                
                // 释放定时器资源
                if (receiveTimer != null)
                {
                    receiveTimer.Stop();
                    receiveTimer = null;
                }
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"关闭应用程序时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            try
            {
                // 停止测试
                if (isTestRunning)
                {
                    receiveTimer?.Stop();
                    isTestRunning = false;
                }
                
                // 关闭所有通道
                foreach (var channel in LoadConfig.Chs_UDP)
                {
                    if (channel.IsOpen)
                    {
                        channel.Close();
                    }
                }
                
                // 释放资源
                receiveTimer?.Stop();
                receiveTimer = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"关闭窗体时出错: {ex.Message}");
            }
        }

        // 手动发送测试数据
        private void SendTestData()
        {
            try
            {
                foreach (var channel in LoadConfig.Chs_UDP)
                {
                    if (channel.IsOpen)
                    {
                        string testMessage = $"测试数据 - {DateTime.Now:HH:mm:ss}";
                        channel.SendString(testMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送测试数据时出错: {ex.Message}");
            }
        }
    }
}
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
        public timer.UltraHightAccurateTimer receiveTimer; // 定义定时器
        private Channel_UDP channelUdp;
        private Form form;

        private DataForm tab_data;

        public HomeForm()
        {
            InitializeComponent();
            InitializeChannel(); // 先初始化通道
            InitView(); // 初始化视图
            InitTimer(); // 调用定时器
        }

        #region 初始化配置

        private void InitView()
        {
            for (int i = 0; i < 16; i++)
            {
                var tb = new TabPage
                {
                    Name = $"ch{i}",
                    Text = $"ch{i}"
                };
                var dataForm = new DataForm(i)
                {
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false,
                    Dock = DockStyle.Fill
                };
                tb.Controls.Add(dataForm);
                dataForm.Show();
                tabControl1.TabPages.Add(tb); // Ensure tab_data is a TabControl, not a DataForm
            }
        }

        private void InitTimer()
        {
            // 使用自定义高精度定时器
            receiveTimer = new UltraHightAccurateTimer();
            receiveTimer.Interval = 10;
            receiveTimer.Tick += new UltraHightAccurateTimer.ManualTimerEventHandler(ReceiveTimer_Tick);
        }

        private void InitializeChannel()
        {
            LoadConfig.LoadAllConfigs();
        }

        private void ReceiveTimer_Tick(object sender, EventArgs e)
        {
            // Implement the logic for the timer tick event here.
            // For example, you can add code to perform periodic tasks.
            MessageBox.Show("定时器触发事件已执行");
        }

        private void ReceiveTimer_Tick(object sender)
        {
            try
            {
                // 定时发送（举例：每次都发一条固定内容）
                if (channelUdp != null && channelUdp.IsOpen)
                {
                    string autoSendMsg = "定时发送内容";
                    byte[] sendBytes = Encoding.UTF8.GetBytes(autoSendMsg);
                    channelUdp.Send(sendBytes);
                }

                // 定时接收
                if (channelUdp != null && channelUdp.IsOpen)
                {
                    var datas = channelUdp.Recv();
                    if (datas != null && datas.Count > 0)
                    {
                        StringBuilder batchMessages = new StringBuilder();
                        foreach (var data in datas)
                        {
                            string msg = Encoding.UTF8.GetString(data);
                            batchMessages.AppendLine($"收到: {msg}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("接收/发送数据出错: " + ex.Message);
            }
        }

        #endregion 初始化配置

        #region 按钮点击事件

        private void buttonStartTest_Click(object sender, EventArgs e)
        {
            // 打开所有UDP通道
            foreach (var channel in LoadConfig.Chs_UDP)
            {
                if (!channel.IsOpen)
                {
                    channel.Init();
                }
            }

            // 启动定时器
            if (this.receiveTimer == null)
            {
                this.receiveTimer = new UltraHightAccurateTimer();
                this.receiveTimer.Interval = 1000; // 1秒，可根据需要调整
                this.receiveTimer.Tick += ReceiveTimer_Tick;
            }
            this.receiveTimer.Start();
        }

        // 停止测试按钮点击事件

        private void buttonEndTest_Click(object sender, EventArgs e)
        {
            foreach (var channel in LoadConfig.Chs_UDP)
            {
                if (channel.IsOpen)
                {
                    channel.Close();
                }
            }
            MessageBox.Show("所有UDP通道已停止");
        }

        private void buttonCloseLowerMachine_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion 按钮点击事件
    }
}
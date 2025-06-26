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
<<<<<<< HEAD
=======
        private DataForm tab_data;
>>>>>>> 2632f6d1847636d5f8baa359cde7a7991b151c47

        public HomeForm()
        {
            InitializeComponent();
            InitializeChannel(); // 先初始化通道
            InitView(); // 初始化视图
            InitTimer(); // 调用定时器
        }

<<<<<<< HEAD
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

=======
>>>>>>> 2632f6d1847636d5f8baa359cde7a7991b151c47
        private void InitTimer()
        {
            // 使用自定义高精度定时器
            receiveTimer = new UltraHightAccurateTimer();
            receiveTimer.Interval = 10;
            receiveTimer.Tick += new UltraHightAccurateTimer.ManualTimerEventHandler(ReceiveTimer_Tick);
        }

<<<<<<< HEAD
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

=======
>>>>>>> 2632f6d1847636d5f8baa359cde7a7991b151c47
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

<<<<<<< HEAD
        #endregion 初始化配置

        #region 按钮点击事件

        //开始测试
=======
        private void InitializeChannel()
        {
            LoadConfig.LoadAllConfigs();
        }

        private void InitView()
        {
            for (int i = 0; i < 16; i++)
            {
                TabPage tb = new TabPage();
                tb.Name = "ch" + i;
                tb.Text = "ch" + i;
                tb.Controls.Clear();
                DataForm dataForm = new DataForm(i); //
                dataForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                dataForm.TopLevel = false;
                dataForm.Dock = DockStyle.Fill;
                tb.Controls.Add(dataForm);
                dataForm.Show();
                tab_data.TabPages.Add(tb);
            }
        }

        // 选项卡切换事件处理
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选中TabPage时，获取对应的通道
            Channel_UDP currentChannel = LoadConfig.Chs_UDP[tab_data.SelectedIndex];
        }

>>>>>>> 2632f6d1847636d5f8baa359cde7a7991b151c47
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

<<<<<<< HEAD
        // 停止测试按钮点击事件
=======
>>>>>>> 2632f6d1847636d5f8baa359cde7a7991b151c47
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

<<<<<<< HEAD
        //关闭上位机按钮点击事件
=======
>>>>>>> 2632f6d1847636d5f8baa359cde7a7991b151c47
        private void buttonCloseLowerMachine_Click(object sender, EventArgs e)
        {
            this.Close();
        }

<<<<<<< HEAD
        #endregion 按钮点击事件
=======
        private void ReceiveTimer_Tick(object sender, EventArgs e)
        {
            // Implement the logic for the timer tick event here.
            // For example, you can add code to perform periodic tasks.
            MessageBox.Show("定时器触发事件已执行");
        }
>>>>>>> 2632f6d1847636d5f8baa359cde7a7991b151c47
    }
}
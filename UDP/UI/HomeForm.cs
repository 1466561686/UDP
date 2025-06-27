using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDP.Interface;
using UDP.timer;
using UDP.Tools;

namespace UDP.UI
{
    public partial class HomeForm : Form
    {
        public static string PathDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string FilePathConfigUdp = PathDir + "\\ConfigFile\\BUS\\Device\\UDP\\UDPChannelConfig.csv"; //配置文件路径

        public UltraHighAccurateTimer ReceiveTimer; // 定义定时器
        private Dictionary<int, DataForm> dataForms = new Dictionary<int, DataForm>();
        private Dictionary<int, ChannelUdp> udpChannels = new Dictionary<int, ChannelUdp>();

        public HomeForm()
        {
            InitializeComponent();
            InitView(); // 初始化视图
        }

        //InitView 方法中的 Channel_UDP 实例化部分，使用配置文件获取端口参数
        private void InitView()
        {
            tabControl.TabPages.Clear();
            dataForms.Clear();
            udpChannels.Clear();
            LoadConfig.LoadUdp();

            foreach (var channel in LoadConfig.UdpChannels.Where(c => c.Id >= 0 && c.Id < 16))
            {
                int i = channel.Id;
                udpChannels[i] = channel;

                var tabPage = new TabPage
                {
                    Name = $"ch{i}",
                    Text = $"ch{i}"
                };
                var dataForm = new DataForm(i, channel, tabControl)
                {
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false,
                    Dock = DockStyle.Fill
                };
                dataForms[i] = dataForm;

                tabPage.Controls.Add(dataForm);
                dataForm.Show();
                tabControl.TabPages.Add(tabPage);
            }
        }

        #region 按钮

        private void buttonStartTest_Click(object sender, EventArgs e)
        {
            LoadConfig.LoadAllConfigs();
            // 打开所有UDP通道
            foreach (var channel in LoadConfig.GetAllChannels())
            {
                if (!channel.IsOpen)
                {
                    channel.Init();
                    if (channel.Init() == 0)
                    {
                        channel.IsOpen = true; // 或者Init内部已设置
                    }
                }
            }
            // 启动定时器
            if (this.ReceiveTimer == null)
            {
                this.ReceiveTimer = new UltraHighAccurateTimer();
                this.ReceiveTimer.Interval = 1000; // 1秒，可根据需要调整
            }
            this.ReceiveTimer.Start();
            MessageBox.Show("通道已打开，定时器已启动");
        }

        // 停止测试按钮点击事件

        private void buttonEndTest_Click(object sender, EventArgs e)
        {
            // 停止定时器
            if (this.ReceiveTimer != null)
            {
                this.ReceiveTimer.Stop();
            }
            // 关闭所有UDP通道
            foreach (var channel in LoadConfig.UdpChannels)
            {
                if (channel.IsOpen)
                {
                    channel.Close();
                }
            }
            MessageBox.Show("定时器已停止，所有UDP通道已关闭");
        }

        private void buttonCloseLowerMachine_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion 按钮
    }
}
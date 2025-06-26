using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using UDP.timer;

namespace UDP.UI
{
    public partial class DataForm : Form
    {
        private TabControl homeTabControl;

        // 自定义高精度定时器
        private UltraHightAccurateTimer receiveTimer;

        private Channel_UDP channelUdp;

        public DataForm(int i)
        {
        }

        // 构造函数
        public DataForm(Channel_UDP channel, TabControl tabControl)
        {
            InitializeComponent();
            this.channelUdp = channel; // 复用 HomeForm 中的 Channel_UDP
            this.homeTabControl = tabControl; // 保存引用
            InitializeView();
        }

        private void InitializeView()
        {
            // 初始化目标端口下拉框
            for (int i = 0; i < 16; i++)
            {
                SelectPort.Items.Add($"ch{i}");
            }
            SelectPort.SelectedIndex = 0; // 默认选中第一个端口
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!channelUdp.IsOpen)
            {
                MessageBox.Show("UDP service is not running.");
                return;
            }

            string message = txtSend.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("发送内容不能为空。");
                return;
            }
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            // 1. 获取下拉框选中的目标通道号

            // 2. 从配置文件获取目标IP和端口
            // 下拉框选中的目标通道号

            // 获取当前选中的Tab
            // 假设端口号和Tab索引一一对应

            // 设置本地端口

            // 3. 发送
        }

        // 释放自定义定时器
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (receiveTimer != null)
            {
                receiveTimer.Stop();
                receiveTimer = null;
            }
        }

        private void bt_clear_send_Click(object sender, EventArgs e)
        {
            txtSend.Clear();
        }

        private void bt_clear_recv_Click(object sender, EventArgs e)
        {
            txtReceived.Clear();
        }

        public void AppendUdpData(int channelIndex, string data)
        {
            txtReceived.AppendText(data + Environment.NewLine);
        }

        public void DisplayUdpDataToDataForm(int channelIndex, string msg)
        {
            //拿到channelIndex
            AppendUdpData(channelIndex, $"Channel {channelIndex}: {msg}");
        }
    }
}
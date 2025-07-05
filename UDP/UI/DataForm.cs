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
        private UltraHightAccurateTimer receiveTimer;
        private Channel_UDP channelUdp;
        private int channelIndex;

        public DataForm(int channelIndex)
        {
            InitializeComponent();
            this.channelIndex = channelIndex;
            InitializeView();
            InitializeUdpChannel();
        }

        // 构造函数
        public DataForm(Channel_UDP channel, TabControl tabControl)
        {
            InitializeComponent();
            this.channelUdp = channel;
            this.homeTabControl = tabControl;
            InitializeView();
            SetupChannelEvents();
        }

        private void InitializeView()
        {
            // 初始化目标端口下拉框
            for (int i = 0; i < 16; i++)
            {
                SelectPort.Items.Add($"ch{i}");
            }
            SelectPort.SelectedIndex = 0;
            
            // 设置文本框属性
            txtReceived.Multiline = true;
            txtReceived.ScrollBars = ScrollBars.Vertical;
            txtReceived.ReadOnly = true;
            
            txtSend.Multiline = true;
            txtSend.ScrollBars = ScrollBars.Vertical;
            
            // 设置默认发送内容
            txtSend.Text = "Hello UDP!";
        }

        private void InitializeUdpChannel()
        {
            // 如果没有传入channel，从LoadConfig中获取
            if (channelUdp == null && channelIndex < LoadConfig.Chs_UDP.Count)
            {
                channelUdp = LoadConfig.Chs_UDP[channelIndex];
            }
            
            SetupChannelEvents();
        }

        private void SetupChannelEvents()
        {
            if (channelUdp != null)
            {
                // 订阅数据接收事件
                channelUdp.DataReceived += OnDataReceived;
                channelUdp.ConnectionStateChanged += OnConnectionStateChanged;
                
                // 更新UI显示当前通道信息
                UpdateChannelInfo();
            }
        }

        private void OnDataReceived(byte[] data, IPEndPoint remoteEndPoint)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<byte[], IPEndPoint>(OnDataReceived), data, remoteEndPoint);
                return;
            }
            
            string receivedText = Encoding.UTF8.GetString(data);
            string timeStamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string message = $"[{timeStamp}] 从 {remoteEndPoint} 接收: {receivedText}";
            
            AppendToReceiveTextBox(message);
        }

        private void OnConnectionStateChanged(bool isConnected)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(OnConnectionStateChanged), isConnected);
                return;
            }
            
            string status = isConnected ? "已连接" : "已断开";
            string timeStamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string message = $"[{timeStamp}] 连接状态: {status}";
            
            AppendToReceiveTextBox(message);
        }

        private void UpdateChannelInfo()
        {
            if (channelUdp != null)
            {
                this.Text = $"UDP通道 - {channelUdp.Name} ({channelUdp.LocalIP}:{channelUdp.LocalPort})";
            }
        }

        private void AppendToReceiveTextBox(string message)
        {
            txtReceived.AppendText(message + Environment.NewLine);
            txtReceived.SelectionStart = txtReceived.Text.Length;
            txtReceived.ScrollToCaret();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (channelUdp == null)
            {
                MessageBox.Show("UDP通道未初始化。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!channelUdp.IsOpen)
            {
                MessageBox.Show("UDP通道未连接，请先启动通道。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = txtSend.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("发送内容不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 发送数据
            int result = channelUdp.SendString(message);
            
            string timeStamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string logMessage;
            
            if (result > 0)
            {
                logMessage = $"[{timeStamp}] 发送成功: {message} -> {channelUdp.TargetIp}:{channelUdp.TargetPort} ({result}字节)";
            }
            else
            {
                logMessage = $"[{timeStamp}] 发送失败: {message}";
            }
            
            AppendToReceiveTextBox(logMessage);
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
            if (InvokeRequired)
            {
                Invoke(new Action<int, string>(AppendUdpData), channelIndex, data);
                return;
            }
            
            AppendToReceiveTextBox(data);
        }

        public void DisplayUdpDataToDataForm(int channelIndex, string msg)
        {
            string timeStamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string message = $"[{timeStamp}] Channel {channelIndex}: {msg}";
            AppendUdpData(channelIndex, message);
        }

        // 释放资源
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            if (receiveTimer != null)
            {
                receiveTimer.Stop();
                receiveTimer = null;
            }
            
            if (channelUdp != null)
            {
                channelUdp.DataReceived -= OnDataReceived;
                channelUdp.ConnectionStateChanged -= OnConnectionStateChanged;
            }
        }

        // 启动UDP通道
        public void StartUdpChannel()
        {
            if (channelUdp != null && !channelUdp.IsOpen)
            {
                int result = channelUdp.Init();
                if (result == 0)
                {
                    string message = $"UDP通道启动成功: {channelUdp.LocalIP}:{channelUdp.LocalPort}";
                    AppendToReceiveTextBox(message);
                }
                else
                {
                    string message = $"UDP通道启动失败";
                    AppendToReceiveTextBox(message);
                }
            }
        }

        // 停止UDP通道
        public void StopUdpChannel()
        {
            if (channelUdp != null && channelUdp.IsOpen)
            {
                int result = channelUdp.Close();
                if (result == 0)
                {
                    string message = $"UDP通道已停止: {channelUdp.LocalIP}:{channelUdp.LocalPort}";
                    AppendToReceiveTextBox(message);
                }
                else
                {
                    string message = $"UDP通道停止失败";
                    AppendToReceiveTextBox(message);
                }
            }
        }
    }
}
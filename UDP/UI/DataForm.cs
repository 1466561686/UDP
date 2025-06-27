using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using UDP.Interface;
using UDP.timer;

namespace UDP.UI
{
    public partial class DataForm : Form
    {
        private TabControl homeTabControl;
        private ChannelUdp udpChannel;

        #region 初始化配置

        public DataForm(int i, ChannelUdp channel, TabControl tabControl)
        {
            InitializeComponent();
            this.udpChannel = channel; // 复用 HomeForm 中的 ChannelUdp
            this.homeTabControl = tabControl; // 使用复用 HomeForm 中的 TabControl
            InitializeView();
        }

        private void InitializeView()
        {
            //// 初始化目标端口下拉框
            //for (int i = 0; i < 16; i++)
            //{
            //    SelectPort.Items.Add($"ch{i}");
            //}
            //SelectPort.SelectedIndex = 0; // 默认选中第一个端口
            ////本地端口是tabPage的索引，目标端口是下拉框选择的端口
            //int localPort = homeTabControl.SelectedIndex; //本地端口
            //string targetPort = SelectPort.SelectedItem.ToString().Replace("ch", ""); // 目标端口
        }

        #endregion 初始化配置

        #region 按钮

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string message = txtSend.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("发送内容不能为空。");
                return;
            }

            // 只检查通道是否已打开
            if (udpChannel == null || !udpChannel.IsOpen)
            {
                MessageBox.Show("通道未打开，无法发送数据");
                return;
            }

            byte[] sendData = Encoding.UTF8.GetBytes(message);
            if (udpChannel.Client != null)
            {
                int sendResult = udpChannel.Send(sendData);
                if (sendResult == 0)
                {
                    MessageBox.Show(
                        $"发送成功！\n" +
                        $"从 {udpChannel.LocalIP}:{udpChannel.LocalPort}\n" +
                        $"到 {udpChannel.TargetIP}:{udpChannel.TargetPort}\n" +
                        $"内容：{message}",
                        "发送结果",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show("发送失败！");
                }
            }
            else
            {
                MessageBox.Show("发送失败！");
            }
        }

        public void AppendReceivedText(string message)
        {
            if (txtReceived.InvokeRequired)
            {
                Invoke(new Action(() => AppendReceivedText(message)));
                return;
            }
            string formattedMessage = $"[{DateTime.Now:HH:mm:ss}] 产生的消息： {message}\n";
            txtReceived.AppendText(formattedMessage);
        }

        private void bt_clear_send_Click(object sender, EventArgs e)
        {
            txtSend.Clear();
        }

        #endregion 按钮
    }
}
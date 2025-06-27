using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using UDP.Interface;

public class ChannelUdp : IChannel
{
    public UdpClient Client;
    public string LocalIP { get; set; }
    public int LocalPort { get; set; }
    public string TargetIP { get; set; }
    public int TargetPort { get; set; }

    public ChannelUdp(string localIP, int localPort, string targetIP, int targetPort)
    {
        LocalIP = localIP;
        LocalPort = localPort;
        TargetIP = targetIP;
        TargetPort = targetPort;
    }

    public ChannelUdp()
    {
    }

    public override int Init()
    {
        try
        {
            if (!IsOpen)
            {
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(LocalIP), LocalPort);
                Client = new UdpClient(localEndPoint);
                IsOpen = true;
            }
        }
        catch
        {
            MessageBox.Show("打开以太网端口" + LocalPort + "失败");
            return -1;
        }
        return 0;
    }

    public override int Close()
    {
        try
        {
            Client.Close();
            IsOpen = false;
        }
        catch
        {
            MessageBox.Show("关闭以太网端口失败");
            return -1;
        }
        return 0;
    }

    public override List<Byte[]> Recv()
    {
        if (!IsOpen)
        {
            return null;
        }
        List<Byte[]> receivedDatas = new List<byte[]>();
        while (Client.Available > 0)
        {
            IAsyncResult asyncResult = Client.BeginReceive(null, null);
            IPEndPoint remoteEndPoint = new IPEndPoint(0, 0);
            Byte[] receivedData = Client.EndReceive(asyncResult, ref remoteEndPoint);
            receivedDatas.Add(receivedData);
        }
        return receivedDatas;
    }

    public override int Send(byte[] SendData)
    {
        if (!IsOpen)
        {
            return -1;
        }
        if (SendData == null)
        {
            return -1;
        }
        Client.BeginSend(SendData, SendData.Length, new IPEndPoint(IPAddress.Parse(TargetIP), TargetPort), null, null);
        return 0;
    }
}
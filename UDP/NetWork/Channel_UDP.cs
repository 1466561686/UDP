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

public class Channel_UDP : IChannel
{
    public UdpClient Client;
    public IPAddress LocalIP { get; set; }
    public int LocalPort { get; set; }
    public IPAddress TargetIp { get; set; }
    public int TargetPort { get; set; }

    public Channel_UDP(IPAddress localIP, int localPort, IPAddress targetIP, int targetPort)
    {
        LocalIP = localIP;
        LocalPort = localPort;
        TargetIp = targetIP;
        TargetPort = targetPort;
    }

    public Channel_UDP()
    {
    }

    public override int Init()
    {
        try
        {
            if (!IsOpen)
            {
                IPEndPoint e = new IPEndPoint(LocalIP, LocalPort);
                Client = new UdpClient(e);
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
        List<Byte[]> Datas = new List<byte[]>();
        while (Client.Available > 0)
        {
            IAsyncResult iar = Client.BeginReceive(null, null);
            IPEndPoint ep = new IPEndPoint(0, 0);
            Byte[] data = Client.EndReceive(iar, ref ep);
            Datas.Add(data);
        }
        return Datas;
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
        Client.BeginSend(SendData, SendData.Length, TargetIp.ToString(), TargetPort, null, 0);
        return 0;
    }
}
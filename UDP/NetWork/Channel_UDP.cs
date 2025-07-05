using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDP.Interface;

public class Channel_UDP : IChannel
{
    public UdpClient Client;
    public IPAddress LocalIP { get; set; }
    public int LocalPort { get; set; }
    public IPAddress TargetIp { get; set; }
    public int TargetPort { get; set; }
    
    // 接收数据事件
    public event Action<byte[], IPEndPoint> DataReceived;
    // 连接状态变化事件
    public event Action<bool> ConnectionStateChanged;
    
    private CancellationTokenSource _cancellationTokenSource;
    private Task _receiveTask;

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
                IPEndPoint localEndPoint = new IPEndPoint(LocalIP, LocalPort);
                Client = new UdpClient(localEndPoint);
                
                // 启动异步接收
                _cancellationTokenSource = new CancellationTokenSource();
                _receiveTask = Task.Run(() => ReceiveDataAsync(_cancellationTokenSource.Token));
                
                IsOpen = true;
                ConnectionStateChanged?.Invoke(true);
                
                // 记录日志
                Console.WriteLine($"UDP通道已打开: {LocalIP}:{LocalPort} -> {TargetIp}:{TargetPort}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"打开UDP端口{LocalPort}失败: {ex.Message}");
            return -1;
        }
        return 0;
    }

    public override int Close()
    {
        try
        {
            IsOpen = false;
            
            // 停止接收任务
            _cancellationTokenSource?.Cancel();
            _receiveTask?.Wait(1000); // 等待最多1秒
            
            Client?.Close();
            Client?.Dispose();
            Client = null;
            
            _cancellationTokenSource?.Dispose();
            
            ConnectionStateChanged?.Invoke(false);
            Console.WriteLine($"UDP通道已关闭: {LocalIP}:{LocalPort}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"关闭UDP端口失败: {ex.Message}");
            return -1;
        }
        return 0;
    }

    // 异步接收数据
    private async Task ReceiveDataAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested && IsOpen)
        {
            try
            {
                if (Client?.Available > 0)
                {
                    var result = await Client.ReceiveAsync();
                    if (result.Buffer?.Length > 0)
                    {
                        // 触发数据接收事件
                        DataReceived?.Invoke(result.Buffer, result.RemoteEndPoint);
                        
                        // 记录接收日志
                        string receivedText = Encoding.UTF8.GetString(result.Buffer);
                        Console.WriteLine($"接收到数据: {receivedText} (来自: {result.RemoteEndPoint})");
                    }
                }
                
                // 短暂延迟，避免CPU占用过高
                await Task.Delay(10, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // 正常取消操作，退出循环
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"接收数据时出错: {ex.Message}");
                await Task.Delay(100, cancellationToken); // 出错时稍长延迟
            }
        }
    }

    public override List<Byte[]> Recv()
    {
        if (!IsOpen || Client == null)
        {
            return new List<byte[]>();
        }
        
        List<Byte[]> datas = new List<byte[]>();
        try
        {
            while (Client.Available > 0)
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = Client.Receive(ref remoteEndPoint);
                if (data?.Length > 0)
                {
                    datas.Add(data);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"同步接收数据时出错: {ex.Message}");
        }
        
        return datas;
    }

    public override int Send(byte[] sendData)
    {
        if (!IsOpen || Client == null)
        {
            return -1;
        }
        
        if (sendData == null || sendData.Length == 0)
        {
            return -1;
        }
        
        try
        {
            IPEndPoint targetEndPoint = new IPEndPoint(TargetIp, TargetPort);
            int sentBytes = Client.Send(sendData, sendData.Length, targetEndPoint);
            
            // 记录发送日志
            string sentText = Encoding.UTF8.GetString(sendData);
            Console.WriteLine($"发送数据成功: {sentText} -> {TargetIp}:{TargetPort} ({sentBytes}字节)");
            
            return sentBytes;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"发送数据失败: {ex.Message}");
            return -1;
        }
    }
    
    // 发送字符串数据
    public int SendString(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return -1;
        }
        
        byte[] data = Encoding.UTF8.GetBytes(message);
        return Send(data);
    }
}
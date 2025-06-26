using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDP.Tools;

namespace UDP.Interface
{
    public abstract class IChannel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string ChanId { set; get; }
        public bool IsOpen { set; get; }
        public EDeviceType DeviceType { set; get; }

        public abstract int Init();

        public abstract int Close();

        public abstract List<Byte[]> Recv();

        public abstract int Send(Byte[] Data);
    }
}
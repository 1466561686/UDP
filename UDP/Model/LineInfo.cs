using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP.Model
{
    class LineInfo
    {
        public string ICDNo { set; get; }
        public string SignalName { set; get; }
        public int SN { set; get; }
        public string ChannelId { set; get; }
        public string ChannelName { set; get; }
        public bool IsSelect { set; get; }
        public List<double> Values { set; get; }

}
}

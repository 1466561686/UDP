using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDP.Model
{
    class ICDConfig_RS422
    {
        public string ICDName { set; get; }
        public string ICDNo { set; get; }
        public int ChannelNum = 0;
        public string[] ChannelIds { set; get; }
        public string[] Ports { set; get; }
        public string[] FirstAddrs { set; get; }
    }
}

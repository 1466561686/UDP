using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP.Interface.impl
{
    public class Config_BUS
    {
        public int Id { set; get; }
        public string Type { set; get; }
        public string ChannelId { set; get; }
        public List<string> ICDNos { set; get; }
        public List<ConfigPar_BUS> ConfigPars = new List<ConfigPar_BUS>();
    }
}

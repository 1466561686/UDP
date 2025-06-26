using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDP.Model
{
    class JSONData
    {
        public int taskcode { set; get; }
        public string icdno { set; get; }
        public string channelid { set; get; }
        public int errorvalue { set; get; }
        public int sn { set; get; }
        public double setvalue { set; get; }
        public string setvaluestr { set; get; }
        public string ToJson() {
            return $"{{\"taskcode\":{taskcode}, \"icdno\":\"{icdno}\", \"channelid\":\"{channelid}\",\"errorvalue\":{errorvalue},\"sn\":{sn},\"setvalue\":{setvalue},\"setvaluestr\":\"{setvaluestr}\", }}";    
        }

        public byte[] ToJsonBytes() {
            string JsonStr = ToJson();
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonStr);
            return byteArray;
        }
    }
}

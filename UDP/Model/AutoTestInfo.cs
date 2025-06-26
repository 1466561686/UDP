using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDP.Model
{
    class AutoTestInfo
    {
        public string TestName { set; get; }
        public InICDInfo InInfo = new InICDInfo();
        public List<OutICDInfo> OutInfos = new List<OutICDInfo>();
    }

    class InICDInfo
    {
        public string ICDNo { set; get; }
        public List<InSignal> InSignals = new List<InSignal>();
    }

    class InSignal
    {
        public int SN { set; get; }
        public string SignalName { set; get; }
        public string[] InValue { set; get; }
        public string[] LLValue { set; get; }
        public string[] Range { set; get; }
    }
    class OutICDInfo
    {
        public int Id { set; get; }
        public string ICDNo { set; get; }
        public List<OutSignal> OutSignals = new List<OutSignal>();
    }
    class OutSignal
    {
        public int SN { set; get; }
        public string SignalName { set; get; }
    }
}

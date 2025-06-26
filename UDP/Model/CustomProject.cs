using UDP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDP.Interface;

namespace UpperMachineApp.Model
{
    public class CustomProject
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public List<Signal> RecvSignals { set; get; }
        public List<Signal> SendSignals { set; get; }
    }
}

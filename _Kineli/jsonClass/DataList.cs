using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class DataList
    {
        public int ack { get; set; }
        public string data { get; set; }
        public string dr { get; set; }
        public int fcnt { get; set; }
        public int freq { get; set; }
        public string gatewayId { get; set; }
        public int port { get; set; }
        public int rssi { get; set; }
        public double snr { get; set; }
        public UInt64 ts { get; set; }
        public string type { get; set; }
    }
}

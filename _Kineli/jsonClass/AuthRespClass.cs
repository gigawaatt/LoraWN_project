using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class AuthRespClass : BaseRespClass
    {
        public List<string> command_list { get; set; }
        public string device_access { get; set; }
        public RxSettings rx_settings { get; set; }
        public bool status { get; set; }
        public string token { get; set; }
        public bool? consoleEnable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class DevicesList
    {
        public string appEui { get; set; }
        public string devEui { get; set; }
        public string devName { get; set; }
        public int? fcnt_down { get; set; }
        public int? fcnt_up { get; set; }
        public int? last_data_ts { get; set; }
        public string device_type { get; set; }
        public string other_info_1 { get; set; }
    }
}

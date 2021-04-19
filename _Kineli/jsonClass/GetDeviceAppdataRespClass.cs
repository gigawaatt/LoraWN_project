using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class GetDeviceAppdataRespClass : BaseRespClass
    {
        public List<DevicesList> devices_list { get; set; }
        public bool status { get; set; }
    }
}

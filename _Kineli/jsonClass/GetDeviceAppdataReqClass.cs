using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{


    public class GetDeviceAppdataReqClass
    {
        public string cmd = "get_device_appdata_req";
        public List<string> keyword { get; set; }
        public AppEuiList select { get; set; }
    }

    public class AppEuiList
    {
        public List<string> appEui_list { get; set; }
    }
}

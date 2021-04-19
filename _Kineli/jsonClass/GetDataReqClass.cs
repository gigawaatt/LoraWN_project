using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class GetDataReqClass
    {
        public string cmd = "get_data_req";
        public string devEui { get; set; }
        public SelectClass select { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Kineli.jsonClass
{

    public class CloseAuthReqClass
    {
        public string cmd = "close_auth_req";
        public string token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class RecoverAuthReqClass
    {
        public string cmd = "token_auth_req";
        public string token { get; set; }
    }
}

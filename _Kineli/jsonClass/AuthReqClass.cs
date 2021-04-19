using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Kineli.jsonClass
{
    class AuthReqClass
    {
        public string cmd = "auth_req";
        public string login { get; set; }
        public string password { get; set; }
    }
}

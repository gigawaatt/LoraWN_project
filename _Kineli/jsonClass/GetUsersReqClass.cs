using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class GetUsersReqClass
    {
        public string cmd = "get_users_req";
        public string[] keyword { get; set; } = new string[0];
    }


}

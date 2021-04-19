using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class ErrorRespClass
    {
        public string err_string { get; set; }
    }
    public class ErrorCmdRespClass
    {
        public string cmd { get; set; }
        public string err_string { get; set; }
    }
    public class CmdRespClass
    {
        public string cmd { get; set; }
    }

}

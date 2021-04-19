using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class SelectClass
    {
       
        public int? date_from { get; set; }
        public int? date_to { get; set; }
        public int? begin_index { get; set; }
        public int? limit  { get; set; }
        public int? port { get; set; }
        public int? direction { get; set; }
        public bool? withMacCommands{ get; set; }
    }

}

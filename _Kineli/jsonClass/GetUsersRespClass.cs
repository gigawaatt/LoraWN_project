using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class GetUsersRespClass : BaseRespClass
    {
        public bool status { get; set; }
        public List<object> user_list { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{

    public class GetDataRespClass : BaseRespClass
    {
        public string appEui { get; set; }
        public List<DataList> data_list { get; set; }
        public string devEui { get; set; }
        public string direction { get; set; }
        public bool status { get; set; }
        public int totalNum { get; set; }
    }

}

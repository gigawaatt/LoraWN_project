using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{
    public class VegaPayloadRAKclass : IVegaPayloadClass
    {
        public double BatteryСharge { get; set; }
        public string Time_Packet { get; set; }

        public VegaPayloadRAKclass()
        {

        }

        public VegaPayloadRAKclass(UInt64 ts , string data)
        {
            Time_Packet = Convert.ToDateTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ts)).ToString();
            BatteryСharge = Convert.ToInt32(data.Substring(2, 2) + data.Substring(0, 2), 16)* 0.00161172161172161172161172161172;
        }

        public string toString()
        {
            return (Time_Packet + ";" + BatteryСharge + ";");
        }



    }
}

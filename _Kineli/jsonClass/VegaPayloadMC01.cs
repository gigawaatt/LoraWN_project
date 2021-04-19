using System;
using System.Collections.Generic;

namespace vegaws.jsonClass
{
    public class VegaPayloadMC0101Class : IVegaPayloadClass
    {
        public TypeOfPacketEnum TypeOfPacket;
        public int BatteryСharge;
        public int AngleDeflection;
        public int Temperature;
        public int SendReason;
        public int BitSensors;
        public DateTime TimeOfSend;
        public List<VegaParamsClass> VegaParams = new List<VegaParamsClass>();

        public VegaPayloadMC0101Class(string data)
        {
            try
            {
                if (data.Length >= 2)
                {
                    TypeOfPacket = (TypeOfPacketEnum) Convert.ToInt32(data.Substring(0, 2), 16);
                }

                if (data.Length >= 14 && TypeOfPacket == TypeOfPacketEnum.Config)
                {
                    var s = data.Substring(2, data.Length - 2);
                    var lengdata = Convert.ToInt32(data.Substring(4, 2), 16);
                    var d = new VegaParamsClass(Convert.ToInt32(data.Substring(0, 4), 16),data.Substring(6, lengdata));

                }

                if (data.Length >= 14 && TypeOfPacket == TypeOfPacketEnum.DataPacket)
                {
                    BatteryСharge = Convert.ToInt32(data.Substring(2, 2), 16);
                    AngleDeflection = Convert.ToInt32(data.Substring(4, 2), 16);
                    Temperature = Convert.ToInt32(data.Substring(6, 2), 16) + Convert.ToInt32(data.Substring(8, 2), 16)*256;
                    SendReason = Convert.ToInt32(data.Substring(10, 2), 16);
                    BitSensors = Convert.ToInt32(data.Substring(12, 2), 16);
                }                                                       
                if (data.Length >= 22 && TypeOfPacket == TypeOfPacketEnum.DataPacket)
                {
                    TimeOfSend = ConvertFromUnixTimestamp(data.Substring(14, 8));
                }
                if (data.Length >= 10 && TypeOfPacket == TypeOfPacketEnum.TimeCorrection)
                {
                    TimeOfSend = ConvertFromUnixTimestamp(data.Substring(2, 8));
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        private static DateTime ConvertFromUnixTimestamp(string timestamp)
        {
            var s = timestamp.Substring(6, 2) + timestamp.Substring(4, 2) + timestamp.Substring(2, 2) + timestamp.Substring(0, 2);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(Convert.ToInt64(s, 16)).ToLocalTime();
        }



        public override string ToString()
        {
            return $"TypeOfPacket:{TypeOfPacket} TimeOfSend: {TimeOfSend.ToString()}  BatteryСharge:{BatteryСharge} AngleDeflection :{AngleDeflection } Temperature:{Temperature/10} SendReason:{SendReason} BitSensors:{Convert.ToString(BitSensors, 2)}" ;
        }
    }
}
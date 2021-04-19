using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vegaws.jsonClass
{
    public class VegaPayloadCi11Class : IVegaPayloadClass
    {
        public TypeOfPacketEnum TypeOfPacket;
        public int BatteryСharge;
        public int BasicSettings;
        public int TriggerNumber;
        public DateTime TimeOfSend;
        public int CurrentReading1;
        public int CurrentReading2;
        public int CurrentReading3;
        public int CurrentReading4;
        public List<VegaParamsClass> VegaParams = new List<VegaParamsClass>();
        
   

        public VegaPayloadCi11Class()
        {
        }

        public VegaPayloadCi11Class(string data)
        {
            try
            {
                if (data.Length >= 2)
                {
                    
                    TypeOfPacket = (TypeOfPacketEnum)Convert.ToInt32(data.Substring(0, 2), 16);
                }

                if (data.Length >= 6 && TypeOfPacket == TypeOfPacketEnum.DataPacket)
                {
                    BatteryСharge = Convert.ToInt32(data.Substring(2, 2), 16);
                    BasicSettings = Convert.ToInt32(data.Substring(4, 2), 16);
                    TriggerNumber = 0;
                }
                else if (TypeOfPacket == TypeOfPacketEnum.TriggerPacket)
                {
                    BatteryСharge = Convert.ToInt32(data.Substring(2, 2), 16);
                    BasicSettings = Convert.ToInt32(data.Substring(4, 2), 16);
                    TriggerNumber = Convert.ToInt32(data.Substring(6, 2), 16);
                }

                if (data.Length >= 14 && TypeOfPacket == TypeOfPacketEnum.DataPacket)
                {
                    TimeOfSend = ConvertFromUnixTimestamp(data.Substring(6, 8));
                }
                else if (TypeOfPacket == TypeOfPacketEnum.TriggerPacket)
                {
                    TimeOfSend = ConvertFromUnixTimestamp(data.Substring(8, 8));
                }


                if (data.Length >= 38 && TypeOfPacket == TypeOfPacketEnum.DataPacket)
                {
                    CurrentReading1 = ConvertFromNormalСounter(data.Substring(16, 8));
                    CurrentReading2 = ConvertFromNormalСounter(data.Substring(24, 8));
                    CurrentReading3 = ConvertFromNormalСounter(data.Substring(32, 8));
                    CurrentReading4 = ConvertFromNormalСounter(data.Substring(40, 8));
                }
                else if (TypeOfPacket == TypeOfPacketEnum.TriggerPacket)
                {
                    CurrentReading1 = ConvertFromNormalСounter(data.Substring(16, 8));
                    CurrentReading2 = ConvertFromNormalСounter(data.Substring(24, 8));
                    CurrentReading3 = ConvertFromNormalСounter(data.Substring(32, 8));
                    CurrentReading4 = ConvertFromNormalСounter(data.Substring(40, 8));
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


        private static int ConvertFromNormalСounter(string counter)
        {
            string s = counter.Substring(6, 2) + counter.Substring(4, 2) + counter.Substring(2, 2) + counter.Substring(0, 2);
            return Convert.ToInt32(s, 16);
        }



        public override string ToString()
        {
            return $"TypeOfPacket: {TypeOfPacket} BatteryСharge: {BatteryСharge} TriggerNumber : {TriggerNumber } TimeOfSend: {TimeOfSend.ToString() } " + $" CurrentReading1:{CurrentReading1} CurrentReading2:{ CurrentReading2} CurrentReading3:{ CurrentReading3} CurrentReading4:{ CurrentReading4 } ";
        }

    }
}

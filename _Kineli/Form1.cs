using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using Newtonsoft.Json;
using _Kineli.jsonClass;
using vegaws.jsonClass;
using System.Threading;
using System.IO;
using System.Globalization;
using CsvHelper;

namespace _Kineli
{
    public partial class Form1 : Form
    {

        public WebSocket client;
        
        //private string host = "ws://10.11.0.145:8002";
        private string host = File.ReadLines("_input.txt").Skip(0).First();

        //private const string host = "ws://10.231.1.226:8002";
        //private const string host = "ws://10.228.64.44:8002";
        public List<VegaPayloadRAKclass> Battery_list = new List<VegaPayloadRAKclass>();
        public List<int> Check_List = new List<int>();
        public int Сounter1 = 0;
        public int timer_count = 0;
        string pathCsvFile = "C:/xampp/htdocs/IOT/result_5.csv";

        private void GetDataFromJson(string data)
        {
            LogData(data);
            var s = JsonConvert.DeserializeObject<ErrorRespClass>(data);
            if (s?.err_string != null)
            {
                GetError(data, s);
                return;
            }

   
            if (((data.Contains("green"))&&data.Contains("303838364E387B02"))||((data.Contains("green")) && data.Contains("343235374E386117"))|| ((data.Contains("green")) && data.Contains("3137353264386C0A")))
            {
                Сounter1++;
                timer1.Enabled = false;
                LogData2(" Датчик №1 отправил "+ Сounter1.ToString() + " пакет");
                label16.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    label16.Text = (Сounter1).ToString();
                });
                pictureBox3.Invoke((MethodInvoker)delegate
                {
                    pictureBox3.Visible = true;
                    //Thread.Sleep(2000);
                    //pictureBox1.Visible = true;
                });
                
                label5.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    label5.Text = "    Занят   ";
                    label5.BackColor = Color.Red;
                    label5.Refresh();
                    Thread.Sleep(1500);
                    label5.Text = "Свободен";
                    label5.BackColor = Color.Green;
                    label5.Refresh();

                });
                pictureBox3.Invoke((MethodInvoker)delegate
                {
                    pictureBox3.Visible = false;
                });




            }

            var authResp = JsonConvert.DeserializeObject<AuthRespClass>(data);
            /*if (authResp?.cmd == "auth_resp")
            {
                GetAuthResp(authResp);
                return;
            }
            */

            var getUsersResp = JsonConvert.DeserializeObject<GetUsersRespClass>(data);
            if (getUsersResp?.cmd == "get_users_resp")
            {
                GetUsersResp(getUsersResp);
                return;
            }


            var getDeviceResp = JsonConvert.DeserializeObject<GetDeviceAppdataRespClass>(data);
            if (getDeviceResp?.cmd == "get_device_appdata_resp")
            {
                GetDeviceResp(getDeviceResp);
                return;
            }

            var getDataResp = JsonConvert.DeserializeObject<GetDataRespClass>(data);
            if (getDataResp?.cmd == "get_data_resp")
            {
                GetDataResp(getDataResp);
                return;
            }
        }
        private void GetDataResp(GetDataRespClass getDataResp)
        {
            int z = 0;

            LogData($"    status:{getDataResp.status}"); 
            LogData($"    appEui:{getDataResp.appEui}");
            LogData($"    direction:{getDataResp.direction}");
            LogData($"    totalNum:{getDataResp.totalNum}");
            LogData($"    DeviceData:");

            Check_List.Add(getDataResp.data_list.Count());

            System.IO.StreamWriter str = new System.IO.StreamWriter("data.dat");

            foreach (var i in getDataResp.data_list)
            {
                if (i.data.Length > 0) Battery_list.Add(new VegaPayloadRAKclass(i.ts, i.data));


            }

            if (Battery_list.Count > 0)
            {
                using (var writer = new StreamWriter(pathCsvFile))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    
                    csv.WriteRecords(Battery_list);
                }
            }

            /*
            z = 0;
            System.IO.StreamWriter str = new System.IO.StreamWriter("data.dat");
            for (int i = TrigList.Count() - 1; i >= 0; i--)
            {
                if (TrigList[i].TypeOfPacket == TypeOfPacketEnum.DataPacket) //менять тип пакета в зависимости от необходимо
                {
                    z++;
                    (this).InvokeIfNeeded(() => textBoxBattery.Text = TrigList[i].BatteryСharge.ToString());
                    (this).InvokeIfNeeded(() => textBox4.Text = TrigList[0].TimeOfSend.ToString());
                    (this).InvokeIfNeeded(() => textBoxKvant.Text = (TrigList[0].CurrentReading1 - TrigList[1].CurrentReading1).ToString());

                    str.WriteLine(TrigList[i].TimeOfSend.ToString() + ';' + TrigList[i].CurrentReading1);
                }
            }
            str.Close();
            /*(this).InvokeIfNeeded(() => textBox1.Text = z.ToString()); //если триггер
            z = z / 4; */

            /*
            
            (this).InvokeIfNeeded(() => textBox1.Text = TrigList[0].CurrentReading1.ToString()); // если датапак
            z = TrigList[0].CurrentReading1 / 4;

            (this).InvokeIfNeeded(() => textBox3.Text = z.ToString());

            z = 0;
            TrigList.Clear();
            */


        }

        private void GetDeviceResp(GetDeviceAppdataRespClass getDeviseResp)
        {
            LogData($"    status:{getDeviseResp.status}");
            LogData($"    DeviceList:");
            foreach (var i in getDeviseResp.devices_list)
            {
                LogData($"         devEui:{i.devEui} appEui:{i.appEui} type:{i.device_type} devName:{i.devName} ({i.fcnt_up}/{i.fcnt_down}/{i.last_data_ts}/{i.other_info_1}) ");
            }
        }

        private void GetUsersResp(GetUsersRespClass getUsersResp)
        {
            LogData($"    UserList:");
            foreach (var i in getUsersResp.user_list)
            {
                LogData($"         {i}");
            }
        }

        private void GetAuthResp(AuthRespClass authResp)
        {
            LogData($"    token:{authResp.token}");
            LogData($"    status:{authResp.status}");
            LogData($"    device_access:{authResp.device_access}");
            (this).InvokeIfNeeded(() => tokenBox.Text = authResp.token);
            LogData($"    CommandList:");
            foreach (var i in authResp.command_list)
            {
                LogData($"         {i}");
            }
        }

        private void GetError(string data, ErrorRespClass sError)
        {
            var s1 = JsonConvert.DeserializeObject<ErrorCmdRespClass>(data);
            if (s1?.cmd != null)
                LogData($"ErrorCmd:{s1.cmd}, Error: {s1.err_string}");
            else
                LogData($"Error: {sError.err_string}");
            return;
        }

        private void LogData(string ee)
        {
            (this).InvokeIfNeeded(() => listBox1.Items.Insert(0 , DateTime.Now.ToString() + ":" + ee));
        }

        private void LogData2(string ee)
        {
            (this).InvokeIfNeeded(() => listBox2.Items.Insert(0, DateTime.Now.ToString() + ":" + ee));
        }

        public Form1()
        {
            InitializeComponent();
            //timer1.Start();
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            timer1.Start();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new WebSocket(host);
            //client.Connect();
            client.OnOpen += (ss, ee) => LogData(string.Format("Connected to {0}successfully", host));
            client.OnError += (ss, ee) => LogData(" Error: " + ee.Message);
            client.OnMessage += (ss, ee) => GetDataFromJson(ee.Data);
            client.OnClose += (ss, ee) => LogData(string.Format("Disconnected with {0}", host));
            client.Connect();
            Send_Ws_Message(new AuthReqClass() { login = "root", password = "123" });


        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            client.Connect();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            client.Close();
            //timer1.Stop();
        }


        private void Send_Ws_Message(object v)
        {
            var vtext = JsonConvert.SerializeObject(v);
            client.Send(vtext);
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            Send_Ws_Message(new AuthReqClass() { login = "root", password = "123" });
            //timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            /*Send_Ws_Message(new GetDataReqClass
            {
                devEui = "3739343570376905", // enter Deveui device RAK
                select = new SelectClass
                {
                    port = 2,
                    limit = 5000
                }

            });*/

            /*int cou = 0;
            

            cou++;

            if (cou == 2)
            {
                timer1.Stop();

                pictureBox1.Invoke((MethodInvoker)delegate
                {
                    pictureBox1.Visible = true;
                });
                label5.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    //Thread.Sleep(2000);
                    label5.Text = "Свободен";
                    label5.BackColor = Color.Green;
                    label5.Refresh();

                });
                cou = 0;
            }
            */
            




        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send_Ws_Message(new GetDataReqClass
            {
                devEui = "303838364E387B02", // enter Deveui device RAK
                select = new SelectClass
                {
                    port = 2,
                    limit = 1000000
                } 

            });
        }
    }
    }


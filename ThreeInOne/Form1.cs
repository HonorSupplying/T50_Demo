using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SetupSTEL
{
    public partial class frmMain : Form
    {
        private static frmMain form = null;
        private const int WM_SIGNPAD = 0x44A;

        private int _count = 0;
    
        public class SmartCardPosResponse
        {
            public string idNbr { get; set; }
            public string thaiNameTitle { get; set; }
            public string thaiFirstName { get; set; }
            public string thaiMiddleName { get; set; }
            public string thaiLastName { get; set; }
            public string thaiName { get; set; }
            public string engNameTitle { get; set; }
            public string engFirstName { get; set; }
            public string engMiddleName { get; set; }
            public string engLastName { get; set; }
            public string engName { get; set; }
            public string birthDateText { get; set; }
            public long dateOfBirth { get; set; }
            public string issueDateText { get; set; }
            public long issueDate { get; set; }
            public string expiryDateText { get; set; }
            public long expiryDate { get; set; }
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string addressLine3 { get; set; }
            public string addressLine4 { get; set; }
            public string road { get; set; }
            public string subDistrict { get; set; }
            public string district { get; set; }
            public string province { get; set; }
            public long errorCode { get; set; }
            public bool isCardExpired { get; set; }
            public string laserId { get; set; }
            public string bp1No { get; set; }
            public string chipNo { get; set; }
            public string deviceResponse { get; set; }
        }
        private class ConfigPort
        {
            public int port { get; set; }
        }
        private class CPosDeviceDLL
        {
            public bool connected = false;
            public bool connecting = false;

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void PosDeviceOpen();

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void PosDeviceClose();

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosMsreRead(byte[] buff);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public static extern int PosMsreWrite(string AccountId);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosSelect();

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosCancel();

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosCheckPort(int com);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public static extern int PosPinLoadMK(string KeyBuffer);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosPinReadKey(byte[] outData, int timeOut);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosPinReadKeyClear(byte[] outData, int timeOut);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosThaiIDRead(byte[] buff, int mode);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int PosEMVRead(byte[] buff);

            [DllImport("middleware.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public static extern int PosExec(byte[] outdata, byte[] buff, byte[] data);

            private static string ToHexString(byte[] hex, int size)
            {
                if (hex == null) return null;
                var s = new StringBuilder();
                for (int i = 0; i < size; i++)
                {
                    s.Append(hex[i].ToString("X2"));
                }
                return s.ToString();
            }

            public CPosDeviceDLL()
            {
                PosDeviceOpen();
            }

            ~CPosDeviceDLL()
            {
                PosDeviceClose();
            }

            public int Select()
            {
                return PosSelect();
            }

            public int Read(ref string buff)
            {
                byte[] trk2 = new byte[300];
                int ret = PosMsreRead(trk2);
                buff = System.Text.Encoding.ASCII.GetString(trk2);
                return ret;
            }

            public int EMVRead(ref string buff)
            {
                byte[] trk2 = new byte[1024];
                int ret = PosEMVRead(trk2);
                buff = System.Text.Encoding.ASCII.GetString(trk2);
                return ret;
            }
            public int ThaiIDRead(ref string buff, int mode)
            {
                byte[] trk2 = new byte[2048];
                int ret = PosThaiIDRead(trk2, mode);
                buff = System.Text.Encoding.UTF8.GetString(trk2);
                return ret;
            }
            public int ThaiIDPhoto(ref byte[] buff)
            {
                //byte[] trk2 = new byte[1024];
                return PosThaiIDRead(buff, 3);
                //buff = System.Text.Encoding.UTF8.GetString(trk2);
                //return ret;
            }
            public int Cancel()
            {
                return PosCancel();
            }
            public int CheckPort(int com)
            {
                return PosCheckPort(com);
            }

            public int Load(string buff)
            {
                string s = buff + char.MinValue;
                return PosPinLoadMK(s);
            }

            public int Read(ref string buff, int timeOut)
            {
                byte[] dummy = new byte[300];
                int ret = PosPinReadKey(dummy, timeOut);
                buff = ToHexString(dummy, 8);
                //buff = System.Text.Encoding.ASCII.GetString(dummy);
                return ret;
            }
            public int ReadClear(ref string buff, int timeOut)
            {
                byte[] dummy = new byte[300];
                int ret = PosPinReadKeyClear(dummy, timeOut);
                buff = ToHexString(dummy, 8);
                //buff = buff.Replace("F", "");
                buff = System.Text.Encoding.ASCII.GetString(dummy);
                return ret;
            }
            public int Exec(ref string outdata, string buff, string data)
            {
                byte[] dummy = new byte[100000];
                string s = buff + char.MinValue;
                Encoding utf8 = new UTF8Encoding(true);
                byte[] cmd1 = utf8.GetBytes(s);
                s = data + char.MinValue;
                byte[] cmd2 = utf8.GetBytes(s);
                int ret = PosExec(dummy, cmd1, cmd2);
                buff = ToHexString(dummy, 16);
                //MessageBox.Show(System.Text.Encoding.ASCII.GetString(dummy));
                //TextData.Text = dummy;
                outdata = System.Text.Encoding.UTF8.GetString(dummy);
                return ret;
            }
        }

        private CPosDeviceDLL threeInOne;

        public frmMain()
        {
            InitializeComponent();
            form = this;

        }
        void loadPorts()
        {
            //----- Get Available Port -----//
            cmb3in1.Items.Clear();

            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {

                cmb3in1.Items.Add("(Select)"); cmb3in1.Enabled = true;

                foreach (string port in ports)
                {
                
                    cmb3in1.Items.Add(port);
                }
            }
            else
            {
           
                cmb3in1.Items.Add("No COM"); cmb3in1.Enabled = false; btnTest3in1MCR.Enabled = false;
                btnTest3in1PIN.Enabled = false;
                btnTest3in1ID.Enabled = false;
            }
        }

        private void btnRefreshPort_Click(object sender, EventArgs e)
        {
            updateStatus("Refresh ports ...");
            loadPorts();
            cmb3in1.SelectedIndex = 0;

            //----- Auto Config all ports -----//
            int indexPort = 1;
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                foreach (string port in ports)
                {
                    updateStatus("Try to config port: " + port.ToString());
                    if (!threeInOne.connected)
                    {
                        updateStatus("Try connect 3in1 with port " + port + " : " + indexPort.ToString());
                        cmb3in1.SelectedIndex = indexPort; Application.DoEvents();
                        //cmb3in1_SelectedIndexChanged(null, null);
                        while (threeInOne.connecting) { }
                        if (threeInOne.connected) { indexPort++; continue; }
                    }
                    indexPort++;
                    if (threeInOne.connected) break;
                }
            }
            updateStatus("Auto Config Finished");
        }



        private void cmb3in1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb3in1.Text.Substring(0, 3) == "COM")
            {
                updateStatus("Connecting to 3 in 1 device ...");
                int nPort = Convert.ToInt32(Regex.Match(cmb3in1.Text, @"\d+").Value);
                int ret = threeInOne.CheckPort(nPort);
                if (ret == 0)
                {
                    updateStatus("3 in 1 device is connected"); threeInOne.connected = true;
                    lbl3in1.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    updateStatus("3 in 1 device is not connected"); threeInOne.connected = false;
                    lbl3in1.BackColor = System.Drawing.Color.Red;
                }
                btnTest3in1MCR.Enabled = (ret == 0);
                btnTest3in1PIN.Enabled = (ret == 0);
                btnTest3in1ID.Enabled = (ret == 0);
            }
            else threeInOne.connected = false;
        }

        private void btnTest3in1MCR_Click(object sender, EventArgs e)
        {
            string trk2 = "";

            updateStatus("Please swipe card ...");
            int ret = threeInOne.Read(ref trk2);

            if (ret != 1)
            {
                updateStatus("Data = " + trk2);
                return;
            }
            else
            {
                updateStatus("No Data");
            }
        }

        private void btnTest3in1PIN_Click(object sender, EventArgs e)
        {
            string trk2 = "";
            _count++;

            updateStatus("Please press PIN ...");
            int ret = _count % 2 == 0
                ? threeInOne.Read(ref trk2, 1000)
                : threeInOne.ReadClear(ref trk2, 1000);

            if (ret != 1)
            {
                updateStatus("Data = " + trk2);
            }
            else
            {
                updateStatus("No Data");
            }
        }


        void updateStatus(string s)
        {
            lblStatus.Text = s + "\r\n" + lblStatus.Text;
            lblStatus.Update();
        }


        private void btnTest3in1ID_Click(object sender, EventArgs e)
        {
            string smartcard0 = "{ \"format\":\"SCREEN\", \"slide\":\"smartcard-0.json\" }";
            string smartcard1 = "{ \"format\":\"SCREEN\", \"slide\":\"smartcard-1.json\" }";
            string checkinsert = "{ \"format\":\"APDU\", \"cmd\":\"card\" }";
            string slide0 = "{ \"format\":\"SCREEN\", \"slide\":\"slide0.json\" }";
            string outdata = "";
            int ret;
            ret = threeInOne.Exec(ref outdata, smartcard0, "");
            if (ret != 110)
            {
                do
                {
                    ret = threeInOne.Exec(ref outdata, checkinsert, "");
                    if (ret == 0) break;
                } while (ret != 0);

                if (ret != 0)
                {
                    threeInOne.Exec(ref outdata, slide0, "");
                } else
                {
                    threeInOne.Exec(ref outdata, smartcard1, "");
                    string id = "";
                    ret = threeInOne.ThaiIDRead(ref id, 0);
                    try
                    {
                        var readResult = JsonConvert.DeserializeObject<SmartCardPosResponse>(id);

                        if (!string.IsNullOrEmpty(readResult.idNbr))
                        {
                            updateStatus("idNbr : " + readResult.idNbr.Remove(0, 9).PadLeft(11, 'x'));
                            updateStatus("ThaiTitle : " + readResult.thaiNameTitle);
                            updateStatus("ThaiFirstName : " + readResult.thaiFirstName);
                            updateStatus("ThaiMiddleName : " + readResult.thaiMiddleName);
                            updateStatus("ThaiLastName : " + readResult.thaiLastName);
                            updateStatus("ThaiFullName : " + readResult.thaiName);
                            updateStatus("EngTitle : " + readResult.engNameTitle);
                            updateStatus("EngFirstName : " + readResult.engFirstName);
                            updateStatus("EngMiddleName : " + readResult.engMiddleName);
                            updateStatus("EngLastName : " + readResult.engLastName);
                            updateStatus("EngFullName : " + readResult.engName);
                            updateStatus("Birth Date : " + readResult.birthDateText);
                            updateStatus("Issue Date : " + readResult.issueDateText);
                            updateStatus("Expire Date : " + readResult.expiryDateText);
                            updateStatus("AddressLine1 : " + readResult.addressLine1);
                            updateStatus("AddressLine2 : " + readResult.addressLine2);
                            updateStatus("AddressLine3 : " + readResult.addressLine3);
                            updateStatus("AddressLine4 : " + readResult.addressLine4);
                            updateStatus("Road : " + readResult.road);
                            updateStatus("Sub District : " + readResult.subDistrict);
                            updateStatus("Dustrict : " + readResult.district);
                            updateStatus("Provice : " + readResult.province);
                            updateStatus("Laser ID : " + readResult.laserId);
                            updateStatus("BP1 no : " + readResult.bp1No);
                            updateStatus("Chip no : " + readResult.chipNo);

                        }
                        else
                        {
                            updateStatus("Cannot read IDCard");
                        }
                    }
                    catch
                    {
                        updateStatus("Timeout");
                    }
                }
            }
        }



        private void frmMain_Shown(object sender, EventArgs e)
        {

            Application.DoEvents();
            updateStatus("Load device objects ...");
            threeInOne = new CPosDeviceDLL();

            updateStatus("Load all ports ..."); Application.DoEvents();
            loadPorts();

            string config = "";


            //----- Load Config 3 in 1 -----//
            updateStatus("Load 3in1 profile ..."); Application.DoEvents();
            try
            {
                config = File.ReadAllText("config\\pc2pos.json");
                var configPort = JsonConvert.DeserializeObject<ConfigPort>(config);
                int i = cmb3in1.FindString("COM" + configPort.port.ToString());
                if (i >= 0)
                {
                    cmb3in1.SelectedIndex = i;
                }
                else
                {
                    int iNew = cmb3in1.Items.Add("COM" + configPort.port.ToString());
                    cmb3in1.SelectedIndex = iNew;
                }
                cmb3in1.Update();
            }
            catch (FileNotFoundException) { }

        }

        private void lbl3in1_Click(object sender, EventArgs e)
        {

        }
    }
}

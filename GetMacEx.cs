using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace GetMacEx
{
    class Program
    {
        static void Main(string[] args)
        {
            string chromeMessage = OpenStandardStreamIn();            
            
            Stream output = Console.OpenStandardOutput();
            String mac = "{\"mac\":\""+ GetMacAddress() + "\"}";
            byte[] bytes = Encoding.UTF8.GetBytes(mac);
            output.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
            output.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
            output.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
            output.WriteByte((byte)((bytes.Length >> 24) & 0xFF));          
            output.Write(bytes, 0, bytes.Length);
            output.Flush();
            output.Close();
            return;
        }

        private static string OpenStandardStreamIn()
        {
            //// We need to read first 4 bytes for length information
            Stream stdin = Console.OpenStandardInput();
            int length = 0;
            byte[] bytes = new byte[4];
            stdin.Read(bytes, 0, 4);
            length = System.BitConverter.ToInt32(bytes, 0);

            string input = "";
            for (int i = 0; i < length; i++)
            {
                input += (char)stdin.ReadByte();
            }

            return input;
        }

        public static string GetMacAddress()
        {
            try
            {
                List<string> macList = new List<string>()
                // string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        string mac = mo["MacAddress"].ToString();
                        if (!string.IsNullOrEmpty(mac))
                            {
                                macList.Add(mac);
                            }                      
                      // strMac = mo["MacAddress"].ToString();
d                    }
                }
                moc = null;
                mc = null;
                // return strMac;
                return string.Join(",", macList);
            }
            catch
            {
                return "unknown";
            }
        }

    }
}

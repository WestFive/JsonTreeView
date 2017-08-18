using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_DLL
{
    public class PLC 
    {
        private static SerialPort serialport;
        public static   SerialPort SerialPort { get { return serialport; } set { serialport = value; } }
               
        public static void Dispose()
        {
            Dispose();
        }

        public static void Init()
        {
            serialport = new SerialPort("COM21",115200);
            serialport.Open();

        }

        public static byte[] Recive()
        {
            string str = serialport.ReadExisting();
            return System.Text.Encoding.Default.GetBytes(str);
        }

        public static void Send(byte[] b)
        {
            serialport.Write(System.Text.Encoding.Default.GetString(b));
        }

 


    }
}

using System;
using System.Text;

namespace MoxaIO_Lib
{
    public class MXIO
    {
        string Input_IpAddress = "192.168.0.12";
        string Output_IpAddress = "192.168.0.20";

        UInt16 PORT = 502;
        string Password = "";
        UInt32 TimeOut = 5000;
        
        Int32 Input_Connection;
        Int32 Output_Connection;

        public void InitMoxaIO()
        {
            MXIO_Lib.MXEIO_Init();

            int result = MXIO_Lib.MXEIO_E1K_Connect(Encoding.UTF8.GetBytes(Input_IpAddress), PORT, TimeOut, out Input_Connection, Encoding.UTF8.GetBytes(Password));
            if(result != 0)
            {
                throw new Exception("Fail to connect with input card");
            }

            result = MXIO_Lib.MXEIO_E1K_Connect(Encoding.UTF8.GetBytes(Output_IpAddress), PORT, TimeOut, out Output_Connection, Encoding.UTF8.GetBytes(Password));
            if(result != 0)
            {
                throw new Exception("Fail to connect with Output card");
            }
        }

        public bool ReadMoxaInput(int portNo)
        {
            uint Read_Byte = 0;
            MXIO_Lib.E1K_DI_Reads(Input_Connection, (byte)portNo, 1, ref Read_Byte);
            return Read_Byte == 0 ? false : true;
        }

        public uint ReadMoxaInputAllBytes()
        {
            uint Read_Byte = 0;
            MXIO_Lib.E1K_DI_Reads(Input_Connection, 0, 16, ref Read_Byte);
            return Read_Byte;
        }

        public uint ReadMoxaOutputAllBytes()
        {
            uint Read_Byte = 0;
            MXIO_Lib.E1K_DI_Reads(Output_Connection, 0, 16, ref Read_Byte);
            return Read_Byte;
        }

        public bool ReadMoxaOutput(int portNo)
        {
            uint Read_Byte = 0;
            MXIO_Lib.E1K_DO_Reads(Output_Connection, (byte)portNo, 1, ref Read_Byte);
            return Read_Byte == 0 ? false : true;
        }

        public void WriteMoxaOutput(int portNo, bool state)
        {
            MXIO_Lib.E1K_DO_Writes(Output_Connection, (byte)portNo, 1, Convert.ToUInt32(state));
        }

        public String getInputIpAddress()
        {
            return this.Input_IpAddress;
        }
        public String getOutputIpAddress()
        {
            return this.Output_IpAddress;
        }

        public void setInputIpAddress(String Ip)
        {
            this.Input_IpAddress = Ip;
        }

        public void setOutputIpAddress(String Ip)
        {
            this.Output_IpAddress = Ip;
        }

    }
}

using System;
using System.Runtime.InteropServices;

namespace MoxaIO_Lib
{
    public class MXIO_Lib
    {
        const string MXIO_Dll_Path = @"MXIO_NET.dll";

        [DllImport(MXIO_Dll_Path)]
        public static extern int MXEIO_Init();

        [DllImport(MXIO_Dll_Path)]
        public static extern int MXEIO_E1K_Connect(byte[] szIP, UInt16 wPort, UInt32 dwTimeOut, out Int32 hConnection, byte[] szPassword);

        [DllImport(MXIO_Dll_Path)]
        public static extern int E1K_DI_Reads(Int32 hConnection, byte bytStartChannel, byte bytCount, ref UInt32 dwValue);

        [DllImport(MXIO_Dll_Path)]
        public static extern int E1K_DO_Reads(Int32 hConnection, byte bytStartChannel, byte bytCount, ref UInt32 dwValue);

        [DllImport(MXIO_Dll_Path)]
        public static extern int E1K_DO_Writes(Int32 hConnection, byte bytStartChannel, byte bytCount, UInt32 dwValue);
    }
}

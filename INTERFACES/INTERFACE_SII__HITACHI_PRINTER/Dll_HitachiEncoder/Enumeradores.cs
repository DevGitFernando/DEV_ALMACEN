using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_HitachiEncoder
{
    public enum NEMONICOS
    {
        NULO = 0,
        SOH = 1,
        STX = 2,
        ETX = 3,
        EOT = 4,
        ACK = 6, 
        DLE = 16,
        NAK = 21, 
        ESC = 27 
    }

    public enum Encabezados
    {

        H_02_PrintDataRecall_01 = 0x56,
        H_02_PrintDataRecall_02 = 0x26 
    }
}

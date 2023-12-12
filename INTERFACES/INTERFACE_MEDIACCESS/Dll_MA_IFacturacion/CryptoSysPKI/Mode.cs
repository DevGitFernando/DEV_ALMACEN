namespace CryptoSysPKI
{
    using System;

    public enum Mode
    {
        CBC = 0x100,
        CFB = 0x300,
        CTR = 0x400,
        ECB = 0,
        OFB = 0x200
    }
}


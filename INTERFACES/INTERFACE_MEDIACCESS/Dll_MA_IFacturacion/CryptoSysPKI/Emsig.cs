namespace CryptoSysPKI
{
    using System;

    internal enum Emsig
    {
        PKI_EMSIG_DEFAULT = 0x20,
        PKI_EMSIG_DIGESTONLY = 0x1000,
        PKI_EMSIG_DIGINFO = 0x2000,
        PKI_EMSIG_ISO9796 = 0x100000
    }
}


namespace CryptoSysPKI
{
    using System;

    internal class MyInternals
    {
        public static int HashBytes(HashAlgorithm alg)
        {
            switch (alg)
            {
                case HashAlgorithm.Sha1:
                    return 20;

                case HashAlgorithm.Md5:
                    return 0x10;

                case HashAlgorithm.Md2:
                    return 0x10;

                case HashAlgorithm.Sha256:
                    return 0x20;

                case HashAlgorithm.Sha384:
                    return 0x30;

                case HashAlgorithm.Sha512:
                    return 0x40;

                case HashAlgorithm.Sha224:
                    return 0x1c;
            }
            return 0x40;
        }

        public static string ModeString(Mode mode)
        {
            Mode mode2 = mode;
            if (mode2 <= Mode.CBC)
            {
                if ((mode2 != Mode.ECB) && (mode2 == Mode.CBC))
                {
                    return "CBC";
                }
            }
            else
            {
                switch (mode2)
                {
                    case Mode.OFB:
                        return "OFB";

                    case Mode.CFB:
                        return "CFB";

                    case Mode.CTR:
                        return "CTR";
                }
            }
            return "ECB";
        }
    }
}


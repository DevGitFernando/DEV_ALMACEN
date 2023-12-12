namespace CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Wipe
    {
        private Wipe()
        {
        }

        public static bool Data(byte[] data)
        {
            return (WIPE_Data(data, data.Length) == 0);
        }

        public static bool File(string fileName)
        {
            return (WIPE_File(fileName, 0) == 0);
        }

        public static bool String(StringBuilder sb)
        {
            return (WIPE_String(sb, sb.Capacity) == 0);
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int WIPE_Data(byte[] lpData, int datalen);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int WIPE_File(string strFileName, int flags);
        [DllImport("diCrPKI.dll", EntryPoint="WIPE_Data", CharSet=CharSet.Ansi)]
        private static extern int WIPE_String(StringBuilder lpData, int datalen);
    }
}


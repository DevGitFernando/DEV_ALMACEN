using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms; 

namespace Dll_IFacturacion.CryptoSysPKI
{
    /// <summary>
    /// General functions
    /// </summary>
    internal class GeneralPKI
    {
        private const int PKI_GEN_PLATFORM = 0x40;
        const string sDllLocation = "diCrPKI.dll";

        private GeneralPKI()
        { }	// Static methods only, so hide constructor.

        /* General FUNCTIONS */
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_LicenceType(int reserved);
        /// <summary>
        /// Gets licence type.
        /// </summary>
        /// <returns>D=Developer P=Personal T=Trial</returns>
        public static char LicenceType()
        {
            int n = PKI_LicenceType(0);
            char ch = (char)n;
            return ch;
        }
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_LastError(StringBuilder sbErrMsg, int nMsgLen);
        /// <summary>
        /// Retrieves the last error message set by the toolkit.
        /// </summary>
        /// <returns>Final error message from last call (may be empty)</returns>
        public static string LastError()
        {
            StringBuilder sb = new StringBuilder(0);
            int n = PKI_LastError(null, 0);
            sb = new StringBuilder(n);
            PKI_LastError(sb, sb.Capacity);
            return sb.ToString();
        }
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_ErrorCode();
        /// <summary>
        /// Returns the <see cref="General.ErrorLookup">error code</see> of the <em>first</em> error that occurred when calling the last function
        /// </summary>
        /// <returns>Error code</returns>
        public static int ErrorCode()
        {
            return PKI_ErrorCode();
        }
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_ErrorLookup(StringBuilder sbErrMsg, int nMsgLen, int nErrCode);
        /// <summary>
        /// Looks up error code
        /// </summary>
        /// <param name="errCode">Code number</param>
        /// <returns>Corresponding error message</returns>
        public static string ErrorLookup(int errCode)
        {
            StringBuilder sb = new StringBuilder(128);
            if (PKI_ErrorLookup(sb, sb.Capacity, errCode) > 0)
                return sb.ToString();
            else
                return String.Empty;
        }
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_CompileTime(StringBuilder sbTimestamp, int nLen);
        /// <summary>
        /// Gets date and time the core CryptoSys PKI DLL module was last compiled
        /// </summary>
        /// <returns>Date and time string</returns>
        public static string CompileTime()
        {
            StringBuilder sb = new StringBuilder(64);
            PKI_CompileTime(sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_ModuleName(StringBuilder sbModuleName, int nLen, int reserved);
        /// <summary>
        /// Gets full path name of core CryptoSys PKI DLL module
        /// </summary>
        /// <returns>File name</returns>
        public static string ModuleName()
        {
            StringBuilder sb = new StringBuilder(0);
            int n = PKI_ModuleName(sb, 0, 0);
            sb = new StringBuilder(n);
            PKI_ModuleName(sb, sb.Capacity, 0);
            return sb.ToString();
        }
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_PowerUpTests(int nOptions);
        /// <summary>
        /// Performs FIPS-140-2 start-up tests
        /// </summary>
        /// <returns>Zero on success</returns>
        public static int PowerUpTests()
        {
            return PKI_PowerUpTests(0);
        }

        // Note the fudge here to avoid `unsafe' pointers to ints
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int PKI_Version(byte[] dummyMajor, byte[] dummyMinor);
        /// <summary>
        /// Returns version number of core CryptoSys PKI DLL.
        /// </summary>
        /// <returns>Version number in form Major * 100 + Minor * 10 + Release. For example, version 2.9.0 would return 290.</returns>
        public static int Version()
        {
            int n = PKI_Version(null, null);
            return n;
        }

        // [Ver 3.4] Use "special" options to find out platform of core DLL

        /// <summary>
        /// Returns flag indicating the platform of the core DLL.
        /// </summary>
        /// <returns>1 if platform is Win64 (X64) or 0 if Win32</returns>
        public static int IsWin64()
        {
            return PKI_LicenceType(PKI_GEN_PLATFORM);
        }

        /// <summary>
        /// Gets the platform of the core DLL.
        /// </summary>
        /// <returns><c>"Win32"</c> or <c>"X64"</c></returns>
        public static string Platform()
        {
            StringBuilder sb = new StringBuilder(0);
            int n = PKI_ModuleName(sb, 0, PKI_GEN_PLATFORM);
            sb = new StringBuilder(n);
            PKI_ModuleName(sb, sb.Capacity, PKI_GEN_PLATFORM);
            return sb.ToString();
        }
    }

}

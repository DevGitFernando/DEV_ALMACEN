namespace AForge.Video.DirectShow.Internals
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    internal static class Win32
    {
        [DllImport("ole32.dll")]
        public static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);
        [DllImport("ntdll.dll")]
        public static extern int memcpy(int dst, int src, int count);
        [DllImport("ole32.dll", CharSet=CharSet.Unicode)]
        public static extern int MkParseDisplayName(IBindCtx pbc, string szUserName, ref int pchEaten, out IMoniker ppmk);
        [DllImport("olepro32.dll")]
        public static extern int OleCreatePropertyFrame(IntPtr hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int cObjects, [MarshalAs(UnmanagedType.Interface)] ref object ppUnk, int cPages, IntPtr lpPageClsID, int lcid, int dwReserved, IntPtr lpvReserved);
    }
}


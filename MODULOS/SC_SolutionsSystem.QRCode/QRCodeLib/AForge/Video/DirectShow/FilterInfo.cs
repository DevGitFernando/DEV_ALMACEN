namespace AForge.Video.DirectShow
{
    using AForge.Video.DirectShow.Internals;
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    public class FilterInfo : IComparable
    {
        public readonly string MonikerString;
        public readonly string Name;

        internal FilterInfo(IMoniker moniker)
        {
            this.MonikerString = this.GetMonikerString(moniker);
            this.Name = this.GetName(moniker);
        }

        public FilterInfo(string monikerString)
        {
            this.MonikerString = monikerString;
            this.Name = this.GetName(monikerString);
        }

        public int CompareTo(object value)
        {
            AForge.Video.DirectShow.FilterInfo info = (AForge.Video.DirectShow.FilterInfo) value;
            if (info == null)
            {
                return 1;
            }
            return this.Name.CompareTo(info.Name);
        }

        public static object CreateFilter(string filterMoniker)
        {
            object ppvResult = null;
            IBindCtx ppbc = null;
            IMoniker ppmk = null;
            int pchEaten = 0;
            if (Win32.CreateBindCtx(0, out ppbc) == 0)
            {
                if (Win32.MkParseDisplayName(ppbc, filterMoniker, ref pchEaten, out ppmk) == 0)
                {
                    Guid gUID = typeof(IBaseFilter).GUID;
                    ppmk.BindToObject(null, null, ref gUID, out ppvResult);
                    Marshal.ReleaseComObject(ppmk);
                }
                Marshal.ReleaseComObject(ppbc);
            }
            return ppvResult;
        }

        private string GetMonikerString(IMoniker moniker)
        {
            string str;
            moniker.GetDisplayName(null, null, out str);
            return str;
        }

        private string GetName(IMoniker moniker)
        {
            object ppvObj = null;
            IPropertyBag bag = null;
            string str2;
            try
            {
                Guid gUID = typeof(IPropertyBag).GUID;
                moniker.BindToStorage(null, null, ref gUID, out ppvObj);
                bag = (IPropertyBag) ppvObj;
                object pVar = "";
                int errorCode = bag.Read("FriendlyName", ref pVar, IntPtr.Zero);
                if (errorCode != 0)
                {
                    Marshal.ThrowExceptionForHR(errorCode);
                }
                string str = (string) pVar;
                if ((str == null) || (str.Length < 1))
                {
                    throw new ApplicationException();
                }
                str2 = str;
            }
            catch (Exception)
            {
                str2 = "";
            }
            finally
            {
                bag = null;
                if (ppvObj != null)
                {
                    Marshal.ReleaseComObject(ppvObj);
                    ppvObj = null;
                }
            }
            return str2;
        }

        private string GetName(string monikerString)
        {
            IBindCtx ppbc = null;
            IMoniker ppmk = null;
            string name = "";
            int pchEaten = 0;
            if (Win32.CreateBindCtx(0, out ppbc) == 0)
            {
                if (Win32.MkParseDisplayName(ppbc, monikerString, ref pchEaten, out ppmk) == 0)
                {
                    name = this.GetName(ppmk);
                    Marshal.ReleaseComObject(ppmk);
                    ppmk = null;
                }
                Marshal.ReleaseComObject(ppbc);
                ppbc = null;
            }
            return name;
        }
    }
}


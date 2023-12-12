namespace AForge.Video.DirectShow
{
    using AForge.Video.DirectShow.Internals;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    public class FilterInfoCollection : CollectionBase
    {
        public FilterInfoCollection(Guid category)
        {
            this.CollectFilters(category);
        }

        private void CollectFilters(Guid category)
        {
            object o = null;
            ICreateDevEnum enum2 = null;
            IEnumMoniker enumMoniker = null;
            IMoniker[] rgelt = new IMoniker[1];
            try
            {
                Type typeFromCLSID = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum);
                if (typeFromCLSID == null)
                {
                    throw new ApplicationException("Failed creating device enumerator");
                }
                o = Activator.CreateInstance(typeFromCLSID);
                enum2 = (ICreateDevEnum) o;
                if (enum2.CreateClassEnumerator(ref category, out enumMoniker, 0) != 0)
                {
                    throw new ApplicationException("No devices of the category");
                }
                IntPtr zero = IntPtr.Zero;
                while (true)
                {
                    if ((enumMoniker.Next(1, rgelt, zero) != 0) || (rgelt[0] == null))
                    {
                        break;
                    }
                    AForge.Video.DirectShow.FilterInfo info = new AForge.Video.DirectShow.FilterInfo(rgelt[0]);
                    base.InnerList.Add(info);
                    Marshal.ReleaseComObject(rgelt[0]);
                    rgelt[0] = null;
                }
                base.InnerList.Sort();
            }
            catch
            {
            }
            finally
            {
                enum2 = null;
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (enumMoniker != null)
                {
                    Marshal.ReleaseComObject(enumMoniker);
                    enumMoniker = null;
                }
                if (rgelt[0] != null)
                {
                    Marshal.ReleaseComObject(rgelt[0]);
                    rgelt[0] = null;
                }
            }
        }

        public AForge.Video.DirectShow.FilterInfo this[int index]
        {
            get
            {
                return (AForge.Video.DirectShow.FilterInfo) base.InnerList[index];
            }
        }
    }
}


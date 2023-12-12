namespace AForge.Video.DirectShow.Internals
{
    using System;
    using System.Runtime.InteropServices;

    internal static class Tools
    {
        public static IPin GetInPin(IBaseFilter filter, int num)
        {
            return GetPin(filter, PinDirection.Input, num);
        }

        public static IPin GetOutPin(IBaseFilter filter, int num)
        {
            return GetPin(filter, PinDirection.Output, num);
        }

        public static IPin GetPin(IBaseFilter filter, PinDirection dir, int num)
        {
            IPin[] pinArray = new IPin[1];
            IEnumPins enumPins = null;
            if (filter.EnumPins(out enumPins) == 0)
            {
                int num2;
                while (enumPins.Next(1, pinArray, out num2) == 0)
                {
                    PinDirection direction;
                    pinArray[0].QueryDirection(out direction);
                    if (direction == dir)
                    {
                        if (num == 0)
                        {
                            return pinArray[0];
                        }
                        num--;
                    }
                    Marshal.ReleaseComObject(pinArray[0]);
                    pinArray[0] = null;
                }
            }
            return null;
        }
    }
}


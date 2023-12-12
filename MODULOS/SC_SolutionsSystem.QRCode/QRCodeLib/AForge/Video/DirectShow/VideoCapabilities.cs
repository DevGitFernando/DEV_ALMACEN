namespace AForge.Video.DirectShow
{
    using AForge.Video.DirectShow.Internals;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class VideoCapabilities
    {
        public readonly Size FrameSize;
        public readonly int MaxFrameRate;

        internal VideoCapabilities()
        {
        }

        internal VideoCapabilities(IAMStreamConfig videoStreamConfig, int index)
        {
            AMMediaType mediaType = null;
            VideoStreamConfigCaps streamConfigCaps = new VideoStreamConfigCaps();
            try
            {
                int errorCode = videoStreamConfig.GetStreamCaps(index, out mediaType, streamConfigCaps);
                if (errorCode != 0)
                {
                    Marshal.ThrowExceptionForHR(errorCode);
                }
                this.FrameSize = streamConfigCaps.InputSize;
                this.MaxFrameRate = (int) (0x989680L / streamConfigCaps.MinFrameInterval);
            }
            finally
            {
                if (mediaType != null)
                {
                    mediaType.Dispose();
                }
            }
        }

        internal static VideoCapabilities[] FromStreamConfig(IAMStreamConfig videoStreamConfig)
        {
            int num;
            int num2;
            if (videoStreamConfig == null)
            {
                throw new ArgumentNullException("videoStreamConfig");
            }
            int numberOfCapabilities = videoStreamConfig.GetNumberOfCapabilities(out num, out num2);
            if (numberOfCapabilities != 0)
            {
                Marshal.ThrowExceptionForHR(numberOfCapabilities);
            }
            if (num <= 0)
            {
                throw new NotSupportedException("This video device does not report capabilities.");
            }
            if (num2 > Marshal.SizeOf(typeof(VideoStreamConfigCaps)))
            {
                throw new NotSupportedException("Unable to retrieve video device capabilities. This video device requires a larger VideoStreamConfigCaps structure.");
            }
            Dictionary<uint, VideoCapabilities> dictionary = new Dictionary<uint, VideoCapabilities>();
            for (int i = 0; i < num; i++)
            {
                VideoCapabilities capabilities = new VideoCapabilities(videoStreamConfig, i);
                uint key = (uint) ((capabilities.FrameSize.Height << 0x10) | capabilities.FrameSize.Width);
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, capabilities);
                }
            }
            VideoCapabilities[] array = new VideoCapabilities[dictionary.Count];
            dictionary.Values.CopyTo(array, 0);
            return array;
        }
    }
}


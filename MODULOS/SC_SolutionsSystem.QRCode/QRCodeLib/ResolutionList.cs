using System;
using System.Collections.Generic;
using System.Text;

namespace SC_SolutionsSystem.QRCode
{
    /// <summary>
    /// ResolutionList class for <see cref="Camera"/>.
    /// </summary>
    /// <remarks>This class is inherited from List<Resolution> class.</remarks>
    /// 
    /// <author> free5lot (free5lot@yandex.ru) </author>
    /// <version> 2013.10.16 </version>
    public class ResolutionList : List<Resolution>
    {
        /// <summary>
        /// Adds resolution to collection if it doesn't already exist in it
        /// </summary>
        /// <param name="item">Resolution should be added if it's new.</param>
        /// <returns>True if was added, False otherwise</returns>
        public bool AddIfNew(Resolution item)
        {
            if (this.Contains(item))
                return false;

            this.Add(item);
            return true;
        }
    }
}

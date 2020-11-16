using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSGISHelper
{
    public class OtherHelper
    {
        public static void ReleaseObject(object o)
        {
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(o);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            GC.Collect();

        }
    }
}

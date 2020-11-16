using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSGISHelper
{
   	/// <summary>
	/// DataRangeHelper 的摘要说明。
	/// 用来计算数据集的数据范围
	/// </summary>
    public class DataRangeHelper
    {
        public static double GetUnits(double pMinValue, double pMaxValue)
        {
            long aRange = (long)int.MaxValue;
            double realRange = Math.Abs(pMaxValue - pMinValue);
            double units = aRange / realRange;
            return units;
        }
        public static double GetMax(double pMinValue, double pUnits)
        {
            long aRange = (long)int.MaxValue;
            double realRange = aRange / pUnits;
            double aMax = pMinValue + realRange;
            return aMax;
        }
    }
}

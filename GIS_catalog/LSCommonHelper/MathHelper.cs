using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSCommonHelper
{
    public class MathHelper
    {
        // Methods
        public static double GetMJC(double a)
        {
            double num = Round((double)(a / 2.0), 2);
            return (Round((double)(num + num), 2) - a);
        }

        public static int Precent(double minValue, double maxValue, double curValue)
        {
            double num = maxValue - minValue;
            num = Math.Abs(num);
            if (num == 0.0)
            {
                return 0;
            }
            double num2 = curValue / num;
            return (int)(num2 * 100.0);
        }
        public static double Hailun(double pa, double pb, double pc)
        {
            double circle = (pa + pb + pc) / 2;
            double area = circle * (circle - pa) * (circle - pb)
            * (circle - pc);
            if (area < 0)
                return double.NaN;
            area = Math.Sqrt(area);
            return area;
        }
        public static double Round(double s, int n)
        {
            return (double)decimal.Round((decimal)s, n, MidpointRounding.AwayFromZero);
        }

        public static double Round(string ss, int n)
        {
            return Round(ConvertHelper.ObjectToDouble(ss), n);
        }
    }

 

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSCommonHelper
{
    public class ConvertHelper
    {
        // Methods
        public static double ObjectToDouble(object str)
        {
            try
            {
                if (str == null)
                {
                    return 0.0;
                }
                double result = 0.0;
                double.TryParse(str.ToString(), out result);
                return result;
            }
            catch
            {
                return 0.0;
            }
        }

        public static double ObjectToDouble(string str)
        {
            try
            {
                if ((str == "") || (str == null))
                {
                    return 0.0;
                }
                double result = 0.0;
                double.TryParse(str, out result);
                return result;
            }
            catch
            {
                return 0.0;
            }
        }

        public static double ObjectToFloat(object str)
        {
            try
            {
                if (str == null)
                {
                    return 0.0;
                }
                float result = 0f;
                float.TryParse(str.ToString(), out result);
                return result;
            }
            catch
            {
                return 0.0;
            }
        }

        public static double ObjectToFloat(string str)
        {
            try
            {
                if ((str == "") || (str == null))
                {
                    return 0.0;
                }
                float result = 0f;
                float.TryParse(str, out result);
                return result;
            }
            catch
            {
                return 0.0;
            }
        }

        public static int ObjectToInt(object str)
        {
            try
            {
                if (str == null)
                {
                    return 0;
                }
                int result = 0;
                int.TryParse(str.ToString(), out result);
                return result;
            }
            catch
            {
                return 0;
            }
        }

        public static int ObjectToInt(string str)
        {
            try
            {
                if (str == null)
                {
                    return 0;
                }
                if (str == "")
                {
                    return 0;
                }
                int result = 0;
                int.TryParse(str, out result);
                return result;
            }
            catch
            {
                return 0;
            }
        }

        public static string ObjectToString(object str)
        {
            try
            {
                if (str == null)
                {
                    return "";
                }
                return Convert.ToString(str);
            }
            catch
            {
                return "";
            }
        }
    }


}

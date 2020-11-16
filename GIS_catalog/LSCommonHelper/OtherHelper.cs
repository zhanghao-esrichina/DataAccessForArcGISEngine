using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading; 
namespace LSCommonHelper
{
    public class OtherHelper
    {
        // Methods
        public static string GetLeftName(string sName)
        {
            if (sName == "")
            {
                return "";
            }
            return sName.Substring(0, sName.IndexOf("|")).Trim();
        }

        public static string GetLeftName(string sName, string sFlag)
        {
            if (sName == "")
            {
                return "";
            }
            if (sName.IndexOf(sFlag) == -1) return sName;
            return sName.Substring(0, sName.IndexOf(sFlag)).Trim();
        }

        public static string GetRightName(string sName)
        {
            if (sName == "")
            {
                return "";
            }
            return sName.Substring(sName.IndexOf("|") + 1, (sName.Length - sName.IndexOf("|")) - 1).Trim();
        }

        public static string GetRightName(string sName, string sFlag)
        {
            if (sName == "")
            {
                return "";
            }
            if (sName.IndexOf(sFlag) == -1) return sName;
            return sName.Substring(sName.IndexOf(sFlag) + 1, (sName.Length - sName.IndexOf(sFlag)) - 1).Trim();
        }

        public static bool OnlyRunOncetime(Form frm, string sProductName)
        {
            bool createdNew = false;
            Mutex mutex = new Mutex(true, "OnlyRunOncetime", out createdNew);
            if (createdNew)
            {
                Application.Run(frm);
                mutex.ReleaseMutex();
                return true;
            }
            MessageBoxHelper.ShowMessageBox(sProductName + "已经启动");
            return false;
        }
    }


}

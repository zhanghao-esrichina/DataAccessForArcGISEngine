using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace LSCommonHelper
{
    public class INIHelper
    {
        // Methods
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public static string IniReadValue(string sIniPath, string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            int num = GetPrivateProfileString(Section, Key, "", retVal, 0xff, sIniPath);
            return retVal.ToString();
        }

        public static void IniWriteValue(string sIniPath, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, sIniPath);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }

 

}

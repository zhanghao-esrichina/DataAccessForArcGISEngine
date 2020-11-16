using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace LSCommonHelper
{
    public class MessageBoxHelper
    {
        // Methods
        public static void ShowErrorMessageBox(string sErr)
        {
            MessageBox.Show(sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowErrorMessageBox(Exception ex, string sErr)
        {
            MessageBox.Show(ex.Message, sErr, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowMessageBox(string sInfo)
        {
            MessageBox.Show(sInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }

 

}

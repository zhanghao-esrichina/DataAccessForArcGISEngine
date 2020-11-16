using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LSArcCatalog
{
    public partial class Rename : DevExpress.XtraEditors.XtraForm
    {
        public Rename()
        {
            InitializeComponent();
        }
     
        private string pOld = null;
        public string OldName
        {
            get { return pOld; }
            set { pOld = value; }
        }
        private void Rename_Load(object sender, EventArgs e)
        {
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Text = pOld;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string sNewName = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sNew = this.textEdit2.Text.Trim();
            if (sNew == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("ÇëÌîÐ´ÐÂÃû³Æ");
                return;
            }
            sNewName = sNew;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
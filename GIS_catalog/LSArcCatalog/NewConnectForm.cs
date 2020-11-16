using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
namespace LSArcCatalog
{
    public partial class NewConnectForm : DevExpress.XtraEditors.XtraForm
    {
        public NewConnectForm()
        {
            InitializeComponent();
        }
        private IWorkspace mWS = null;
        public IWorkspace Workspace
        {
            get { return mWS; }
            set { mWS = value; }
        }
        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void testConnection_Click(object sender, EventArgs e)
        {
            if (Ischeck() == false) return;
            #region ���ӵ�SDE
            IPropertySet aProp = new PropertySetClass();
            aProp.SetProperty("server", this.server.Text);
            aProp.SetProperty("instance", this.service.Text);
            aProp.SetProperty("user", this.userName.Text);
            aProp.SetProperty("password", this.password.Text);
            aProp.SetProperty("version", "sde.DEFAULT");
            SdeWorkspaceFactoryClass aFac = new SdeWorkspaceFactoryClass();
            try
            {
                IWorkspace aWK = aFac.Open(aProp, 0);
                if (aWK == null)
                {
                    MessageBox.Show("�������ӵ�SDE", "��������"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("���Ӳ��Գɹ�!", "��������"
                         , MessageBoxButtons.OK
                         , MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "�������ӵ�SDE");
                return;
            }
            #endregion
        }
        private bool Ischeck()
        {
            if (this.server.Text.Trim() == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("��������������Ϊ��");
                return false;
            }
            if (this.service.Text.Trim() == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("�˿ں�Ϊ�գ�����д��5151");
                return false;
            }

            if (this.userName.Text.Trim() == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("��½�û���Ϊ��");
                return false;
            }
            if (this.password.Text.Trim() == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("��½����Ϊ��");
                return false;
            }
            return true;
        }
        private void enter_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ischeck() == false) return;
                string sServer = this.server.Text.Trim();
                string sPort = this.service.Text.Trim();
                string sUser = this.userName.Text.Trim();
                string sPwd = this.password.Text.Trim();
                LSCommonHelper.DataBaseHelper.Connstring = LSCommonHelper.DataBaseHelper.AccessConnectionString;

                string sql = "insert into dataconnect (server,port,user,pwd,version) values('" + sServer +
                    "','" + sPort + "','" + sUser + "','" + sPwd + "','SDE.DEFAULT')";
                LSCommonHelper.DataBaseHelper.getcmd(sql);
                this.Close();
            }
            catch { }
        }

        private void NewConnectForm_Load(object sender, EventArgs e)
        {
            if (mWS != null)
            {
                this.server.Text = mWS.ConnectionProperties.GetProperty("server").ToString();
                this.service.Text = mWS.ConnectionProperties.GetProperty("instance").ToString();
                this.userName.Text = mWS.ConnectionProperties.GetProperty("user").ToString();
               // this.password.Text = mWS.ConnectionProperties.GetProperty("password").ToString();
            }
        }

        private void changeVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
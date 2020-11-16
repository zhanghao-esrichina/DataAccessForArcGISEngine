using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using LSCommonHelper;
using LSGISHelper;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesRaster;
using System.IO;

namespace LSArcCatalog.Other
{
    public partial class PrivilegeForm : DevExpress.XtraEditors.XtraForm
    {
        public PrivilegeForm()
        {
            InitializeComponent();
        }
        private ISQLPrivilege pSql = null;
        public ISQLPrivilege SQLPrivilege
        {
            get { return pSql; }
            set { pSql = value; }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void PrivilegeForm_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sUser = this.txtUser.Text.Trim();
            if (sUser == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("请输入赋权用户");
                return;
            }
            try
            {
                int n = this.cboRead.SelectedIndex;
                int m = this.cboWrite.SelectedIndex;
                if (n == 1)
                {
                    pSql.Grant(sUser, 1, false);
                }
                else
                {
                    pSql.Revoke(sUser, 1);
                }

                if (m == 1)
                {
                    pSql.Grant(sUser, 2, false);
                    pSql.Grant(sUser, 4, false);
                    pSql.Grant(sUser, 8, false);
                }
                else
                {
                    pSql.Revoke(sUser, 2);
                    pSql.Revoke(sUser, 4);
                    pSql.Revoke(sUser, 8);
                }
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("授权成功");
            }
            catch (Exception ex)
            { LSCommonHelper.MessageBoxHelper.ShowMessageBox("授权失败"); }
        }

    }
    //public class PrivilegeClass
    //{
    //    private string sUser = "";
    //    public string User
    //    {
    //        get { return sUser; }
    //        set { sUser = value; }
    //    }
    //}
}
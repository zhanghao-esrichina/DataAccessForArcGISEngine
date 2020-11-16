using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
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

using ESRI.ArcGIS.DataSourcesRaster;
namespace LSArcCatalog.Version
{
    public partial class NewVersion : DevExpress.XtraEditors.XtraForm
    {
        public NewVersion()
        {
            InitializeComponent();
        }
        private IVersion m_pParentV = null;
        public IVersion ParentVersion
        {
            get { return m_pParentV; }
            set { m_pParentV = value; }
        }
        private List<string> m_pVNameList = new List<string>();
        public List<string> VersionNameList
        {
            get { return m_pVNameList; }
            set { m_pVNameList = value; }
        }
        public IVersion ChildVersion = null;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sName = this.textEdit1.Text.Trim();
            if (sName == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("请输入子版本名称");
                return;
            }
            string sDes = this.memoEdit1.Text.Trim();
            int n = this.radioGroup1.SelectedIndex;
            if (!clsVersionClass.CheckVersionName(this.m_pVNameList, sName))
            {
                ChildVersion = this.m_pParentV.CreateVersion(sName);
                ChildVersion.Description = sDes;
                if (n == 0)
                {
                    ChildVersion.Access = esriVersionAccess.esriVersionAccessPublic;
                }
                else if (n == 1)
                {
                    ChildVersion.Access = esriVersionAccess.esriVersionAccessPrivate;
                }
                else if (n == 2)
                {
                    ChildVersion.Access = esriVersionAccess.esriVersionAccessProtected;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
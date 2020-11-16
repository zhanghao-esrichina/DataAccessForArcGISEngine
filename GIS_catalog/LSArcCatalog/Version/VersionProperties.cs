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
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesRaster;
namespace LSArcCatalog.Version
{
    public partial class VersionProperties : DevExpress.XtraEditors.XtraForm
    {
        public VersionProperties()
        {
            InitializeComponent();
            this.memoEdit1.Enabled = false;
            this.radioGroup1.Enabled = false;
        }
        private IVersionInfo m_pInfo = null;
        public IVersionInfo VersionInfo
        {
            get { return m_pInfo; }
            set { m_pInfo = value; }
        }
        private void VersionProperties_Load(object sender, EventArgs e)
        {
            this.labelControl8.Text = m_pInfo.VersionName;
            this.labelControl9.Text = LSCommonHelper.OtherHelper.GetLeftName(m_pInfo.VersionName, ".");
            if (m_pInfo.Parent != null)
            {
                this.labelControl10.Text = m_pInfo.Parent.VersionName;
            }
            else
            {
                this.labelControl10.Text = "";
            }
            this.labelControl11.Text = m_pInfo.Created.ToString();
                
            this.labelControl12.Text = m_pInfo.Modified.ToString();
            this.memoEdit1.Text = m_pInfo.Description;
           
            if (m_pInfo.Access == esriVersionAccess.esriVersionAccessPublic)
            {
                this.radioGroup1.SelectedIndex = 0;
            }
            else if (m_pInfo.Access == esriVersionAccess.esriVersionAccessPrivate)
            {
                this.radioGroup1.SelectedIndex = 1;
            }
            else if (m_pInfo.Access == esriVersionAccess.esriVersionAccessProtected)
            {
                this.radioGroup1.SelectedIndex = 2;
            }
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
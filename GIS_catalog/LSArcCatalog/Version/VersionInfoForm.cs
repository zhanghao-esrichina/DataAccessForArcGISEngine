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
namespace LSArcCatalog
{
    public partial class VersionInfoForm : DevExpress.XtraEditors.XtraForm
    {
        public VersionInfoForm()
        {
            InitializeComponent();
        }
        private IWorkspace m_pSDEWorkspace = null;
        public IWorkspace Workspace
        {
            get { return m_pSDEWorkspace; }
            set { m_pSDEWorkspace = value; }
        }
        private void VersionInfoForm_Load(object sender, EventArgs e)
        {
            LoadVersion();
        }
        List<string> pVersionNameList = new List<string>();
        private void LoadVersion()
        {
            this.listView1.Items.Clear();
            List<IVersionInfo> pList = clsVersionClass.GetVersionInfoList(this.m_pSDEWorkspace);
            foreach (IVersionInfo pInfo in pList)
            {
                ListViewItem lv = new ListViewItem();
                lv.SubItems.Add(pInfo.VersionName);
                pVersionNameList.Add(pInfo.VersionName);
                lv.SubItems.Add(LSCommonHelper.OtherHelper.GetLeftName(pInfo.VersionName, "."));
                esriVersionAccess pAccess = pInfo.Access;
                lv.SubItems.Add(pAccess.ToString());
                lv.SubItems.Add(pInfo.Modified.ToString());
                lv.Tag = pInfo;
                this.listView1.Items.Add(lv);
            }
        }
        private void 新建版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            IVersion pVersion = this.SeleteVersion;
                    if (pVersion != null)
                    {
                        if (clsVersionClass.CheckVersionLocks(pVersion))
                        {
                            Version.NewVersion frm = new LSArcCatalog.Version.NewVersion();
                            frm.ParentVersion = pVersion;
                            frm.VersionNameList = this.pVersionNameList;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                IVersion pChildVersion = frm.ChildVersion;
                                IVersionInfo pInfo = pChildVersion.VersionInfo;
                                ListViewItem lv = new ListViewItem();
                                lv.SubItems.Add(pInfo.VersionName);
                                pVersionNameList.Add(pInfo.VersionName);
                                lv.SubItems.Add(LSCommonHelper.OtherHelper.GetLeftName(pInfo.VersionName, "."));
                                esriVersionAccess pAccess = pInfo.Access;
                                lv.SubItems.Add(pAccess.ToString());
                                lv.SubItems.Add(pInfo.Modified.ToString());
                                lv.Tag = pInfo;
                                this.listView1.Items.Add(lv);
                            }
                        }
                
            }
        }
        public IVersion SeleteVersion
        {
            get
            {
                if (this.listView1.Items.Count > 0)
                {
                    if (this.listView1.SelectedItems.Count == 0) return null;
                    if (this.listView1.SelectedItems[0] != null)
                    {
                        IVersionedWorkspace pVW = this.m_pSDEWorkspace as IVersionedWorkspace;
                        IVersionInfo pVInfo = this.listView1.SelectedItems[0].Tag as IVersionInfo;
                        IVersion pVersion = pVW.FindVersion(pVInfo.VersionName);
                        return pVersion;
                    }
                }
                return null;
            }
        }
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IVersion pVersion = this.SeleteVersion;
            if (pVersion != null)
            {
                Rename frm = new Rename();
                frm.OldName = pVersion.VersionName;
                string sNew = "";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    sNew = frm.sNewName;
                    pVersion.VersionName = sNew;
                    LoadVersion();
                }
            }
        }

        private void 删除版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
              DialogResult dr = MessageBox.Show("是否删除?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
              if (dr == DialogResult.Yes)
              {
                  IVersion pVersion = this.SeleteVersion;
                  if (pVersion != null)
                  {
                      pVersion.Delete();
                      LoadVersion();
                      LSCommonHelper.MessageBoxHelper.ShowMessageBox("删除完毕");
                  }
              }
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadVersion();
        }

        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count > 0)
            {
                if (this.listView1.SelectedItems.Count == 0) return;
                if (this.listView1.SelectedItems[0] != null)
                {
                    IVersionInfo pInfo = this.listView1.SelectedItems[0].Tag as IVersionInfo;
                    Version.VersionProperties frm = new LSArcCatalog.Version.VersionProperties();
                    frm.VersionInfo = pInfo;
                    LSCommonHelper.ControlStyleHelper.InitFormShowDialogStyle(frm);
                }
            }
        }
    }
}
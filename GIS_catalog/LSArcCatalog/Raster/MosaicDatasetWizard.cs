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
using System.IO;
namespace LSArcCatalog.Raster
{
    public partial class MosaicDatasetWizard : DevExpress.XtraEditors.XtraForm
    {
        public MosaicDatasetWizard()
        {
            InitializeComponent();
            this.buttonEdit1.Properties.ReadOnly = true;
        }
        private IWorkspace pWS = null;
        public IWorkspace Workspace
        {
            get { return pWS; }
            set { pWS = value; }
        }

        private void MosaicDatasetWizard_Load(object sender, EventArgs e)
        {
            List<IConfigurationKeyword> pList = LSGISHelper.WorkspaceHelper.GetConfigurationKeywordList(pWS);
            foreach (IConfigurationKeyword pKey in pList)
            {
                this.cboKeyword.Properties.Items.Add(pKey.Name);
            }
            this.cboKeyword.SelectedIndex = 0;
        }
        private ISpatialReference sr = null;
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "投影文件(.prj)|*.prj";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                sr = LSGISHelper.SpatialReferenceHelper.ReadSR(
                    ofd.FileName);
                this.buttonEdit1.Text = sr.Name;

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public MosaicDatasetClass cls = new MosaicDatasetClass();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sName = this.txtName.Text.Trim();
            if (sName == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("请输入镶嵌数据集名称");
                return;
            }
            cls.Name = sName;
            cls.KeyWord = this.cboKeyword.Text.Trim();
            cls.SpatialReference = this.sr;
            ICreateMosaicDatasetParameters pPara = new CreateMosaicDatasetParametersClass();
            pPara.BandCount = LSCommonHelper.ConvertHelper.ObjectToInt(this.cboBandNum);
            pPara.PixelType = (rstPixelType)Enum.Parse(typeof(rstPixelType),
                this.cboPixelType.Text.Trim());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
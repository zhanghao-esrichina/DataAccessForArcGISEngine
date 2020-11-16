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
    public partial class RasterDatasetWizard : DevExpress.XtraEditors.XtraForm
    {
        public RasterDatasetWizard()
        {
            InitializeComponent();
            this.m_srControl =new SpatialReferenceControl ();
            this.m_srControl.Dock = DockStyle.Fill;
            this.m_srControl.cbPrecision.Checked = true;
            this.panelControl3.Controls.Add(this.m_srControl);

        }
        public RasterDatasetClass pRD=new RasterDatasetClass();
        private SpatialReferenceControl m_srControl;
        private IWorkspace pWS=null;
        public IWorkspace  Workspace
        {
            get{return pWS;}
            set{pWS=value;}
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sName=this.txtName.Text.Trim();
            if(sName=="")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("请输入影像数据集名称");
                return;
            }
            ISpatialReference sr=this.m_srControl.OutputSR;
            pRD.Name=sName;
            pRD.NumRand=LSCommonHelper.ConvertHelper.ObjectToInt(this.cboBandNum.Text.Trim());
            pRD.KeyWord = this.cboKeyword.Text.Trim();
            IRasterStorageDef pRSDef = new RasterStorageDefClass();
            pRSDef.CompressionQuality = LSCommonHelper.ConvertHelper.ObjectToInt(
                this.cboquality.Text.Trim());
            pRSDef.CompressionType = (esriRasterCompressionType)Enum.Parse(typeof(esriRasterCompressionType),
                this.cboCompressType.Text.Trim());
            pRSDef.PyramidLevel = LSCommonHelper.ConvertHelper.ObjectToInt(
                this.cboLevel.Text.Trim());
            pRSDef.PyramidResampleType = (rstResamplingTypes)Enum.Parse(typeof(rstResamplingTypes),
                this.cboResample.Text.Trim());
            pRSDef.TileHeight = LSCommonHelper.ConvertHelper.ObjectToInt(
               this.txtHeight.Text.Trim());
            pRSDef.TileWidth = LSCommonHelper.ConvertHelper.ObjectToInt(
               this.txtWidth.Text.Trim());
            IGeometryDef pGDef = new GeometryDefClass();
            IGeometryDefEdit pGDEdit = pGDef as IGeometryDefEdit;
            pGDEdit.SpatialReference_2 = sr;

            IRasterDef pRDef = new RasterDefClass();
            pRDef.SpatialReference = sr;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RasterDatasetWizard_Load(object sender, EventArgs e)
        {
         
            
            List<IConfigurationKeyword> pList=LSGISHelper.WorkspaceHelper.GetConfigurationKeywordList(pWS);
            foreach (IConfigurationKeyword pKey in pList)
            {
                this.cboKeyword.Properties.Items.Add(pKey.Name);
            }
            this.cboKeyword.SelectedIndex = 0;
        }

  

      
    }
}
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
    public partial class RasterCatalogWizard : DevExpress.XtraEditors.XtraForm
    {
        public RasterCatalogWizard()
        {
            InitializeComponent();
            this.buttonEdit1.Properties.ReadOnly = true;
            this.buttonEdit2.Properties.ReadOnly = true;
        }
        private IWorkspace pWS = null;
        public IWorkspace Workspace
        {
            get { return pWS; }
            set { pWS = value; }
        }
        public RasterCatalogClass cls = new RasterCatalogClass();
        private void RasterCatalogWizard_Load(object sender, EventArgs e)
        {
            List<IConfigurationKeyword> pList = LSGISHelper.WorkspaceHelper.GetConfigurationKeywordList(pWS);
            foreach (IConfigurationKeyword pKey in pList)
            {
                this.cboKeyword.Properties.Items.Add(pKey.Name);
            }
            this.cboKeyword.SelectedIndex = 0;
            #region 构建默认字段
            if (cls.Fields.FindField("OBJECTID") < 0)
            {
                IField oid = LSGISHelper.FieldHelper.CreateOIDField();
                (cls.Fields as IFieldsEdit).AddField(oid);
            }
            if (cls.Fields.FindField("SHAPE") < 0)
            {
                IField m_spatialField = LSGISHelper.FieldHelper.CreateGeometryField(esriGeometryType.esriGeometryPolygon
                    , new UnknownCoordinateSystemClass());
                (cls.Fields as IFieldsEdit).AddField(m_spatialField);
            }
            if (cls.Fields.FindField("RASTER") < 0)
            {
                IField m_spatialField = LSGISHelper.FieldHelper.CreateRasterField("RASTER", "影像",
                    new UnknownCoordinateSystemClass());
              
                (cls.Fields as IFieldsEdit).AddField(m_spatialField);
            }
            #endregion
            m_fldControl = new DataTableFieldControl();
            this.m_fldControl.Dock = DockStyle.Fill;
            this.m_fldControl.Fields = cls.Fields;
            this.m_fldControl.Workspace = this.Workspace;
            this.panelControl2.Controls.Add(this.m_fldControl);

        
        }
        private DataTableFieldControl m_fldControl;
        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sName = this.txtName.Text.Trim();
            if (sName == "")
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("请输入影像目录名称");
                return;
            }
            cls.Name = sName;
            cls.KeyWord = this.cboKeyword.Text.Trim();
            IField pF1 = LSGISHelper.FieldHelper.QueryField(cls.Fields, "SHAPE");
            IField pF2 = LSGISHelper.FieldHelper.QueryField(cls.Fields, "RASTER");
            pF1 = LSGISHelper.FieldHelper.AlterGeometryFieldSR(pF1, this.mSR1);
            pF2 = LSGISHelper.FieldHelper.AlterRasterFieldSR(pF2, this.mSR2);
         
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        ISpatialReference mSR1 = null;
        ISpatialReference mSR2 = null;
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
              ofd.Filter = "投影文件(.prj)|*.prj";
                    ofd.Multiselect = false;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        mSR1 = LSGISHelper.SpatialReferenceHelper.ReadSR(
                            ofd.FileName);
                        this.buttonEdit1.Text = mSR1.Name;

                    }
        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "投影文件(.prj)|*.prj";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                mSR2 = LSGISHelper.SpatialReferenceHelper.ReadSR(
                    ofd.FileName);
                this.buttonEdit2.Text = mSR2.Name;

            }
        }
    }
}
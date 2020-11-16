using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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
using DevExpress.XtraEditors;

namespace LSArcCatalog.Topology
{
    public partial class AddRule : DevExpress.XtraEditors.XtraForm
    {
        public AddRule()
        {
            InitializeComponent();
        }
        private List<IFeatureClass> pList = new List<IFeatureClass>();
        public List<IFeatureClass> GetFeatureClassList
        {
            get { return pList; }
            set { pList = value; }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pRule.SrcFeatureClass = pTable[this.cbofc1.Text.Trim()] as IFeatureClass;
            pRule.DesFeatureClass = pTable[this.cbofc2.Text.Trim()] as IFeatureClass;

            esriTopologyRuleType oType =
                (esriTopologyRuleType)Enum.Parse(typeof(esriTopologyRuleType), 
                this.cborule.Text.Trim());
            pRule.RuleType = oType;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public TopologyFeatureClassRule pRule = new TopologyFeatureClassRule();
        public Hashtable pTable = new Hashtable();
        private void AddRule_Load(object sender, EventArgs e)
        {
            foreach (IFeatureClass pfc in pList)
            {
                this.cbofc1.Properties.Items.Add((pfc as IDataset).Name);
                this.cbofc2.Properties.Items.Add((pfc as IDataset).Name);
                pTable.Add((pfc as IDataset).Name, pfc);
            }
            this.cbofc1.SelectedIndex = 0;
            this.cbofc2.SelectedIndex = 0;
            this.cborule.SelectedIndex = 0;
        }
    }
}
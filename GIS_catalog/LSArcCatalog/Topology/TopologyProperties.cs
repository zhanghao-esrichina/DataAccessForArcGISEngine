using System;
using System.Collections.Generic;
using System.Collections;
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
using DevExpress.XtraEditors;

namespace LSArcCatalog
{
    public partial class TopologyProperties : DevExpress.XtraEditors.XtraForm
    {
        public TopologyProperties()
        {
            InitializeComponent();
        }
        private ITopologyName pTName = null;
        public ITopologyName TopologyName
        {
            get { return pTName; }
            set { pTName = value; }
        }
        private ITopology pTop = null;
        private void LoadData()
        {
            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
        
            IName pName = pTName as IName;
            this.txtName.Text = pName.NameString;
            pTop = pName.Open() as ITopology;
            this.txtTol.Text = pTop.ClusterTolerance.ToString();
            this.lblStatus.Text = GetTopoStatus(pTop.State);
            ITopologyRuleContainer pRC = pTop as ITopologyRuleContainer;
            LoadTopoFeatureClass(pTop, this.listView1);
            IEnumRule pEnumRule = pRC.Rules;
            IRule pRule = pEnumRule.Next();
            ITopologyRule pTR = null;
            int i = 0;
            pRuleList.Clear();
            while (pRule != null)
            {
                pTR = pRule as ITopologyRule;
                pRuleList.Add(pTR.TopologyRuleType);
                LoadRule(pTR,i,this.listView2);
                pRule = pEnumRule.Next();
                i++;
            }
        }
        private string GetTopoStatus(esriTopologyState eState)
        {
            string ss = "";
            if (esriTopologyState.esriTSUnanalyzed == eState)
            {
                ss = "该拓扑还没有经过验证";
            }
            else if (esriTopologyState.esriTSEmpty == eState)
            {
                ss = "该拓扑为空";
            }
            else if (esriTopologyState.esriTSAnalyzedWithErrors == eState)
            {
                ss = "该拓扑经过验证存在拓扑错误";
            }
            else if (esriTopologyState.esriTSAnalyzedWithoutErrors == eState)
            {
                ss = "该拓扑经过验证没有拓扑错误";
            }
            return ss;
        }
        private List<esriTopologyRuleType> pRuleList = new List<esriTopologyRuleType>();
        private void LoadRule(ITopologyRule pRule,int i,ListView listview1)
        {
          
            string sSrcFcName = pTable[pRule.OriginClassID].ToString();
            string sDesFcName = pTable[pRule.DestinationClassID].ToString();
            string sRuleType = pRule.TopologyRuleType.ToString();
            ListViewItem lv = new ListViewItem();
            lv.SubItems.Add(sSrcFcName);
            lv.SubItems.Add(sRuleType);
            lv.SubItems.Add(sDesFcName);
            
            if (i% 2 == 0)
            {
                lv.BackColor = System.Drawing.Color.GreenYellow;
            }
            listview1.Items.Add(lv);
        }
        private Hashtable pTable = new Hashtable();
        private void LoadTopoFeatureClass(ITopology pTop, ListView listview1)
        {
            ITopologyProperties pTopP = pTop as ITopologyProperties;
            IEnumFeatureClass pEnumFc = pTopP.Classes;
            IFeatureClass pfc = pEnumFc.Next();
            int i = 0;
            pTable.Clear();
            string sName = "";
            while (pfc != null)
            {
                ITopologyClass pTc=pfc as ITopologyClass;
                this.txtRank.Text = pTc.XYRank.ToString();
                ListViewItem lv = new ListViewItem();
                sName = (pfc as IDataset).Name;
                pTable.Add(pfc.FeatureClassID, sName);
                lv.SubItems.Add(sName);
                lv.SubItems.Add(pTc.XYRank.ToString());
                lv.SubItems.Add(pTc.ZRank.ToString());
                lv.SubItems.Add(pTc.Weight.ToString());
                if (i % 2 == 0)
                {
                    lv.BackColor = System.Drawing.Color.GreenYellow;
                }
                listview1.Items.Add(lv);
                i++;
                pfc = pEnumFc.Next();
            }
        }
     
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TopologyProperties_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        //产生错误概要
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.listView3.Items.Clear();
            if ((pTop.State == esriTopologyState.esriTSUnanalyzed) ||
                (pTop.State == esriTopologyState.esriTSEmpty))
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("该拓扑为空或者未进行拓扑验证");
                return;
            }
            IErrorFeatureContainer pErrorFCon = pTop as IErrorFeatureContainer;
            IGeoDataset pGDS=pTop.FeatureDataset as IGeoDataset ;
            if(pGDS ==null) return ;
           // List<TopologyErrorSummary> pList = new List<TopologyErrorSummary>();
            for (int i = 0; i < pRuleList.Count; i++)
            {
                
                esriTopologyRuleType eType = pRuleList[i];
                IEnumTopologyErrorFeature pEnumTopErrorFea = pErrorFCon.get_ErrorFeaturesByRuleType(
                    pGDS.SpatialReference, eType, pGDS.Extent, true, true);
                ITopologyErrorFeature pTopErrorFea = pEnumTopErrorFea.Next();
                int ErrorNum = 0;
                int ExpNum = 0;
                string sSrcFc="";
                string sDesFc="";
                int j=0;
                while (pTopErrorFea != null)
                {
                    if (pTopErrorFea.IsException == true)
                    {
                        ExpNum++;
                    }
                    else
                    {
                        ErrorNum++;
                    }
                 
                    pTopErrorFea = pEnumTopErrorFea.Next();
                }
                //TopologyErrorSummary cls = new TopologyErrorSummary();
                //cls.RuleType = eType.ToString();
                //cls.ErrorNum = ErrorNum;
                //cls.ExpNum = ExpNum;
                //cls.SrcFeatureClass = sSrcFc;
                //cls.DesFeatureClass = sDesFc;
                ListViewItem lv = new ListViewItem();
                string sS = eType.ToString();
                lv.SubItems.Add(sS);
                lv.SubItems.Add(ErrorNum.ToString());
                lv.SubItems.Add(ExpNum.ToString());
                if (i % 2 == 0)
                {
                    lv.BackColor = System.Drawing.Color.GreenYellow;
                }
                this.listView3.Items.Add(lv);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //LSCommonHelper.ExportHelper.ExportListViewToExcel(
            //    "拓扑报告", DateTime.Now.ToLongTimeString(), this.listView3);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
   
}
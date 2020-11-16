using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using LSCommonHelper;
using LSGISHelper;
using ESRI.ArcGIS.DataSourcesGDB;
using DevExpress.XtraEditors;

namespace LSArcCatalog.Topology
{
    public partial class TopologyWizard : DevExpress.XtraEditors.XtraForm
    {
        public TopologyWizard()
        {
            InitializeComponent();
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }
        private IDatasetName pDN = null;
        public IDatasetName DatasetName
        {
            get { return pDN; }
            set { pDN = value; }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 1)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 0;
                this.simpleButton1.Enabled = false;
            }
            else if (this.xtraTabControl1.SelectedTabPageIndex == 2)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 1;
                this.simpleButton1.Enabled = true;
            }
            else if (this.xtraTabControl1.SelectedTabPageIndex == 3)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 2;
                this.simpleButton1.Enabled = true;
            }
            else if (this.xtraTabControl1.SelectedTabPageIndex == 4)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 3;
                this.simpleButton1.Enabled = true;
            }
            else if (this.xtraTabControl1.SelectedTabPageIndex == 5)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 4;
                this.simpleButton1.Enabled = true;
            }
            else if (this.xtraTabControl1.SelectedTabPageIndex == 6)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 5;
                this.simpleButton1.Enabled = true;
            }
        }
        private IDataset pDS = null;
        private HasTopologyClass pHasToplogyClass = null;
        private void TopologyWizard_Load(object sender, EventArgs e)
        {
            this.simpleButton1.Enabled = false;

            pHasToplogyClass = LoadHasTopology();
        }
        private HasTopologyClass LoadHasTopology()
        {
            if (pDN == null) return null;
            pDS = (pDN as IName).Open() as IDataset;
            IEnumDatasetName childNames = pDN.SubsetNames;
            IDatasetName childName = childNames.Next();
            HasTopologyClass hascls = new HasTopologyClass();
            List<string> pName = new List<string>();
            List<IFeatureClass> pTopFc = new List<IFeatureClass>();
            List<IFeatureClass> pTopHasNotFc = new List<IFeatureClass>();
            ITopologyName pTopName = null;
            while (childName != null)
            {
                if (childName is ITopologyName)
                {
                    pTopName = childName as ITopologyName;
                    pName.Add((pTopName as IName).NameString);
                    ITopology pTop = (pTopName as IName).Open() as ITopology;
                    ITopologyProperties pTopP = pTop as ITopologyProperties;
                    IEnumFeatureClass pEnumFc = pTopP.Classes;
                    IFeatureClass pfc = pEnumFc.Next();
                    while (pfc != null)
                    {
                        if (!pTopFc.Contains(pfc))
                        {
                            pTopFc.Add(pfc);
                        }
                      
                        pfc = pEnumFc.Next();
                    }
                }
               
                childName = childNames.Next();
            }
            childNames = pDN.SubsetNames;
            childName = childNames.Next();
            while (childName != null)
            {
                if (childName is IFeatureClassName )
                {
                    IFeatureClassName pFCName = childName as IFeatureClassName;
                    IFeatureClass pfc = (pFCName as IName).Open() as IFeatureClass;
                    if (!CheckHasTopologyFeatureClass(pTopFc,pfc))
                    {
                        pTopHasNotFc.Add(pfc);
                    }
                }
                childName = childNames.Next();
            }
            hascls.TopologyNameList = pName;
            hascls.TopologyFeatureClassList = pTopFc;
            hascls.TopologyHasNotFeatureClassList = pTopHasNotFc;
            return hascls;
        }
        private double GetTolerance()
        {
            ISpatialReference sr = (pDS as IGeoDataset).SpatialReference;
            double fx = 0, fy = 0, uxy = 0.001;
            try
            {
                if (sr != null)
                    sr.GetFalseOriginAndUnits(out fx, out fy, out uxy);
            }
            catch (Exception ex)
            {
                return 0.0;
            }
            uxy = 2 / uxy;
            return uxy;
        }
        private TopologyClass pTopologyClass = new TopologyClass();
        private double pTolerance = 0.00;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
            #region 1
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 1;
                this.simpleButton1.Enabled = true;
                this.txtTopName.Text = LSCommonHelper.OtherHelper.GetRightName(pDS.Name,".")+"_topology";
                pTolerance = GetTolerance();
                this.txtTopTol.Text = pTolerance.ToString();
            }
            #endregion
            #region 2
            else if (this.xtraTabControl1.SelectedTabPageIndex == 1)
            {
               
                string sName = this.txtTopName.Text.Trim();
                if (sName == "")
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("拓扑名称为空");
                    return;
                }
                if (CheckHasTopologyName(sName))
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("拓扑名称已存在");
                    return;
                }
                else
                {
                    pTopologyClass.TopologyName = sName;
                }
                double pTop = LSCommonHelper.ConvertHelper.ObjectToDouble(this.txtTopTol.Text.Trim());
                if (pTop < pTolerance)
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("拓扑容差不能小于数据集XY容差");
                    return;
                }
                else
                {
                    pTopologyClass.TopologyTolerance = pTop;
                }
                this.xtraTabControl1.SelectedTabPageIndex = 2;
                this.simpleButton1.Enabled = true;
                this.listView2.Items.Clear();    
                foreach (IFeatureClass pfc in pHasToplogyClass.TopologyHasNotFeatureClassList)
                {
                    ListViewItem lv = new ListViewItem();
                    lv.Tag = pfc;
                    lv.SubItems.Add((pfc as IDataset).Name);
                    this.listView2.Items.Add(lv);
                }
            }
#endregion
#region 3
            else if (this.xtraTabControl1.SelectedTabPageIndex == 2)
            {
                this.listView3.Items.Clear();
                string sRank = this.cboRank.Text;
                foreach (ListViewItem lv in this.listView2.Items)
                {
                    ListViewItem lvv = new ListViewItem();
                    lvv.SubItems.Add(lv.SubItems[1].Text);
                    lvv.SubItems.Add(sRank);
                    lvv.Tag = lv.Tag;
                    this.listView3.Items.Add(lvv);
                }
                this.xtraTabControl1.SelectedTabPageIndex = 3;
                this.simpleButton1.Enabled = true;
                
            }
#endregion
            #region 4
            else if (this.xtraTabControl1.SelectedTabPageIndex == 3)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 4;
                this.simpleButton1.Enabled = true;
            }
            #endregion
            #region 5
            else if (this.xtraTabControl1.SelectedTabPageIndex == 4)
            {
                if (pRuleList.Count > 0)
                {
                    pTopologyClass.TopologyFeatureClassRule = pRuleList;

                    this.listBoxControl1.Items.Clear();
                    this.listBoxControl1.Items.Add("拓扑名称：");
                    this.listBoxControl1.Items.Add(pTopologyClass.TopologyName);
                    this.listBoxControl1.Items.Add("**************************************");
                    this.listBoxControl1.Items.Add("拓扑容差：");
                    this.listBoxControl1.Items.Add(pTopologyClass.TopologyTolerance);

                    TopologyFeatureClassRule pRule = null;
                    for (int i = 0; i < pTopologyClass.TopologyFeatureClassRule.Count; i++)
                    {
                        this.listBoxControl1.Items.Add("**************************************");
                        pRule = pTopologyClass.TopologyFeatureClassRule[i];
                        this.listBoxControl1.Items.Add("拓扑规则" + (i + 1));
                        this.listBoxControl1.Items.Add("源要素类");
                        this.listBoxControl1.Items.Add((pRule.SrcFeatureClass as IDataset).Name);
                        this.listBoxControl1.Items.Add("源要素类");
                        this.listBoxControl1.Items.Add((pRule.RuleType.ToString()));
                        this.listBoxControl1.Items.Add("目的要素类");
                        this.listBoxControl1.Items.Add((pRule.DesFeatureClass as IDataset).Name);
                    }
                    this.simpleButton2.Text = "完成";
                    this.xtraTabControl1.SelectedTabPageIndex = 5;
                    this.simpleButton1.Enabled = true;
                }
                else
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("请输入参与拓扑的要素类与规则");
                    return;
                }
            }
            #endregion
            #region 6
            else if (this.xtraTabControl1.SelectedTabPageIndex == 5)
            {
                  DialogResult dr = MessageBox.Show("是否创建拓扑", "提示"
    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                  if (DialogResult.Yes == dr)
                  {
                      CreateTopology(pTopologyClass, pDS);
                  }
                  this.Close();
            }
            #endregion
            
        }
      
        private bool CheckHasTopologyName(string sName)
        {
            List<string> pNames = pHasToplogyClass.TopologyNameList;
            foreach (string str in pNames)
            {
                if (str.ToLower()==sName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckHasTopologyFeatureClass( List<IFeatureClass > pfcNames,
            IFeatureClass pfc)
        {
            
            IFeatureClass fc = null;
            for (int i = 0; i < pfcNames.Count; i++)
            {
                fc = pfcNames[i];
                if (pfc.FeatureClassID == fc.FeatureClassID)
                {
                    return true;
                }
            }
            return false;
        }
        #region 全选
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (this.listView2.Items.Count > 0)
            {
                foreach (ListViewItem lv in this.listView2.Items)
                {
                        lv.Checked = true;
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (this.listView2.Items.Count > 0)
            {
                foreach (ListViewItem lv in this.listView2.Items)
                {
                    lv.Checked = false;
                }
            }
        }
        #endregion
        #region 规则
        private List<TopologyFeatureClassRule> pRuleList = new List<TopologyFeatureClassRule>();
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            AddRule frm = new AddRule();   
            frm.GetFeatureClassList = pHasToplogyClass.TopologyHasNotFeatureClassList;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                pRuleList.Add(frm.pRule);
                ListViewItem lv = new ListViewItem();
                lv.SubItems.Add((frm.pRule.SrcFeatureClass as IDataset).Name);
                lv.SubItems.Add((frm.pRule.RuleType.ToString()));
                lv.SubItems.Add((frm.pRule.DesFeatureClass as IDataset).Name);
                this.listView1.Items.Add(lv);
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count > 0)
            {
                if (this.listView1.SelectedItems .Count==0) return;
                if (this.listView1.SelectedItems[0] != null)
                {
                    
                    this.listView1.Items.Remove(this.listView1.SelectedItems[0]);
                }
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            pRuleList.Clear();
        }
        #endregion

        public  void CreateTopology(TopologyClass pCls, IDataset pDS)
        {
            try
            {
                ITopologyContainer pTopC = pDS as ITopologyContainer;
                // IFeatureClassContainer pFCC = (IFeatureClassContainer)pDS;
                ITopology top = pTopC.CreateTopology(pCls.TopologyName, pCls.TopologyTolerance, -1, "");
                IFeatureClass pfc = null;
                int pRank = 0;
                for (int i = 0; i < this.listView3.Items.Count; i++)
                {
                    pfc = this.listView3.Items[i].Tag as IFeatureClass;
                    pRank = LSCommonHelper.ConvertHelper.ObjectToInt(this.listView3.Items[i].SubItems[2].Text);
                    top.AddClass(pfc as IFeatureClass, 5, pRank, pRank, false);
                }
                ITopologyRuleContainer topologyRuleContainer;
                ITopologyRule topologyRule;
                IFeatureClass pOriginFC;//源要素类
                IFeatureClass pDestinationFC;//目标要素类
                foreach (TopologyFeatureClassRule pRule in pCls.TopologyFeatureClassRule)
                {
                    topologyRuleContainer = (ITopologyRuleContainer)top;
                    topologyRule = new TopologyRuleClass();
                    pOriginFC = pRule.SrcFeatureClass;
                    pDestinationFC = pRule.DesFeatureClass;
                    topologyRule.TopologyRuleType = pRule.RuleType;
                    topologyRule.OriginClassID = pOriginFC.ObjectClassID;
                    topologyRule.DestinationClassID = pDestinationFC.ObjectClassID;
                    topologyRule.AllOriginSubtypes = true;
                    topologyRule.AllDestinationSubtypes = true;
                    // Add the rule to the Topology    
                    if (topologyRuleContainer.get_CanAddRule(topologyRule))
                    {
                        topologyRuleContainer.AddRule(topologyRule);
                    }
                }
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("创建成功");
            }
            catch (Exception ex)
            { }
        }
    }
}
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
using ESRI.ArcGIS.DataSourcesRaster;
namespace LSArcCatalog
{
    public partial class ArcCatalog : Form, TaskMonitor
    {
        public ArcCatalog()
        {
            InitializeComponent();
            sAccessConn=LSCommonHelper.DataBaseHelper.GetAccess2007ConnectionString(
                Application.StartupPath+@"\data\gis.accdb");
            LSCommonHelper.DataBaseHelper.AccessConnectionString = sAccessConn;
            LSCommonHelper.DataBaseHelper.Connstring = sAccessConn;
            LSCommonHelper.ControlStyleHelper.InitListViewStyle(this.listView1,false);
            LoadTreeView();
            LoadArcToolbox();
            this.dockPanel1.Text = "数据连接";
        }
        string sAccessConn = "";
        public class ConnectProperty
        {
            string sServer = "";
            public string Server
            {
                get { return sServer; }
                set { sServer = value; }
            }
            string sPort = "";
            public string Port
            {
                get { return sPort; }
                set { sPort = value; }
            }
            string sUser = "";
            public string User
            {
                get { return sUser; }
                set { sUser = value; }
            }
            string sPassword = "";
            public string Password
            {
                get { return sPassword; }
                set { sPassword = value; }
            }
        }
        private List<ConnectProperty>  GetConnectPropertyList()
        {
            List<ConnectProperty> pList = new List<ConnectProperty>();
            string sql = "select * from dataconnect";
            DataSet ds = LSCommonHelper.DataBaseHelper.getDataSet(sql, "dataconnect");
            if (ds.Tables["dataconnect"].Rows.Count > 0)
            {
                DataRow[] drr = ds.Tables["dataconnect"].Select();
                foreach (DataRow dr in drr)
                {
                    ConnectProperty cls = new ConnectProperty();
                    cls.Server = dr[0].ToString();
                    cls.Port = dr[1].ToString();
                    cls.User = dr[2].ToString();
                    cls.Password = dr[3].ToString();
                    pList.Add(cls);
                }
            }
            //ConnectProperty cls = new ConnectProperty();
            //cls.Server = "192.168.220.165";
            //cls.Port = "5151";
            //cls.User = "sde";
            //cls.Password = "sde";
            //pList.Add(cls);
            return pList;
        }

        private TaskMonitor m_appTaskMonitor;
        public TaskMonitor ApplicationTaskMonitor
        {
            get
            {
                if (this.m_appTaskMonitor == null)
                    this.m_appTaskMonitor = new NullTaskMonitor();
                return this.m_appTaskMonitor;
            }
            set
            {
                this.m_appTaskMonitor = value;
            }
        }

        #region TaskMonitor 成员
        private bool m_taskCanCanceld = false;
        private bool m_taskCanceld = false;
        private Timer mTaskTimer;
        public bool CanCanceld
        {
            get
            {
                return this.m_taskCanCanceld;
            }
            set
            {
                this.m_taskCanCanceld = value;
            }
        }

        public bool Canceld
        {
            get
            {
                return this.m_taskCanceld;
            }
            set
            {
                this.m_taskCanceld = value;
            }
        }

        public string TaskCaption
        {
            get
            {
                return this.spComment.Caption;
            }
            set
            {
                if (value == null) value = "";
                this.spComment.Caption = value;
                Application.DoEvents();
            }
        }

        public void ExitWaitState()
        {
            if (this.mTaskTimer != null)
                this.mTaskTimer.Stop();
            this.Cursor = Cursors.Default;
            this.PutTaskInfo("欢迎使用ArcCatalog系统", 0);
        }

        public void PutTaskInfo(string pCaption, int pProgress)
        {
            if (pCaption == null) pCaption = "";
            this.spComment.Caption = pCaption;
            this.spProgress.EditValue = this.ProcessTaskRate(pProgress);
            Application.DoEvents();

        }
        private int ProcessTaskRate(int pRate)
        {
            while (pRate > 100) pRate -= 100;
            while (pRate < 0) pRate += 100;
            return pRate;
        }
        public int TaskProgress
        {
            get
            {
                return (int)this.spProgress.EditValue;
            }
            set
            {
                this.ProcessTaskRate(value);
                this.spProgress.EditValue = value;
                Application.DoEvents();
            }
        }

        public void EnterWaitState()
        {
            this.EnterWaitState(false);
        }
        public void EnterWaitState(bool pUnlimited)
        {
            this.ExitWaitState();
            if (pUnlimited)
            {
                if (this.mTaskTimer == null)
                {
                    this.mTaskTimer = new Timer();
                    this.mTaskTimer.Interval = 1000;
                    this.mTaskTimer.Tick += new EventHandler(mTaskTimer_Tick);
                }
                this.mTaskTimer.Start();
            }
            this.Cursor = Cursors.WaitCursor;
        }

        void mTaskTimer_Tick(object sender, EventArgs e)
        {
            this.TaskProgress += 10;
        }
        #endregion
        private void LoadTreeView()
        {
            List<ConnectProperty> pList = GetConnectPropertyList();
            this.myTreeView1.Nodes.Clear();
        
            TreeNode pRootNode = new TreeNode();
            pRootNode.Text = "ArcSDE数据连接";
          
            this.myTreeView1.Nodes.Add(pRootNode);
            string sNodeName = "";
            TreeNode pRoot2Node = new TreeNode();
            pRoot2Node.Text = "添加数据连接";
            pRoot2Node.ImageIndex = 13;
            this.myTreeView1.Nodes[0].Nodes.Add(pRoot2Node);
            for (int i = 0; i < pList.Count;i++ )
            {
                ConnectProperty cls = pList[i];
                sNodeName = "数据连接到：" + cls.Server+"_"+cls.User;
                TreeNode pChildNode = new TreeNode(sNodeName);
                pChildNode.ImageIndex = 1;
          
                pChildNode.Tag = cls;
                this.myTreeView1.Nodes[0].Nodes.Add(pChildNode);
            }
            this.myTreeView1.ExpandAll();

        }

        private void LoadArcToolbox()
        {
            TreeNode pRootNode = new TreeNode();
            pRootNode.Text = "ArcToolbox";
            pRootNode.ImageIndex = 0;
            this.myTreeView2.Nodes.Add(pRootNode);
            List<string> pChildNodeList = new List<string>();
                pChildNodeList.Add("3D Analyst Tools");
                pChildNodeList.Add("Analysis Tools");
                pChildNodeList.Add("Conversion Tools");
                pChildNodeList.Add("Data Management Tools");
                pChildNodeList.Add("Editing Tools");
                pChildNodeList.Add("Spatial Analyst Tools");
                pChildNodeList.Add("Spatial Statistics Tools");
                TreeNode pChildNode = null;
                   foreach (string str in pChildNodeList)
                   {
                       pChildNode = new TreeNode();
                       pChildNode.Text = str;
                       pChildNode.ImageIndex = 1;
                       this.myTreeView2.Nodes[0].Nodes.Add(pChildNode);
                   }
                   pChildNode = this.myTreeView2.Nodes[0].Nodes[3];
                   List<string> pChildChildNodeList = new List<string>();
                   pChildChildNodeList.Add("Data Comparison");
                   pChildChildNodeList.Add("Database");
                   pChildChildNodeList.Add("Distributed Geodatabase");
                   pChildChildNodeList.Add("Domains");
                   pChildChildNodeList.Add("Feature Class");
                   pChildChildNodeList.Add("Features");
                   pChildChildNodeList.Add("Fields");
                   pChildChildNodeList.Add("File Geodatabase");
                   pChildChildNodeList.Add("General");
                   pChildChildNodeList.Add("Generalization");
                   pChildChildNodeList.Add("Graph");
                   pChildChildNodeList.Add("Indexes");
                   pChildChildNodeList.Add("Joins");
                   pChildChildNodeList.Add("Layers and Table Views");
                   pChildChildNodeList.Add("Package");
                   pChildChildNodeList.Add("Projections and Transformations");
                   pChildChildNodeList.Add("Raster");
                   pChildChildNodeList.Add("Relationship Classes");
                   pChildChildNodeList.Add("Subtypes");
                   pChildChildNodeList.Add("Table");
                   pChildChildNodeList.Add("Topology");
                   pChildChildNodeList.Add("Versions");
                   pChildChildNodeList.Add("Workspace");
                   TreeNode pChildChildNode = null;
                   foreach (string str in pChildChildNodeList)
                   {
                       pChildChildNode = new TreeNode();
                       pChildChildNode.Text = str;
                       pChildChildNode.ImageIndex = 2;
                       pChildNode.Nodes.Add(pChildChildNode);
                   }
                   pChildChildNode = pChildNode.Nodes[21];
                   List<string> pChildChildChildNodeList = new List<string>();
                   pChildChildChildNodeList.Add("Alter Version");
                   pChildChildChildNodeList.Add("Change Version");
                   pChildChildChildNodeList.Add("Create Version");
                   pChildChildChildNodeList.Add("Delete Version");
                   pChildChildChildNodeList.Add("Reconcile Version"); 
                    pChildChildChildNodeList.Add("Register As Versioned");
                    pChildChildChildNodeList.Add("Unregister As Versioned");
                    TreeNode pChildChildChildNode = null;
                    foreach (string str in pChildChildChildNodeList)
                    {
                        pChildChildChildNode = new TreeNode();
                        pChildChildChildNode.Text = str;
                        pChildChildChildNode.ImageIndex = 3;
                        pChildChildNode.Nodes.Add(pChildChildChildNode);
                    }

                    this.myTreeView2.ExpandAll();
        }
       
        private void LoadData(IWorkspace pWorkspace, TreeNode pNode,string userName)
        {
            if (pWorkspace == null) return;
            try
            {
                TreeNode sDSNode = new TreeNode();
                sDSNode.Text = "数据集";
                sDSNode.ImageIndex = 2;
                pNode.Nodes.Add(sDSNode);
                #region //获得数据集
                IEnumDatasetName dsNames = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                IDatasetName dsName = dsNames.Next();
                while (dsName != null)
                {
                    String dsNameStr = dsName.Name.ToUpper();
                    if (dsNameStr.StartsWith(userName))
                    {
                        TreeNode dsNode = new TreeNode(LayerHelper.GetClassShortName(dsName.Name));
                        dsNode.ImageIndex = 2;
                        dsNode.Tag = dsName;
                        sDSNode.Nodes.Add(dsNode);
                        ReadFeatureDataset(dsNode, dsName as IFeatureDatasetName);
                    }
                    dsName = dsNames.Next();
                }
                #endregion
                #region  获得要素类
                TreeNode aFCNodeParent = new TreeNode("特性类");
                aFCNodeParent.ImageIndex = 11;
                pNode.Nodes.Add(aFCNodeParent);

                IEnumDatasetName fcNames = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
                IDatasetName fcName = fcNames.Next();
                IFeatureClassName fdName = null;
                while (fcName != null)
                {
                        fdName = fcName as IFeatureClassName;
                        String fcNameStr = fcName.Name;
                        if (fcNameStr.ToUpper().StartsWith(userName))
                        {
                            
                                String fcShortName = LayerHelper.GetClassShortName(fcNameStr);
                                TreeNode fcNode = new TreeNode(fcShortName);
                                if (fdName.ShapeType == esriGeometryType.esriGeometryPolyline)
                                {
                                    fcNode.ImageIndex = 4;
                                    fcNode.Tag = fcName;
                                    aFCNodeParent.Nodes.Add(fcNode);
                                }
                                else if (fdName.ShapeType == esriGeometryType.esriGeometryPoint)
                                {
                                    fcNode.ImageIndex = 3;
                                    fcNode.Tag = fcName;
                                    aFCNodeParent.Nodes.Add(fcNode);
                                }
                                else if (fdName.ShapeType == esriGeometryType.esriGeometryPolygon)
                                {
                                    fcNode.ImageIndex = 5;
                                    fcNode.Tag = fcName;
                                    aFCNodeParent.Nodes.Add(fcNode);
                                }
                                else
                                {
                                    
                                }
                        }
                    
                    fcName = fcNames.Next();
                }
                #endregion
                #region  获得普通表
                TreeNode aFTNodeParent = new TreeNode("特性表");
                aFTNodeParent.ImageIndex = 9;
                pNode.Nodes.Add(aFTNodeParent);
                IEnumDatasetName dtNames = pWorkspace.get_DatasetNames(esriDatasetType.esriDTTable);
                IDatasetName dtName = dtNames.Next();
                while (dtName != null)
                {
                    String dtNameStr = dtName.Name;
                    if (dtNameStr.ToUpper().StartsWith(userName))
                    {
                        String dtShortName = LayerHelper.GetClassShortName(dtNameStr);
                        TreeNode dtNode = new TreeNode(dtShortName);
                        dtNode.ImageIndex = 9;
                        dtNode.Tag = dtName;
                        aFTNodeParent.Nodes.Add(dtNode);
                    }
                    dtName = dtNames.Next();
                }
                #endregion
                #region 获取RasterCatalog
                TreeNode aRCNodeParent = new TreeNode("影像目录");
                aRCNodeParent.ImageIndex = 7;
                pNode.Nodes.Add(aRCNodeParent);
                IEnumDatasetName drcNames = pWorkspace.get_DatasetNames(esriDatasetType.esriDTRasterCatalog);
                IDatasetName drcName = drcNames.Next();
                while (drcName != null)
                {
                    String drcNameStr = drcName.Name;
                    if (drcNameStr.ToUpper().StartsWith(userName))
                    {
                        String drcShortName = LayerHelper.GetClassShortName(drcNameStr);
                        TreeNode drcNode = new TreeNode(drcShortName);
                        drcNode.ImageIndex = 7;
                        drcNode.Tag = drcName;
                        aRCNodeParent.Nodes.Add(drcNode);
                    }
                    drcName = drcNames.Next();
                }
                #endregion
                #region 获取RasterDataset
                TreeNode aRDNodeParent = new TreeNode("影像数据集");
                aRDNodeParent.ImageIndex = 8;
                pNode.Nodes.Add(aRDNodeParent);

                IEnumDatasetName drdNames = pWorkspace.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
                IDatasetName drdName = drdNames.Next();
                while (drdName != null)
                {
                    String drcNameStr = drdName.Name;
                    if (drcNameStr.ToUpper().StartsWith(userName))
                    {
                        String drcShortName = LayerHelper.GetClassShortName(drcNameStr);
                        TreeNode drcNode = new TreeNode(drcShortName);
                        drcNode.ImageIndex = 8;
                        drcNode.Tag = drdName;
                        aRDNodeParent.Nodes.Add(drcNode);
                    }
                    drdName = drdNames.Next();
                }
                #endregion
                #region 获取MosicDataset
                TreeNode aMDNodeParent = new TreeNode("镶嵌数据集");
                aMDNodeParent.ImageIndex = 6;
                pNode.Nodes.Add(aMDNodeParent);
                IEnumDatasetName dmdNames = pWorkspace.get_DatasetNames(esriDatasetType.esriDTMosaicDataset);
                IDatasetName dmdName = dmdNames.Next();
                while (dmdName != null)
                {
                    String drcNameStr = dmdName.Name;
                    if (drcNameStr.ToUpper().StartsWith(userName))
                    {
                        String drcShortName = LayerHelper.GetClassShortName(drcNameStr);
                        TreeNode drcNode = new TreeNode(drcShortName);
                        drcNode.ImageIndex = 6;
                        drcNode.Tag = dmdName;
                        aMDNodeParent.Nodes.Add(drcNode);
                    }
                    dmdName = dmdNames.Next();
                }
                #endregion
            }
            catch (Exception ex)
            { }
        }
        private void ReadFeatureDataset(TreeNode pTreeNode, IFeatureDatasetName pName)
        {
            pTreeNode.Nodes.Clear();
            IEnumDatasetName childNames = (pName as IDatasetName).SubsetNames;
            IDatasetName childName = childNames.Next();
            IFeatureClassName fdName = null;
            IName aName = null;
            IFeatureClass pFc = null;
            while (childName != null)
            {
                String childNameStr = childName.Name;
                String childShortName = LayerHelper.GetClassShortName(childNameStr);
                TreeNode childNode = new TreeNode(childShortName);
                if (childName is ITopologyName)
                {
                    childNode.ImageIndex = 19;
                }
                else if (childName is IGeometricNetworkName)
                {
                    childNode.ImageIndex = 20;
                }
                else if (childName is INetworkDatasetName )
                {
                    childNode.ImageIndex = 15;
                }
                else if (childName is IRelationshipClassName)
                {
                    childNode.ImageIndex = 16;
                }
                else
                {
                    fdName = childName as IFeatureClassName;
                    aName = fdName as IName;
                    pFc = aName.Open() as IFeatureClass;
                    if (pFc.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        childNode.ImageIndex = 14;
                    }
                    else
                    {
                        if (pFc.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            childNode.ImageIndex = 4;
                        }
                        else if (pFc.ShapeType == esriGeometryType.esriGeometryPoint)
                        {
                            childNode.ImageIndex = 3;
                        }
                        else if (pFc.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            childNode.ImageIndex = 5;
                        }
                    }
                   
                }
              
                childNode.Tag = childName;
                pTreeNode.Nodes.Add(childNode);
                childName = childNames.Next();
            }
        }
        //打开要素类
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode pNode=this.myTreeView1.SelectedNode;
            if(pNode==null) return;
            this.Cursor = Cursors.WaitCursor;
            if (pNode.Tag is IFeatureClassName)
            {
                IFeatureClassName pFCName = pNode.Tag as IFeatureClassName;

                clsOpenClass.OpenFeatureClass(this.axMapControl1, pFCName,
                    this.listView1);
            }
            else if (pNode.Tag is ITableName)
            {
                ITableName pTName = pNode.Tag as ITableName;
                clsOpenClass.OpenTable(this.axMapControl1, pTName,
                   this.listView1);
                this.xtraTabControl1.SelectedTabPageIndex = 1;
            }
            else if (pNode.Tag is IRasterDatasetName )
            {
                IRasterDatasetName pRDName = pNode.Tag as IRasterDatasetName;
                clsOpenClass.OpenRasterDataset(this.axMapControl1, pRDName, this.listView1);
            }
            else if (pNode.Tag is IMosaicDatasetName)
            {
                IMosaicDatasetName pMDName = pNode.Tag as IMosaicDatasetName;
                clsOpenClass.OpenMosaicDataset(this.axMapControl1, pMDName, this.listView1);
            }

            this.Cursor = Cursors.Arrow;
        }

       
        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
              TreeNode pNode=this.myTreeView1.SelectedNode;
            if(pNode==null) return;
            this.Cursor = Cursors.WaitCursor;
            if (pNode.Tag is IFeatureClassName)
            {
                IFeatureClassName pFCName = pNode.Tag as IFeatureClassName;


                FeatureClassProperties frm = new FeatureClassProperties();
                frm.FeatureClassName = pFCName;
                LSCommonHelper.ControlStyleHelper.InitFormShowDialogStyle(frm);
            }
            else if (pNode.Tag is IMosaicDatasetName)
            {
            }
            else if (pNode.Tag is IRasterDatasetName )
            {
            }
            else if (pNode.Tag is IRasterCatalogName)
            {
            }
            this.Cursor = Cursors.Arrow;
        }

        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            //鼠标移动
            IPoint aPt = new PointClass();
            aPt.PutCoords(e.mapX, e.mapY);
      
            #region 显示XY坐标
            double px = Math.Round(e.mapX, 3);
            double py = Math.Round(e.mapY, 3);
            string pStr = px + "," + py;
            this.spMouseLoc.Caption = pStr;
            #endregion
            #region 显示经纬度
            ISpatialReference aSR = this.axMapControl1.Map.SpatialReference;
            if (aSR.Name.ToLower() == "unknown")//没有投影不显示经纬度
            {
                this.spLoc2.Caption = "未知坐标系";

            }
            else
            {
                IPoint jwPt = LSGISHelper.SpatialReferenceHelper.XY2JYD(this.axMapControl1.Map.SpatialReference
                , aPt);
                if (jwPt != null && !jwPt.IsEmpty)
                {
                    string xStr = LSGISHelper.SpatialReferenceHelper.FormatJWD(jwPt.X);
                    string yStr = LSGISHelper.SpatialReferenceHelper.FormatJWD(jwPt.Y);
                    this.spLoc2.Caption = xStr + "," + yStr;
                }
            }
            #endregion
        }

        private void 数据导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            this.Cursor = Cursors.WaitCursor;
            if (pNode.Tag is IFeatureClassName)
            {
                IFeatureClassName pFCName = pNode.Tag as IFeatureClassName;

                clsExportClass.ExportFeatureClass2Shapefile(pFCName);
            }
            else if (pNode.Tag is ITableName)
            {
             
            }
            else if (pNode.Tag is IRasterDatasetName)
            {
                
            }
            else if (pNode.Tag is IMosaicDatasetName)
            {
            
            }

            this.Cursor = Cursors.Arrow;
        }

        private void 数据注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
              DialogResult dr = MessageBox.Show("是否数据注册?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
              if (dr == DialogResult.Yes)
              {
                  TreeNode pNode = this.myTreeView1.SelectedNode;
                  if (pNode == null) return;
                  if (pNode.Tag is IDatasetName)
                  {
                      IDatasetName pDN = pNode.Tag as IDatasetName;
                      clsOtherClass.Register(pDN, true);
                  }
              }
        }

        private void 导出PGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IDatasetName)
            {
                clsExportClass.ExportFeatureDataset2GDB(pNode.Tag as IDatasetName,0);
            }
        }

        private void 导出FGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IDatasetName)
            {
                clsExportClass.ExportFeatureDataset2GDB(pNode.Tag as IDatasetName, 1);
            }
        }

        private void 属性ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
        
            if (pNode.Tag is ITopologyName)
            {
                ITopologyName pFCName = pNode.Tag as ITopologyName;
                TopologyProperties frm = new TopologyProperties();
                frm.TopologyName = pFCName;
                LSCommonHelper.ControlStyleHelper.InitFormShowDialogStyle(frm);
           
            }
        }

        private void 拓扑验证ToolStripMenuItem_Click(object sender, EventArgs e)
        {
              TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;

            if (pNode.Tag is ITopologyName)
            {
                ITopologyName pFCName = pNode.Tag as ITopologyName;
                IName pName = pFCName as IName;
                ITopology pTop = pName.Open() as ITopology;
                IFeatureDataset pFDS = pTop.FeatureDataset;
                IVersionedObject vo = pFDS as IVersionedObject;
                if (vo != null)
                {
                    if (vo.IsRegisteredAsVersioned)
                    {
                        LSCommonHelper.MessageBoxHelper.ShowMessageBox("该数据集已经注册版本，不能进行拓扑验证");
                        return;
                    }
                    else
                    {
                        if (pTop.State == esriTopologyState.esriTSUnanalyzed)
                        {
                            //ISchemaLock pSLock = pFDS as ISchemaLock;
                            //IEnumSchemaLockInfo pEnumSLockInfo=null;
                            //pSLock.GetCurrentSchemaLocks(out pEnumSLockInfo);
                            //ISchemaLockInfo pSLockInfo = pEnumSLockInfo.Next();
                            //if(pSLockInfo != null)
                            //{

                            //}
                            this.Cursor = Cursors.WaitCursor;
                            clsOtherClass.ValidateTopology(pTop);
                            LSCommonHelper.MessageBoxHelper.ShowMessageBox("验证完毕");
                            this.Cursor = Cursors.Arrow;
                        }
                        else if (pTop.State == esriTopologyState.esriTSEmpty)
                        {
                            LSCommonHelper.MessageBoxHelper.ShowMessageBox("拓扑为空，不能进行验证");
                        }
                        else
                        {
                            LSCommonHelper.MessageBoxHelper.ShowMessageBox("已经进行过拓扑验证");
                        }
                    }
                }
              
            }
        }

        private void 导入PGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
             TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IWorkspace)
            {
                IWorkspace pWs = pNode.Tag as IWorkspace;
                clsImportClass.ImportGDB2SDE(pWs, 0);
                RefrashWorkspace(pNode);
            }
        }

        private void 导入ShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IWorkspace)
            {
                IWorkspace pWs = pNode.Tag as IWorkspace;
                clsImportClass.ImportShapefile2SDE(pWs,this.m_appTaskMonitor,null);
                RefrashWorkspace(pNode);
            }
        }

        private void 导入FGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IWorkspace)
            {
                IWorkspace pWs = pNode.Tag as IWorkspace;
                clsImportClass.ImportGDB2SDE(pWs, 1);
                RefrashWorkspace(pNode);
            }
        }

        private void 导出ShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IDatasetName)
            {
                clsExportClass.ExportFeatureDataset2Shapefile(pNode.Tag as IDatasetName,
                    this.m_appTaskMonitor);
            }
        }

        private void ArcCatalog_Load(object sender, EventArgs e)
        {
            this.m_appTaskMonitor = this;
        }

        private void RefrashWorkspace(TreeNode pSelNode)
        {
            if (pSelNode != null)
            {
                this.Cursor = Cursors.WaitCursor;

                IWorkspace pWS = pSelNode.Tag as IWorkspace;
                pSelNode.Nodes.Clear();
                string sUser = LSCommonHelper.OtherHelper.GetRightName(pSelNode.Text, "：");
                sUser = LSCommonHelper.OtherHelper.GetRightName(sUser, "_");
                LoadData(pWS, this.myTreeView1.SelectedNode, sUser.ToUpper());
                this.Cursor = Cursors.Arrow;
            }

        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            RefrashWorkspace(pNode);
        }
        //s删除Dataset
        private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            DialogResult dr = MessageBox.Show("是否删除数据集："+pNode.Text+"?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (pNode.Tag is IDatasetName)
                {
                    IDatasetName pDName = pNode.Tag as IDatasetName;
                    IName pName = pDName as IName;
                    IDataset pDS = pName.Open() as IDataset;
                    pDS.Delete();
                    pNode.Remove();
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("删除成功");
                }
            }
        }
        //s删除featureclass
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            DialogResult dr = MessageBox.Show("是否删除要素类：" + pNode.Text + "?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (pNode.Tag is IFeatureClassName)
                {
                    IDatasetName pDName = pNode.Tag as IDatasetName;
                    IName pName = pDName as IName;
                    IFeatureClass pFc = pName.Open() as IFeatureClass;
                    IDataset pDS = pFc as IDataset;
                    pDS.Delete();
                    pNode.Remove();
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("删除成功");
                }
            }
        }
        //数据集导入Shapefile
        private void 导入ShapefileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IDatasetName )
            {
                IDatasetName pDatasetName = pNode.Tag as IDatasetName;
                IName pName = pDatasetName as IName;
                IDataset pDS = pName.Open() as IDataset;
                IWorkspace pSrcWS = pDS.Workspace;
                IFeatureDatasetName pFDN = pDatasetName as IFeatureDatasetName;
                clsImportClass.ImportShapefile2SDE(pSrcWS, this.m_appTaskMonitor, pFDN);
                TreeNode pWSNode = pNode.Parent.Parent;
                RefrashWorkspace(pWSNode);
            }
        }

        private void 数据压缩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IWorkspace)
            {
                IWorkspace pWs = pNode.Tag as IWorkspace;
                IPropertySet pSet = pWs.ConnectionProperties;
                string sUser = pSet.GetProperty("User").ToString();
                if (sUser.ToUpper() != "SDE")
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("只有SDE用户才能进行版本压缩");
                    return;
                }
                else
                {
                     DialogResult dr = MessageBox.Show("是否进行了版本的协调提交，是否压缩?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (dr == DialogResult.Yes)
                     {
                         IVersionedWorkspace pVW = pWs as IVersionedWorkspace;
                         pVW.Compress();
                         LSCommonHelper.MessageBoxHelper.ShowMessageBox("版本压缩完毕");
                     }
                }
            }
        }

        private void 创建新连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewConnectForm frm = new NewConnectForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTreeView();
            }
        }

        private void 数据反注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            if (pNode.Tag is IDatasetName)
            {
                IDatasetName pDN = pNode.Tag as IDatasetName;
                clsOtherClass.Register(pDN, false);
            }
        }
        //重命名要素类
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            IDatasetName pDN=pNode.Tag as IDatasetName;
            Rename frm = new Rename();
            frm.OldName = pDN.Name;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string sNew = frm.sNewName;
                clsOtherClass.Rename(pNode, sNew);
                pNode.Text = sNew;
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("重命名成功");
            }
        }

        private void 重命名ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            IDatasetName pDN = pNode.Tag as IDatasetName;
            Rename frm = new Rename();
            frm.OldName = pDN.Name;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string sNew = frm.sNewName;
                clsOtherClass.Rename(pNode, sNew);
                pNode.Text = sNew;
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("重命名成功");
            }
        }
        //删除Workspace
        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            DialogResult dr = MessageBox.Show("是否删除?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                 IWorkspace mWS = pNode.Tag as IWorkspace;
                 if (mWS != null)
                 {
                     string sServer = mWS.ConnectionProperties.GetProperty("server").ToString();
                     string sService = mWS.ConnectionProperties.GetProperty("instance").ToString();
                     string sUser = mWS.ConnectionProperties.GetProperty("user").ToString();
                     string sConn=LSCommonHelper.DataBaseHelper.GetAccess2007ConnectionString(
                         Application.StartupPath+@"\Data\gis.accdb");
                     LSCommonHelper.DataBaseHelper.Connstring=sConn;
                     string sql="delete * from dataconnet where server='"+sServer+"' and port='"+sService+"' and user='"+sUser+"'";
                     LSCommonHelper.DataBaseHelper.getcmd(sql);
                     pNode.Remove();
                      LSCommonHelper.MessageBoxHelper.ShowMessageBox("删除完毕");
                 }
            }
        }

        private void 连接参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            IWorkspace pWS = pNode.Tag as IWorkspace;
            NewConnectForm frm = new NewConnectForm();
            frm.Workspace = pWS;
            LSCommonHelper.ControlStyleHelper.InitFormShowDialogStyle(frm);
        }

        private void 断开连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            IWorkspace mWS = pNode.Tag as IWorkspace;
            if (mWS != null)
            {
                
                string sServer = mWS.ConnectionProperties.GetProperty("server").ToString();
                string sService = mWS.ConnectionProperties.GetProperty("instance").ToString();
                string sUser = mWS.ConnectionProperties.GetProperty("user").ToString();
                string sql = "select * from dataconnect where server='"+sServer+"' and port='"+sService+"' and user='"+sUser+"'";
                DataSet ds = LSCommonHelper.DataBaseHelper.getDataSet(sql, "dataconnect");
                ConnectProperty cls = new ConnectProperty();
                if (ds.Tables["dataconnect"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["dataconnect"].Rows[0];
                    cls.Server = dr[0].ToString();
                    cls.Port = dr[1].ToString();
                    cls.User = dr[2].ToString();
                    cls.Password = dr[3].ToString();
                }
                mWS = null;
                pNode.Tag = cls;
                pNode.Nodes.Clear();
                pNode.ImageIndex = 1;
            }
        }

        private void 创建拓扑ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //如果注册版本，不能创建拓扑
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            this.Cursor = Cursors.WaitCursor;
            IDatasetName pDN = pNode.Tag as IDatasetName;
           
              IVersionedObject vo = (pDN as IName).Open() as IVersionedObject;
              if (vo != null)
              {
                  if (vo.IsRegisteredAsVersioned)
                  {
                      LSCommonHelper.MessageBoxHelper.ShowMessageBox("该数据集已经注册版本，不能创建拓扑");
                      return;
                  }
              }

            Topology.TopologyWizard frm = new LSArcCatalog.Topology.TopologyWizard();
            frm.DatasetName = pDN;
            LSCommonHelper.ControlStyleHelper.InitFormShowDialogStyle(frm);
            this.Cursor = Cursors.Arrow;
         
            TreeNode pWSNode = pNode.Parent.Parent;
            RefrashWorkspace(pWSNode);
        }
        //删除topo
        private void 删除ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
             TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            DialogResult dr = MessageBox.Show("是否删除?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (pNode.Tag is ITopologyName)
                {
                    ITopologyName pDName = pNode.Tag as ITopologyName;
                    IName pName = pDName as IName;
                    ITopology pFc = pName.Open() as ITopology;
                    IDataset pDS = pFc as IDataset;
                    pDS.Delete();
                    pNode.Remove();
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("删除成功");
                }
            }
        }
        //创建要素类
        private void 创建要素类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.myTreeView1.SelectedNode;
            if (selNode.Tag is IFeatureDatasetName)
            {
                IFeatureDataset aDS = (selNode.Tag as IName).Open() as IFeatureDataset;
                clsCreateClass aWizard = new clsCreateClass();
                IFeatureClass aClass = aWizard.CreateFeatureClass(aDS);
                if (aClass != null)
                {
                    string className = LayerHelper.GetClassShortName(aClass as IDataset);
                    TreeNode classNode = new TreeNode(className);
                    classNode.Tag = (aClass as IDataset).FullName;
                    if (aClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        classNode.ImageIndex = 3;
                    }
                    else if (aClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        classNode.ImageIndex = 4;
                    }
                    else if (aClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        classNode.ImageIndex = 5;
                    }
                    selNode.Nodes.Add(classNode);
                }
            }
        }

        private void 新建数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.myTreeView1.SelectedNode;
            if (selNode.Tag is IWorkspace)
            {
                IWorkspace workspace = selNode.Tag as IWorkspace;
                clsCreateClass aWizard = new clsCreateClass();
                IFeatureDataset aDS = aWizard.CreateDataset(workspace);
                if (aDS != null)
                {
                    TreeNode aDSNode = new TreeNode(LayerHelper.GetClassShortName(aDS as IDataset));
                    aDSNode.Tag = aDS.FullName;
                    TreeNode aNode = selNode.Nodes[0];
                    aDSNode.ImageIndex = 2;
                    aNode.Nodes.Add(aDSNode);
                }
            }
            else
            {
                MessageBox.Show("不能获取数据集所在的Workspace!", "新建数据集"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 新建要素类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.myTreeView1.SelectedNode;
            if (selNode.Tag is IWorkspace)
            {
                IWorkspace workspace = selNode.Tag as IWorkspace;
                clsCreateClass aWizard = new clsCreateClass();
                IFeatureClass aFc= aWizard.CreateFeatureClass(workspace);
                if (aFc != null)
                {
                    TreeNode afcNode = new TreeNode(LayerHelper.GetClassShortName(aFc as IDataset));
                    afcNode.Tag = (aFc as IDataset).FullName;
                    if (aFc.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        afcNode.ImageIndex = 3;
                    }
                    else if (aFc.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        afcNode.ImageIndex = 4;
                    }
                    else if (aFc.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        afcNode.ImageIndex = 5;
                    }
                    TreeNode pNode = selNode.Nodes[1];
                    pNode.Nodes.Add(afcNode);
                }
            }
            else
            {
                MessageBox.Show("不能获要素类所在的Workspace!", "新建要素集"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 新建表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.myTreeView1.SelectedNode;
            if (selNode.Tag is IWorkspace)
            {
                IWorkspace workspace = selNode.Tag as IWorkspace;
                clsCreateClass aWizard = new clsCreateClass();
                ITable aTable = aWizard.CreateTable(workspace);
                if (aTable != null)
                {
                    TreeNode afcNode = new TreeNode(LayerHelper.GetClassShortName(aTable as IDataset));
                    afcNode.Tag = (aTable as IDataset).FullName;
                    afcNode.ImageIndex = 9;
                    TreeNode pNode = selNode.Nodes[2];
                    pNode.Nodes.Add(afcNode);
                }
            }
            else
            {
                MessageBox.Show("不能获取特性表所在的Workspace!", "新建特性表"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //dataset fenxi
        private void 分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
               DialogResult dr = MessageBox.Show("是否进行数据分析?"
              , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (dr == DialogResult.Yes)
               {
                   this.Cursor = Cursors.WaitCursor;
                   IDatasetName pDN = pNode.Tag as IDatasetName;
                   IFeatureDataset pfDS = (pDN as IName).Open() as IFeatureDataset;
                   IDatasetAnalyze pAnalyze = pfDS as IDatasetAnalyze;
                   try
                   {
                       pAnalyze.Analyze(pAnalyze.AllowableComponents);
                       LSCommonHelper.MessageBoxHelper.ShowMessageBox("分析完毕");
                   }
                   catch (Exception ex) { LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "分析失败"); }
                   this.Cursor = Cursors.Arrow;
               }
        }
        //要素类分析
        private void 分析ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            DialogResult dr = MessageBox.Show("是否进行数据分析?"
           , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                IDatasetName pDN = pNode.Tag as IDatasetName;
                IFeatureClass pfc = (pDN as IName).Open() as IFeatureClass;
                IDataset pDS = pfc as IDataset;
                IDatasetAnalyze pAnalyze = pDS as IDatasetAnalyze;
                try
                {
                    pAnalyze.Analyze(pAnalyze.AllowableComponents);
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("分析完毕");
                }
                catch (Exception ex) { LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "分析失败"); }
                this.Cursor = Cursors.Arrow;
            }
        }

        private void 启动归档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            DialogResult dr = MessageBox.Show("是否进行数据归档?"
           , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                IDatasetName pDN = pNode.Tag as IDatasetName;
                IArchivableObject pArcObject = (pDN as IName).Open() as IArchivableObject;
                try
                {
                    pArcObject.EnableArchiving(null, null, false);
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("数据归档完毕");
                }
                catch (Exception ex) { LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "数据归档失败"); }
                this.Cursor = Cursors.Arrow;
            }
        }

        private void 禁用归档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            DialogResult dr = MessageBox.Show("是否进行禁用数据归档?"
           , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                IDatasetName pDN = pNode.Tag as IDatasetName;
                IArchivableObject pArcObject = (pDN as IName).Open() as IArchivableObject;
                try
                {
                      DialogResult drr = MessageBox.Show("是否进行删除数据归档类?\n点击[是|删除]，点击[否|保留]"
           , "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                      if (drr == DialogResult.Yes)
                      {
                          pArcObject.DisableArchiving(true, false);
                      }
                      else
                      {
                          pArcObject.DisableArchiving(false, false);
                      }
                      LSCommonHelper.MessageBoxHelper.ShowMessageBox("禁用数据归档完毕");
                }
                catch (Exception ex) { LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "禁用数据归档失败"); }
                this.Cursor = Cursors.Arrow;
            
            }
        }

        private void 版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             TreeNode selNode = this.myTreeView1.SelectedNode;
             if (selNode.Tag is IWorkspace)
             {
                 IWorkspace workspace = selNode.Tag as IWorkspace;
                 VersionInfoForm frm = new VersionInfoForm();
                 frm.Workspace = workspace;
                 LSCommonHelper.ControlStyleHelper.InitFormShowDialogStyle(frm);
             }
        }

        private void 新建影像数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
              TreeNode selNode = this.myTreeView1.SelectedNode;
              if (selNode.Tag is IWorkspace)
              {
                  IWorkspace workspace = selNode.Tag as IWorkspace;
                  Raster.RasterDatasetWizard frm = new LSArcCatalog.Raster.RasterDatasetWizard();
                  frm.Workspace = workspace;
                  RasterDatasetClass cls = null;
                  if (frm.ShowDialog() == DialogResult.OK)
                  {
                      cls = frm.pRD;
                      IRasterWorkspaceEx pEx=workspace as IRasterWorkspaceEx;
                      IRasterDataset aRD = pEx.CreateRasterDataset(cls.Name,
                          cls.NumRand, cls.rstPixelType, cls.RasterStorageDef, cls.KeyWord,
                          cls.RasterDef, cls.GeometryDef);
                      if (aRD != null)
                      {
                          TreeNode afcNode = new TreeNode(LayerHelper.GetClassShortName(aRD as IDataset));
                          afcNode.Tag = (aRD as IDataset).FullName;
                          afcNode.ImageIndex =8;
                          TreeNode pNode = selNode.Nodes[4];
                          pNode.Nodes.Add(afcNode);
                      }
                  }
              }
        }

        private void 新建影像目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.myTreeView1.SelectedNode;
            if (selNode.Tag is IWorkspace)
            {
                IWorkspace workspace = selNode.Tag as IWorkspace;
                Raster.RasterCatalogWizard frm = new LSArcCatalog.Raster.RasterCatalogWizard();
                frm.Workspace = workspace;
                RasterCatalogClass cls = null;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cls = frm.cls;
                    IRasterWorkspaceEx pEx = workspace as IRasterWorkspaceEx;
                    IRasterCatalog aRC = pEx.CreateRasterCatalog(cls.Name,
                        cls.Fields, "SHAPE", "RASTER", cls.KeyWord);
                    if (aRC != null)
                    {
                        TreeNode afcNode = new TreeNode(LayerHelper.GetClassShortName(aRC as IDataset));
                        afcNode.Tag = (aRC as IDataset).FullName;
                        afcNode.ImageIndex = 7;
                        TreeNode pNode = selNode.Nodes[3];
                        pNode.Nodes.Add(afcNode);
                    }
                }
            }
        }

        private void 新建镶嵌数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.myTreeView1.SelectedNode;
            if (selNode.Tag is IWorkspace)
            {
                IWorkspace workspace = selNode.Tag as IWorkspace;
                Raster.MosaicDatasetWizard frm = new LSArcCatalog.Raster.MosaicDatasetWizard();
                frm.Workspace = workspace;
                IWorkspaceExtensionManager pWExtManger = workspace as IWorkspaceExtensionManager;
                IWorkspaceExtension pWExt = null;
                
                for (int i = 0; i < pWExtManger.ExtensionCount; i++)
                {
                    pWExt = pWExtManger.get_Extension(i);
                    if (pWExt is IMosaicWorkspaceExtension)
                    {
                        break;
                    }
                }
                IMosaicWorkspaceExtension pMosaicWExt = pWExt as IMosaicWorkspaceExtension;
                MosaicDatasetClass cls=new MosaicDatasetClass();
  
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cls=frm.cls;
                    IMosaicDataset pMD = pMosaicWExt.CreateMosaicDataset(cls.Name,
                        cls.SpatialReference, cls.CreateMosaicDatasetParameters, cls.KeyWord);
                    if (pMD != null)
                    {
                        TreeNode afcNode = new TreeNode(LayerHelper.GetClassShortName(pMD as IDataset));
                        afcNode.Tag = (pMD as IDataset).FullName;
                        afcNode.ImageIndex = 6;
                        TreeNode pNode = selNode.Nodes[5];
                        pNode.Nodes.Add(afcNode);
                    }
                   
                }
            }
        }

        private void 权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.myTreeView1.SelectedNode;
            if (pNode == null) return;
            IDatasetName pDN = pNode.Tag as IDatasetName;
            ISQLPrivilege pSql = pDN as ISQLPrivilege;
            Other.PrivilegeForm frm = new LSArcCatalog.Other.PrivilegeForm();
            frm.SQLPrivilege = pSql;
            LSCommonHelper.ControlStyleHelper.InitFormShowDialogStyle(frm);

        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.dockPanel1.Text = this.xtraTabControl2.SelectedTabPage.Text;
        }

        private void myTreeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pNode=this.myTreeView2.SelectedNode;
            this.myTreeView2.SelectedImageIndex = pNode.ImageIndex;
        }



        private void myTreeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string sSs = this.myTreeView1.SelectedNode.Text;
                if (sSs == "ArcSDE数据连接") return;
                if (sSs == "添加数据连接")
                {
                    NewConnectForm frm = new NewConnectForm();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadTreeView();
                    }
                }
                if (LSCommonHelper.OtherHelper.GetLeftName(sSs, "：") == "数据连接到")
                {
                    ConnectProperty cls = this.myTreeView1.SelectedNode.Tag as ConnectProperty;
                    IWorkspace pWS = LSGISHelper.WorkspaceHelper.GetSDEWorkspace(
                        cls.Server, cls.Port, cls.User, cls.Password, "sde.DEFAULT");
                    if (pWS == null)
                    {
                        LSCommonHelper.MessageBoxHelper.ShowMessageBox("数据连接不成功");
                        return;
                    }
                    else
                    {

                        this.myTreeView1.SelectedNode.ImageIndex = 0;
                        this.Cursor = Cursors.WaitCursor;
                        if (this.myTreeView1.SelectedNode.Nodes.Count == 0)
                        {
                            this.myTreeView1.SelectedNode.Tag = pWS;
                            LoadData(pWS, this.myTreeView1.SelectedNode, cls.User.ToUpper());
                            this.myTreeView1.SelectedNode.Expand();
                        }
                        this.Cursor = Cursors.Arrow;
                    }
                }
            }
            catch { this.Cursor = Cursors.Arrow; }
        }

        private void myTreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode selNode = this.myTreeView1.GetNodeAt(e.X, e.Y);
            if (selNode == null) return;

            if (e.Button == MouseButtons.Left)
            {//左键

            }
            else
            {
                System.Drawing.Point aPt = new System.Drawing.Point(e.X, e.Y);
                if (selNode.Tag is IFeatureClassName)
                {//

                    this.cmFeatureClass.Show(this.myTreeView1, aPt);
                }
                else if (selNode.Tag is ITableName)
                {//

                    this.cmFeatureClass.Show(this.myTreeView1, aPt);
                }
                else if (selNode.Tag is IRasterDatasetName)
                {//

                    this.cmFeatureClass.Show(this.myTreeView1, aPt);
                }
                else if (selNode.Tag is IMosaicDatasetName)
                {//

                    this.cmFeatureClass.Show(this.myTreeView1, aPt);
                }
                else if (selNode.Tag is IDatasetName)
                {//
                    IDatasetName pDName = selNode.Tag as IDatasetName;
                    if (pDName.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        #region 注册
                        IVersionedObject vo = (pDName as IName).Open() as IVersionedObject;
                        if (vo != null)
                        {

                            if (!vo.IsRegisteredAsVersioned)
                            {
                                this.数据注册ToolStripMenuItem.Enabled = true;
                                this.数据反注册ToolStripMenuItem.Enabled = false;
                                this.启动归档ToolStripMenuItem.Enabled = false;
                                this.禁用归档ToolStripMenuItem.Enabled = false;
                            }
                            else
                            {
                                this.数据注册ToolStripMenuItem.Enabled = false;
                                this.数据反注册ToolStripMenuItem.Enabled = true;
                                #region 归档
                                IArchivableObject pArcObject = (pDName as IName).Open() as IArchivableObject;
                                if (pArcObject.IsArchiving)
                                {
                                    this.启动归档ToolStripMenuItem.Enabled = false;
                                    this.禁用归档ToolStripMenuItem.Enabled = true;
                                }
                                else
                                {
                                    this.启动归档ToolStripMenuItem.Enabled = true;
                                    this.禁用归档ToolStripMenuItem.Enabled = false;
                                }
                                #endregion
                            }
                        }
                        #endregion

                        this.cmDataset.Show(this.myTreeView1, aPt);
                    }
                    else if (selNode.Tag is ITopologyName)
                    {
                        this.cmTopology.Show(this.myTreeView1, aPt);
                    }
                }
                else if (selNode.Tag is IWorkspace)
                {//
                    IWorkspace pWS = selNode.Tag as IWorkspace;
                    this.cmWorkpace.Show(this.myTreeView1, aPt);
                }
            }
        }
       

        
    
       
    }
}

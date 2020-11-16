using System;
using System.Collections.Generic;
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
using System.IO;

namespace LSArcCatalog.Topology
{
    public class TopologyHelper
    {
        
    }
    public class TopologyFeatureClassRule
    {
       
        private IFeatureClass  pSrcFc = null;
        public IFeatureClass SrcFeatureClass
        {
            get { return pSrcFc; }
            set { pSrcFc = value; }
        }
        private int pSrcFCRank = 0;
        public int SrcFeatureClassRank
        {
            get { return pSrcFCRank; }
            set { pSrcFCRank = value; }
        }
        private int pDesFCRank = 0;
        public int DesFeatureClassRank
        {
            get { return pDesFCRank; }
            set { pDesFCRank = value; }
        }
        private IFeatureClass pDesFc = null;
        public IFeatureClass DesFeatureClass
        {
            get { return pDesFc; }
            set { pDesFc = value; }
        }
        private esriTopologyRuleType pRuleType ;
        public esriTopologyRuleType RuleType
        {
            get { return pRuleType; }
            set { pRuleType = value; }
        }
    }
    public class TopologyClass
    {
        string sName = "";
        public string TopologyName
        {
            get { return sName; }
            set { sName = value; }
        }
        double sTol = 0.00;
        public double TopologyTolerance
        {
            get { return sTol; }
            set { sTol = value; }
        }
        private List<TopologyFeatureClassRule> pFRule = null;
        public List<TopologyFeatureClassRule> TopologyFeatureClassRule
        {
            get { return pFRule; }
            set { pFRule = value; }
        }
      
    }
    public class HasTopologyClass
    {
        private List<string> pTopNameList = new List<string>();
        public List<string> TopologyNameList
        {
            get { return pTopNameList; }
            set { pTopNameList = value; }
        }
        private List<IFeatureClass> pTopFcList = new List<IFeatureClass>();
        public List<IFeatureClass> TopologyFeatureClassList
        {
            get { return pTopFcList; }
            set { pTopFcList = value; }
        }
        private List<IFeatureClass> pHasNotTopFcList = new List<IFeatureClass>();
        public List<IFeatureClass> TopologyHasNotFeatureClassList
        {
            get { return pHasNotTopFcList; }
            set { pHasNotTopFcList = value; }
        }
    }
   
}

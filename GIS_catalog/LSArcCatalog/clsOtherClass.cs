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

namespace LSArcCatalog
{
    public class clsOtherClass
    {

        public static void Register(IDatasetName pDName, bool regOrUnReg)
        {
            bool userConfig = false;
            if (!regOrUnReg)
            {//取消注册需要警告
                DialogResult dr = MessageBox.Show("警告:取消数据库版本,会导致版本化以后所做的数据修改全部丢失！\n是否继续取消版本?"
                    , "取消版本", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.No == dr)
                {
                    userConfig = false;
                }
                else userConfig = true;
            }
            if (!userConfig) return ;
            try
            {
                IVersionedObject vo = (pDName as IName).Open() as IVersionedObject;
                if (vo != null)
                {
                    if (regOrUnReg)
                    {
                        if (!vo.IsRegisteredAsVersioned)
                        {
                            vo.RegisterAsVersioned(true);
                            LSCommonHelper.MessageBoxHelper.ShowMessageBox("注册完毕");
                        }
                    }
                    else
                    {
                        if (vo.IsRegisteredAsVersioned)
                        {
                            vo.RegisterAsVersioned(false);
                            LSCommonHelper.MessageBoxHelper.ShowMessageBox("反注册完毕");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("注册版本或取消版本时，请断开与该数据集的其他连接");
                return ;
            }
        }

        public static void ValidateTopology(ITopology topology)
        {
            try
            {
                ESRI.ArcGIS.Geometry.ISegmentCollection location = (ESRI.ArcGIS.Geometry.ISegmentCollection)new ESRI.ArcGIS.Geometry.PolygonClass();
                IGeoDataset geoDataset = (IGeoDataset)topology;
                // Set the rectangle of the pLocation polygon to be equal to the Topology extent       
                location.SetRectangle(geoDataset.Extent);
                ESRI.ArcGIS.Geometry.IPolygon locationPolygon = (ESRI.ArcGIS.Geometry.IPolygon)location;

                // Get the Dirty Area that covers the entire topology.       
                IPolygon polygon = topology.get_DirtyArea(locationPolygon);
                // Define the Area to validate and validate the topology 
                IEnvelope areatoValidate = polygon.Envelope;
                IEnvelope areaValidated = topology.ValidateTopology(areatoValidate);
            }
            catch (Exception ex) { LSCommonHelper.MessageBoxHelper.ShowMessageBox("查看该数据是否被其他用户独占"); }
        }

        public static void Rename(TreeNode pNode, string sNewName)
        {
            if (pNode.Tag is IFeatureClassName)
            {
                IFeatureClassName pDN = pNode.Tag as IFeatureClassName;
                IName pName = pDN as IName;
                IFeatureClass pfc = pName.Open() as IFeatureClass;
                IDataset pDS = pfc as IDataset;
                pDS.Rename(sNewName);
            }
            else if (pNode.Tag is IDatasetName)
            {
                IDatasetName pDN = pNode.Tag as IDatasetName;
                IName pName = pDN as IName;
                IDataset pDS = pName.Open() as IDataset;
                pDS.Rename(sNewName);
            }
        }
    }
}

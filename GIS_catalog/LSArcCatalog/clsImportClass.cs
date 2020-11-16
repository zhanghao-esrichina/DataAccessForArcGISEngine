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
    public  class clsImportClass
    {
        public static void ImportGDB2SDE(IWorkspace pDesWS, int flag)
        {
            IWorkspace pSrcWS = null;
            try
            {
                if (flag == 0)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "PGDB文件(.mdb)|*.mdb";
                    ofd.Multiselect = false;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string sFileName = ofd.FileName;
                        pSrcWS = LSGISHelper.WorkspaceHelper.GetAccessWorkspace(sFileName);
                    }
                }
                else
                {
                    FolderBrowserDialog fdb = new FolderBrowserDialog();
                    if (fdb.ShowDialog() == DialogResult.OK)
                    {
                        string sFileName = fdb.SelectedPath;
                        pSrcWS = LSGISHelper.WorkspaceHelper.GetFGDBWorkspace(sFileName);
                    }
                }
                if (pSrcWS != null)
                {
                    IEnumDatasetName pEnumDSName = pSrcWS.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                    IDatasetName pDName = pEnumDSName.Next();
        
                    while (pDName != null)
                    {
                        clsExportClass.ConvertFeatureDataset(pSrcWS, pDesWS, pDName.Name, pDName.Name);
               
                        pDName = pEnumDSName.Next();
                    }
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("导入成功");
                }
            }
            catch { }
        }

        public static void ImportShapefile2SDE(IWorkspace pDesWS, TaskMonitor mTaskMonitor,
            IFeatureDatasetName pFDN)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开SHP数据";
            ofd.Filter = "SHP数据(*.shp)|*.shp";
            ofd.Multiselect = true;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] sFileNames = ofd.FileNames;
                string sFileName = "";
                IFeatureClass pFC = null;

                mTaskMonitor.EnterWaitState();
                string sName = "";
                IWorkspace pSrcWS=null;
                for (int i = 0; i < sFileNames.Length; i++)
                {
                    mTaskMonitor.TaskCaption = "共" + sFileNames.Length + "个文件，先处理第" + (i + 1) + "个文件";
                    mTaskMonitor.TaskProgress = LSCommonHelper.MathHelper.Precent(0, sFileNames.Length, i);
                    sFileName = sFileNames[i].ToString();
                    pSrcWS=LSGISHelper.WorkspaceHelper.GetShapefileWorkspace(sFileName);
                   sFileName = System.IO.Path.GetFileNameWithoutExtension(sFileName);
                   IFeatureWorkspace pFWS = pSrcWS as IFeatureWorkspace;
                    pFC = pFWS.OpenFeatureClass(sFileName);
                    sName = (pFC as IDataset).Name;
                    if (ConvertFeatureClass2FeatureDataset(pSrcWS, pDesWS, sName, sName, pFDN))
                    { }
                }
                mTaskMonitor.ExitWaitState();
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("导入成功");
            }
        }
        public static bool ConvertFeatureClass2FeatureDataset(IWorkspace sourceWorkspace,
      IWorkspace targetWorkspace, string nameOfSourceFeatureClass,
      string nameOfTargetFeatureClass, IFeatureDatasetName pName)
        {
            try
            {
                //create source workspace name 
                IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;
                //create source dataset name   
                IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
                IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = nameOfSourceFeatureClass;

                //create target workspace name   
                IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;
                //create target dataset name    
                IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOfTargetFeatureClass;
                //Open input Featureclass to get field definitions.  
                ESRI.ArcGIS.esriSystem.IName sourceName = (ESRI.ArcGIS.esriSystem.IName)sourceFeatureClassName;
                IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();
                //Validate the field names because you are converting between different workspace types.   
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IFields targetFeatureClassFields;
                IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
                IEnumFieldError enumFieldError;
                // Most importantly set the input and validate workspaces! 
                fieldChecker.InputWorkspace = sourceWorkspace;
                fieldChecker.ValidateWorkspace = targetWorkspace;
                fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError,
                    out targetFeatureClassFields);
                // Loop through the output fields to find the geomerty field   
                IField geometryField;
                for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
                {
                    if (targetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        geometryField = targetFeatureClassFields.get_Field(i);
                        // Get the geometry field's geometry defenition          
                        IGeometryDef geometryDef = geometryField.GeometryDef;
                        //Give the geometry definition a spatial index grid count and grid size     
                        IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;
                        targetFCGeoDefEdit.GridCount_2 = 1;
                        targetFCGeoDefEdit.set_GridSize(0, 0);
                        //Allow ArcGIS to determine a valid grid size for the data loaded     
                        targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                        // we want to convert all of the features    
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = "";
                        // Load the feature class            
                        IFeatureDataConverter fctofc = new FeatureDataConverterClass();
                        IEnumInvalidObject enumErrors = fctofc.ConvertFeatureClass(sourceFeatureClassName,
                            queryFilter, pName, targetFeatureClassName,
                            geometryDef, targetFeatureClassFields, "", 1000, 0);
                        break;
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}

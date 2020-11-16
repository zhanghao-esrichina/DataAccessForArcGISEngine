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
namespace LSArcCatalog
{
    public class clsExportClass
    {
   
        /// <summary>
        /// 导出FeatureClass到Shapefile文件
        /// </summary>
        /// <param name="apFeatureClass"></param>
        public static bool ExportFeatureClassToShp(string sPath,IFeatureClass apFeatureClass)
        {
            try
            {
                string ExportFileShortName = System.IO.Path.GetFileNameWithoutExtension(sPath);
                if (ExportFileShortName == "")
                {
                    ExportFileShortName =LSCommonHelper.OtherHelper.GetRightName( (apFeatureClass as IDataset).Name,".");
                }
                string ExportFilePath = System.IO.Path.GetDirectoryName(sPath);
                if (ExportFilePath == null)
                {
                    ExportFilePath = sPath;
                }
                //设置导出要素类的参数
                IFeatureClassName pOutFeatureClassName = new FeatureClassNameClass();
                IDataset pOutDataset = (IDataset)apFeatureClass;
                pOutFeatureClassName = (IFeatureClassName)pOutDataset.FullName;
                //创建一个输出shp文件的工作空间
                IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                IWorkspaceName pInWorkspaceName = new WorkspaceNameClass();
                pInWorkspaceName = pShpWorkspaceFactory.Create(ExportFilePath, ExportFileShortName, null, 0);

                //创建一个要素集合
                IFeatureDatasetName pInFeatureDatasetName = null;
                //创建一个要素类
                IFeatureClassName pInFeatureClassName = new FeatureClassNameClass();
                IDatasetName pInDatasetClassName;
                pInDatasetClassName = (IDatasetName)pInFeatureClassName;
                pInDatasetClassName.Name = ExportFileShortName;//作为输出参数
                pInDatasetClassName.WorkspaceName = pInWorkspaceName;
                //通过FIELDCHECKER检查字段的合法性，为输出SHP获得字段集合
                long iCounter;
                IFields pOutFields, pInFields;
      
                IField pGeoField;
                IEnumFieldError pEnumFieldError = null;
                pInFields = apFeatureClass.Fields;
                IFieldChecker pFieldChecker = new FieldChecker();
                pFieldChecker.Validate(pInFields, out pEnumFieldError, out pOutFields);
                //通过循环查找几何字段
                pGeoField = null;
                for (iCounter = 0; iCounter < pOutFields.FieldCount; iCounter++)
                {
                    if (pOutFields.get_Field((int)iCounter).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        pGeoField = pOutFields.get_Field((int)iCounter);
                        break;
                    }
                }
                //得到几何字段的几何定义
                IGeometryDef pOutGeometryDef;
                IGeometryDefEdit pOutGeometryDefEdit;
                pOutGeometryDef = pGeoField.GeometryDef;
                //设置几何字段的空间参考和网格
                pOutGeometryDefEdit = (IGeometryDefEdit)pOutGeometryDef;
                pOutGeometryDefEdit.GridCount_2 = 1;
                pOutGeometryDefEdit.set_GridSize(0, 1500000);

                //开始导入
                IFeatureDataConverter pShpToClsConverter = new FeatureDataConverterClass();
                pShpToClsConverter.ConvertFeatureClass(pOutFeatureClassName, null, pInFeatureDatasetName, pInFeatureClassName, pOutGeometryDef, pOutFields, "", 1000, 0);
                return true;
            }
            catch (Exception ex)
            {
             return false;
            }
        }
        public static void ExportFeatureClass2Shapefile(IFeatureClassName pFcName)
        {
            
            IName pName = pFcName as IName;
            IFeatureClass pFc = pName.Open() as IFeatureClass;

            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "Shapefile文件(.shp)|*.shp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string sPath = ofd.FileName;
                if (ExportFeatureClassToShp(sPath, pFc))
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("导出成功");
                }
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("导出失败");
            }
        }

        public static bool  ConvertFeatureDataset(IWorkspace sourceWorkspace, IWorkspace targetWorkspace,
     string nameOfSourceFeatureDataset, string nameOfTargetFeatureDataset)
        {
            try
            {
                //create source workspace name  
                IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;
                //create source dataset name   
                IFeatureDatasetName sourceFeatureDatasetName = new FeatureDatasetNameClass();
                IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureDatasetName;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = nameOfSourceFeatureDataset;
                //create target workspace name   
                IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;
                //create target dataset name  
                IFeatureDatasetName targetFeatureDatasetName = new FeatureDatasetNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureDatasetName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOfTargetFeatureDataset;
                //Convert feature dataset     
                IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();
                featureDataConverter.ConvertFeatureDataset(sourceFeatureDatasetName, targetFeatureDatasetName, null, "", 1000, 0);
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        public static void ExportFeatureDataset2GDB(IDatasetName pDSName,int flag)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择保存路径";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sPath = fbd.SelectedPath;
                string sTemplate = "";
                if (flag == 0)
                {
                    sTemplate = Application.StartupPath + @"\template\pgdb.mdb";
                    File.Copy(sTemplate, sPath + "\\pgdb.mdb");
                }
                else
                {
                    sTemplate = Application.StartupPath + @"\template\fgdb.gdb";
                    FileHelper.CopyDir(sTemplate, sPath+"\\fgdb.gdb");
                }
               
                IName pName = pDSName as IName;
                string sSrcDSName = pDSName.Name;
                sSrcDSName = LSCommonHelper.OtherHelper.GetRightName(sSrcDSName, ".");
                IDataset pDS = pName.Open() as IDataset;
                IWorkspace pSrcWS = pDS.Workspace;
                IWorkspace pDesWS = null;
                if (flag == 0)
                {
                    pDesWS = LSGISHelper.WorkspaceHelper.GetAccessWorkspace(sPath + "\\pgdb.mdb");
                }
                else
                {
                    pDesWS = LSGISHelper.WorkspaceHelper.GetFGDBWorkspace(sPath + "\\fgdb.gdb");
                }
                if (ConvertFeatureDataset(pSrcWS, pDesWS, sSrcDSName, sSrcDSName))
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("导出成功");
                }
                else
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("导出失败");
                }
            }

        }

        public static void ExportFeatureDataset2Shapefile(IDatasetName pDSName,
             TaskMonitor mTaskMonitor)
        {
            
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择保存路径";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string sPath = fbd.SelectedPath;
                    IName pName = pDSName as IName;
                    IDataset pDS = pName.Open() as IDataset;
                    IFeatureDataset pFDS = pDS as IFeatureDataset;
                    IFeatureClassContainer pFCC = pFDS as IFeatureClassContainer;
                    IFeatureClass pfc = null;
                    mTaskMonitor.EnterWaitState();

                    for (int i = 0; i < pFCC.ClassCount; i++)
                    {
                        pfc = pFCC.get_Class(i);
                        mTaskMonitor.TaskCaption = "正在导出第"+(i+1)+"个"+pfc.AliasName+"图层，共"+pFCC.ClassCount+"个";
                        mTaskMonitor.TaskProgress = LSCommonHelper.MathHelper.Precent(
                            0, pFCC.ClassCount, i);
                        if (ExportFeatureClassToShp(sPath, pfc))
                        { }
                    }
                    mTaskMonitor.ExitWaitState();
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("导出完毕");
                }
        }
   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
namespace LSArcCatalog
{
   public  class clsCreateClass
    {
        public IFeatureDataset CreateDataset(IWorkspace pWorkspace)
        {
            try
            {
                if (pWorkspace == null) return null;
                IFeatureWorkspace aFeaWorkspace = pWorkspace as IFeatureWorkspace;
                if (aFeaWorkspace == null) return null;
                DatasetPropertiesForm aForm = new DatasetPropertiesForm();
                aForm.HignPrecision = LSGISHelper.WorkspaceHelper.HighPrecision(pWorkspace);
                if (aForm.ShowDialog() == DialogResult.OK)
                {
                    string dsName = aForm.FeatureDatasetName;
                    ISpatialReference aSR = aForm.SpatialReference;
                    IFeatureDataset aDS = aFeaWorkspace.CreateFeatureDataset(dsName, aSR);
                    return aDS;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public IRasterDataset CreateRasterDataset(IWorkspace pWorkspace,string sName
           )
        {
            try
            {
                IRasterWorkspaceEx pRWEx = pWorkspace as IRasterWorkspaceEx;
                IGeometryDef pGDef=new GeometryDefClass ();
               
                IRasterDataset pRD = pRWEx.CreateRasterDataset(
                    sName, 3, rstPixelType.PT_CHAR, null, null, null, null);
            }
            catch { }
            return null;
        }
        public IFeatureClass CreateFeatureClass(IWorkspace pWorkspace)
        {
            if (pWorkspace == null) return null;
            IFeatureWorkspace aFeaWorkspace = pWorkspace as IFeatureWorkspace;
            if (aFeaWorkspace == null) return null;
            IFeatureClass aClass = null;
            FeatureClassWizard aForm = new FeatureClassWizard();
            aForm.Workspace = pWorkspace;
            if (aForm.ShowDialog() == DialogResult.OK)
            {
                while (true)
                {
                    string className = aForm.FeatureClassName;
                    string aliasName = aForm.FeatureClassAliasName;
                    IFields flds = aForm.Fields;
                    try
                    {
                        aClass = aFeaWorkspace.CreateFeatureClass(className, flds
                            , null, null, esriFeatureType.esriFTSimple, "SHAPE", null);
                        if (!aliasName.Equals(""))
                        {
                            IClassSchemaEdit aClassEdit = aClass as IClassSchemaEdit;
                            if (aClassEdit != null) aClassEdit.AlterAliasName(aliasName);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show ("错误:\n"+ex.Message ,"新建特性类",
                        //    MessageBoxButtons.OK ,MessageBoxIcon.Error );
                        LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "");
                    }
                    aForm = new FeatureClassWizard();
                    aForm.Workspace = pWorkspace;
                    aForm.FeatureClassName = className;
                    aForm.FeatureClassAliasName = aliasName;
                    aForm.Fields = flds;
                    if (aForm.ShowDialog() == DialogResult.Cancel) break;
                }
            }
            return aClass;
        }
        public IFeatureClass CreateFeatureClass(IFeatureDataset pDS)
        {
            if (pDS == null) return null;
            IFeatureClass aClass = null;

            FeatureClassWizard aForm = new FeatureClassWizard();
            aForm.Workspace = (pDS as IDataset).Workspace;
            IGeoDataset pGDS = pDS as IGeoDataset;
            if (pGDS != null)
            {
                aForm.SpatialReference = pGDS.SpatialReference;
            }
            if (aForm.ShowDialog() == DialogResult.OK)
            {
                while (true)
                {
                    string className = aForm.FeatureClassName;
                    string aliasName = aForm.FeatureClassAliasName;
                    IFields flds = aForm.Fields;

                    try
                    {
                        aClass = pDS.CreateFeatureClass(className, flds
                            , null, null, esriFeatureType.esriFTSimple, "SHAPE", null);
                        if (!aliasName.Equals(""))
                        {
                            IClassSchemaEdit aClassEdit = aClass as IClassSchemaEdit;
                            if (aClassEdit != null) aClassEdit.AlterAliasName(aliasName);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "请选择高精度坐标系");
                    }
                    aForm = new FeatureClassWizard();
                    aForm.Workspace = (pDS as IDataset).Workspace;
                    aForm.FeatureClassName = className;
                    aForm.FeatureClassAliasName = aliasName;
                  

                    aForm.Fields = flds;
                    if (aForm.ShowDialog() == DialogResult.Cancel) break;
                }
            }
            return aClass;
        }
        public ITable CreateTable(IWorkspace pWorkspace)
        {
            if (pWorkspace == null) return null;
            IFeatureWorkspace aFeaWorkspace = pWorkspace as IFeatureWorkspace;
            if (aFeaWorkspace == null) return null;
            ITable aTable = null;
            DataTableWizard aWizard = new DataTableWizard();
            aWizard.Workspace = pWorkspace;
            if (aWizard.ShowDialog() == DialogResult.OK)
            {
                while (true)
                {
                    string tableName = aWizard.TableName;
                    string aliasName = aWizard.TableAliasName;
                    IFields flds = aWizard.Fields;
                    try
                    {
                        aTable = aFeaWorkspace.CreateTable(tableName, flds
                            , null, null, null);

                        if (!aliasName.Equals(""))
                        {
                            IClassSchemaEdit aClassEdit = aTable as IClassSchemaEdit;
                            aClassEdit.RegisterAsObjectClass("OBJECTID", null);
                            if (aClassEdit != null) aClassEdit.AlterAliasName(aliasName);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show ("错误:\n"+ex.Message ,"新建表",
                        //    MessageBoxButtons.OK ,MessageBoxIcon.Error );
                        LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "");
                    }
                    aWizard = new DataTableWizard();
                    aWizard.Workspace = pWorkspace;
                    aWizard.TableName = tableName;
                    aWizard.TableAliasName = aliasName;
                    aWizard.Fields = flds;
                    if (aWizard.ShowDialog() == DialogResult.Cancel) break;
                }
            }
            return aTable;
        }
    }

   public class RasterDatasetClass
   {
       private rstPixelType pPixelTye = rstPixelType.PT_CHAR;
       public rstPixelType rstPixelType
       {
           get { return pPixelTye; }
           set { pPixelTye = value; }
       }
       private IRasterDef pRDef = null;
       public IRasterDef RasterDef
       {
           get { return pRDef; }
           set { pRDef = value; }
       }
       private IGeometryDef pGeoDef = null;
       public IGeometryDef GeometryDef
       {
           get { return pGeoDef; }
           set { pGeoDef = value; }
       }
       private IRasterStorageDef pRSDef = null;
       public IRasterStorageDef RasterStorageDef
       {
           get { return pRSDef; }
           set { pRSDef = value; }
       }
       private int sNumRand = 1;
       public int NumRand
       {
           get { return sNumRand; }
           set { sNumRand = value; }
       }
       private string sName = "";
       public string Name
       {
           get { return sName; }
           set { sName = value; }
       }
       private string sKeyWord = "";
       public string KeyWord
       {
           get { return sKeyWord; }
           set { sKeyWord = value; }
       }
   }
   public class MosaicDatasetClass
   {
       private string sName = "";
       public string Name
       {
           get { return sName; }
           set { sName = value; }
       }
       private string sKeyWord = "";
       public string KeyWord
       {
           get { return sKeyWord; }
           set { sKeyWord = value; }
       }
       private ISpatialReference pSR = null;
       public ISpatialReference SpatialReference
       {
           get { return pSR; }
           set { pSR = value; }
       }
       private ICreateMosaicDatasetParameters pMDPara = new CreateMosaicDatasetParametersClass();
       public ICreateMosaicDatasetParameters CreateMosaicDatasetParameters
       {
           get { return pMDPara; }
           set { pMDPara = value; }
       }
   }
   public class RasterCatalogClass
   {
       private string sName = "";
       public string Name
       {
           get { return sName; }
           set { sName = value; }
       }
       private string sKeyWord = "";
       public string KeyWord
       {
           get { return sKeyWord; }
           set { sKeyWord = value; }
       }
       private IGeometryDef pGeoDef = null;
       public IGeometryDef GeometryDef
       {
           get { return pGeoDef; }
           set { pGeoDef = value; }
       }
       private IFields pFields = new FieldsClass();
       public IFields Fields
       {
           get { return pFields; }
           set { pFields = value; }
       }
   }
}

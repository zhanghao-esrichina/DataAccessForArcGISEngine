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
   public class clsVersionClass
    {
       public static List<IVersionInfo> GetVersionInfoList(IWorkspace pWS)
       {
           List<IVersionInfo> pList = new List<IVersionInfo>();
           IVersionedWorkspace pVW = pWS as IVersionedWorkspace;
           IEnumVersionInfo pEnumV = pVW.Versions;
           IVersionInfo pV = pEnumV.Next();
           while (pV != null)
           {
               pList.Add(pV);
               pV = pEnumV.Next();
           }
           return pList;
       }
       public static  bool CheckVersionLocks(IVersion pVersion)
       {
           IEnumLockInfo aEnumLock = pVersion.VersionLocks;
           ILockInfo aLock = aEnumLock.Next();
           StringBuilder aBuilder = new StringBuilder();

           while (aLock != null)
           {
               aBuilder.Append("数据库被" + aLock.UserName + "所锁定!");
               aLock = aEnumLock.Next();
           }
           string aLockStr = aBuilder.ToString();
           if (!aLockStr.Equals(""))
           {
               MessageBox.Show(aLockStr, "检查数据库锁定"
                   , MessageBoxButtons.OK, MessageBoxIcon.Error);
               return false;
           }
           return true;
       }
       public static bool CheckVersionName(List<string> pVersionNameList,
           string sVersionName)
       {
           string ss = "";
           foreach (string str in pVersionNameList)
           {
               ss = LSCommonHelper.OtherHelper.GetRightName(str, ".");
               if (ss.Equals(sVersionName.ToUpper()))
               {
                   return true;
               }
           }
           return false;
       }
    }
}

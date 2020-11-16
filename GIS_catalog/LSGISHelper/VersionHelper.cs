using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace LSGISHelper
{
  public  class VersionHelper
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
        public static List<string> GetVersionNameList(List<IVersionInfo> pListVI)
        {
            List<string> pList = new List<string>();
            foreach (IVersionInfo str in pListVI)
            {
                pList.Add(str.VersionName);
            }
            return pList;
        }
        public static bool CheckVersionLocks(IVersion pVersion)
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
        public static List<IVersion> GetParentVersionList(IVersion pVersion)
        {
            List<IVersion> pList = new List<IVersion>();
            if (pVersion.HasParent())
            {
                IVersionedWorkspace pVWS = pVersion as IVersionedWorkspace;

                IEnumVersionInfo pEnumVI = pVersion.VersionInfo.Ancestors;
                IVersionInfo pVI = pEnumVI.Next();
                IVersion pParentVersion = null;
                while (pVI != null)
                {
                    pParentVersion = pVWS.FindVersion(pVI.VersionName);
                    pList.Add(pParentVersion);
                    pVI = pEnumVI.Next();
                }
            }
            return pList;
        }
        public static List<string> GetParentVersionNameList(IVersion pVersion)
        {
            List<string> pList = new List<string>();
            if (pVersion.HasParent())
            {
                IEnumVersionInfo pEnumVI = pVersion.VersionInfo.Ancestors;
                IVersionInfo pVI = pEnumVI.Next();
                while (pVI != null)
                {
                    pList.Add(pVI.VersionName);
                    pVI = pEnumVI.Next();
                }
            }
            return pList;
        }
        public static IVersion GetDefaultVersion(IWorkspace pWS)
        {
            
            IVersionedWorkspace pVW = pWS as IVersionedWorkspace;

            return pVW.DefaultVersion;
        }
        public static IVersion GetVersion(IWorkspace pWS,string sVersionName,string sPws)
        {
            pWS =WorkspaceHelper.SwitchVersionWorkspace(pWS,sVersionName,sPws);
            IVersionedWorkspace pVW = pWS as IVersionedWorkspace;

            return pVW as IVersion;
            
        }
   
      /// <summary>
      /// The following complete code example finds the differences
      /// between two versions.  As previously stated, 
      /// if the objective is to find differences that might result in a conflict,
      /// open the difference table from the parent version and not the common 
      /// ancestor version (as in the following code example).
      /// </summary>
      /// <param name="workspace"></param>
      /// <param name="childVersionName"></param>
      /// <param name="parentVersionName"></param>
      /// <param name="tableName"></param>
      /// <param name="differenceType"></param>
      /// <returns></returns>
      public static IFIDSet FindVersionDifferences(IWorkspace workspace, String childVersionName,
    String parentVersionName, String tableName, esriDifferenceType differenceType)
      {
          // Get references to the child and parent versions.
          IVersionedWorkspace versionedWorkspace = (IVersionedWorkspace)workspace;
          IVersion childVersion = versionedWorkspace.FindVersion(childVersionName);
          IVersion parentVersion = versionedWorkspace.FindVersion(parentVersionName);

          // Cast to the IVersion2 interface to find the common ancestor.
          IVersion2 childVersion2 = (IVersion2)childVersion;
          IVersion commonAncestorVersion = childVersion2.GetCommonAncestor(parentVersion);

          // Cast the child version to IFeatureWorkspace and open the table.
          IFeatureWorkspace childFWS = (IFeatureWorkspace)childVersion;
          ITable childTable = childFWS.OpenTable(tableName);

          // Cast the common ancestor version to IFeatureWorkspace and open the table.
          IFeatureWorkspace commonAncestorFWS = (IFeatureWorkspace)commonAncestorVersion;
          ITable commonAncestorTable = commonAncestorFWS.OpenTable(tableName);

          // Cast to the IVersionedTable interface to create a difference cursor.
          IVersionedTable versionedTable = (IVersionedTable)childTable;
          IDifferenceCursor differenceCursor = versionedTable.Differences
              (commonAncestorTable, differenceType, null);

          // Create output variables for the IDifferenceCursor.Next method and a FID set.
          IFIDSet fidSet = new FIDSetClass();
          IRow differenceRow = null;
          int objectID = -1;

          // Step through the cursor, showing the ID of each modified row.
          differenceCursor.Next(out objectID, out differenceRow);
          while (objectID != -1)
          {
              fidSet.Add(objectID);
              differenceCursor.Next(out objectID, out differenceRow);
          }

          fidSet.Reset();
          return fidSet;
      }

      /// <summary>
      /// The following code example finds the
      /// differences between a historical and default versions of a table:
      /// </summary>
      /// <param name="workspace"></param>
      /// <param name="historicalMarkerName"></param>
      /// <param name="tableName"></param>
      /// <param name="differenceType"></param>
      /// <returns></returns>
      public IFIDSet FindHistoricalDifferences(IWorkspace workspace, String
    historicalMarkerName, String tableName, esriDifferenceType differenceType)
      {
          // Cast to the IHistoricalWorkspace interface and get the 2004 and default versions.
          IHistoricalWorkspace historicalWorkspace = (IHistoricalWorkspace)workspace;
          IHistoricalVersion defaultVersion =
              historicalWorkspace.FindHistoricalVersionByName
              (historicalWorkspace.DefaultMarkerName);
          IHistoricalVersion historicalVersion =
              historicalWorkspace.FindHistoricalVersionByName(historicalMarkerName);

          // Cast both versions to IFeatureWorkspace and open the table from each.
          IFeatureWorkspace defaultFWS = (IFeatureWorkspace)defaultVersion;
          IFeatureWorkspace historicalFWS = (IFeatureWorkspace)historicalVersion;
          ITable defaultTable = defaultFWS.OpenTable(tableName);
          ITable historicalTable = historicalFWS.OpenTable(tableName);

          // Create a difference cursor.
          IVersionedTable versionedTable = (IVersionedTable)defaultTable;
          IDifferenceCursor differenceCursor = versionedTable.Differences(historicalTable,
              differenceType, null);

          // Create output variables for the IDifferenceCursor.Next method and a FID set.
          IFIDSet fidSet = new FIDSetClass();
          IRow differenceRow = null;
          int objectID = -1;

          // Step through the cursor, showing the ID of each modified row.
          differenceCursor.Next(out objectID, out differenceRow);
          while (objectID != -1)
          {
              fidSet.Add(objectID);
              differenceCursor.Next(out objectID, out differenceRow);
          }

          fidSet.Reset();
          return fidSet;
      }

    }

    /// <summary>
  /// The following code example shows 
  /// the full implementation of the previously described event listener:
    /// </summary>
  public class EventListener
  {
      public EventListener(IVersion version)
      {
          // Cast the version to the event interface.
          IVersionEvents_Event versionEvent = (IVersionEvents_Event)version;

          // Instantiate the delegate type and add it to the event.
          versionEvent.OnReconcile += new IVersionEvents_OnReconcileEventHandler
              (OnReconcile);
      }

      public void OnReconcile(String targetVersionName, Boolean hasConflicts)
      {
          // TODO: Implement the event handling.
      }
  }

    /// <summary>
  /// By iterating through the ObjectIDs from each conflict class, 
  /// the code attempts to merge any conflicting geometries, 
  /// and removes the feature from the UpdateUpdate conflict set if successful.
    /// </summary>
  public class MergeEventListener
  {
      private IFeatureWorkspace featureWorkspace = null;

      public MergeEventListener(IVersion version)
      {
          // Save the version as a member variable.
          featureWorkspace = (IFeatureWorkspace)version;

          // Subscribe to the OnReconcile event.
          IVersionEvents_Event versionEvent = (IVersionEvents_Event)version;
          versionEvent.OnConflictsDetected += new
              IVersionEvents_OnConflictsDetectedEventHandler(OnConflictsDetected);
      }

      /// <summary>
      /// Pseudocode:
      /// - Loop through all conflict classes after the reconcile.
      /// - Loop through every UpdateUpdate conflict on the class.
      /// - Determine if geometry is in conflict on the feature.
      /// - If so, merge geometries together (handling errors) and store the feature.
      /// </summary>
      public void OnConflictsDetected(ref bool conflictsRemoved, ref bool
          errorOccurred, ref string errorString)
      {
          try
          {
              IVersionEdit4 versionEdit4 = (IVersionEdit4)featureWorkspace;

              // Get the various versions on which to output information.
              IFeatureWorkspace commonAncestorFWorkspace = (IFeatureWorkspace)
                  versionEdit4.CommonAncestorVersion;
              IFeatureWorkspace preReconcileFWorkspace = (IFeatureWorkspace)
                  versionEdit4.PreReconcileVersion;
              IFeatureWorkspace reconcileFWorkspace = (IFeatureWorkspace)
                  versionEdit4.ReconcileVersion;

              IEnumConflictClass enumConflictClass = versionEdit4.ConflictClasses;
              IConflictClass conflictClass = null;
              while ((conflictClass = enumConflictClass.Next()) != null)
              {
                  IDataset dataset = (IDataset)conflictClass;

                  // Make sure the class is a feature class.
                  if (dataset.Type == esriDatasetType.esriDTFeatureClass)
                  {
                      String datasetName = dataset.Name;
                      IFeatureClass featureClass = featureWorkspace.OpenFeatureClass
                          (datasetName);

                      Console.WriteLine("Conflicts on feature class {0}", datasetName);

                      // Get all UpdateUpdate conflicts.
                      ISelectionSet updateUpdates = conflictClass.UpdateUpdates;
                      if (updateUpdates.Count > 0)
                      {
                          // Get conflict feature classes on the three reconcile versions.
                          IFeatureClass featureClassPreReconcile =
                              preReconcileFWorkspace.OpenFeatureClass(datasetName);
                          IFeatureClass featureClassReconcile =
                              reconcileFWorkspace.OpenFeatureClass(datasetName);
                          IFeatureClass featureClassCommonAncestor =
                              commonAncestorFWorkspace.OpenFeatureClass(datasetName);

                          // Iterate through each OID, outputting information.
                          IEnumIDs enumIDs = updateUpdates.IDs;
                          int oid = -1;
                          while ((oid = enumIDs.Next()) != -1)
                          //Loop through all conflicting features. 
                          {
                              Console.WriteLine(
                                  "UpdateUpdate conflicts on feature {0}", oid);

                              // Get conflict feature on the three reconcile versions.
                              IFeature featurePreReconcile =
                                  featureClassPreReconcile.GetFeature(oid);
                              IFeature featureReconcile =
                                  featureClassReconcile.GetFeature(oid);
                              IFeature featureCommonAncestor =
                                  featureClassCommonAncestor.GetFeature(oid);

                              // Check to make sure each shape is different than the common ancestor (conflict is on shape field).
                              if (IsShapeInConflict(featureCommonAncestor,
                                  featurePreReconcile, featureReconcile))
                              {
                                  Console.WriteLine(
                                      " Shape attribute has changed on both versions...");

                                  // Geometries are in conflict ... merge geometries.
                                  try
                                  {
                                      IConstructMerge constructMerge = new
                                          GeometryEnvironmentClass();
                                      IGeometry newGeometry =
                                          constructMerge.MergeGeometries
                                          (featureCommonAncestor.ShapeCopy,
                                          featureReconcile.ShapeCopy,
                                          featurePreReconcile.ShapeCopy);

                                      // Setting new geometry as a merge between the two versions.
                                      IFeature feature = featureClass.GetFeature(oid);
                                      feature.Shape = newGeometry;
                                      feature.Store();
                                      updateUpdates.RemoveList(1, ref oid);
                                      conflictsRemoved = true;
                                  }
                                  catch (COMException comExc)
                                  {
                                      // Check if the error is from overlapping edits.
                                      if (comExc.ErrorCode == (int)
                                          fdoError.FDO_E_WORKSPACE_EXTENSION_DATASET_CREATE_FAILED || comExc.ErrorCode == (int)fdoError.FDO_E_WORKSPACE_EXTENSION_DATASET_DELETE_FAILED)
                                      {
                                          // Edited areas overlap.
                                          Console.WriteLine(
                                              "Error from overlapping edits on feature {0}", oid);
                                          Console.WriteLine(" Error Message: {0}",
                                              comExc.Message);
                                          Console.WriteLine(
                                              " Can't merge overlapping edits to same feature.");
                                      }
                                      else
                                      {
                                          // Unexpected COM exception, throw this to the exception handler.
                                          throw comExc;
                                      }
                                  }
                              }
                              else
                              {
                                  Console.WriteLine(
                                      " Shape field not in conflict: merge not necessary ... ");
                              }
                          }
                      }
                  }
              }
          }
          catch (COMException comExc)
          {
              Console.WriteLine("Error Message: {0}, Error Code: {1}", comExc.Message,
                  comExc.ErrorCode);
          }
          catch (Exception exc)
          {
              Console.WriteLine("Unhandled Exception: {0}", exc.Message);
          }
      }

      // Method to determine if the shape field is in conflict.
      private bool IsShapeInConflict(IFeature commonAncestorFeature, IFeature
          preReconcileFeature, IFeature reconcileFeature)
      {
          // 1st check: Common Ancestor with PreReconcile.
          // 2nd check: Common Ancestor with Reconcile. 
          // 3rd check: Reconcile with PreReconcile (case of same change on both versions).
          if (IsGeometryEqual(commonAncestorFeature.ShapeCopy,
              preReconcileFeature.ShapeCopy) || IsGeometryEqual
              (commonAncestorFeature.ShapeCopy, reconcileFeature.ShapeCopy) ||
              IsGeometryEqual(reconcileFeature.ShapeCopy,
              preReconcileFeature.ShapeCopy))
          {
              return false;
          }
          else
          {
              return true;
          }
      }

      // Method returning if two shapes are equal to one another.
      private bool IsGeometryEqual(IGeometry shape1, IGeometry shape2)
      {
          if (shape1 == null & shape2 == null)
          {
              return true;
          }
          else if (shape1 == null ^ shape2 == null)
          {
              return false;
          }
          else
          {
              IClone clone1 = (IClone)shape1;
              IClone clone2 = (IClone)shape2;
              return clone1.IsEqual(clone2);
          }
      }
  }
}

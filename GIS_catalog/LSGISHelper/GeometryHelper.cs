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
namespace LSGISHelper
{
   public class GeometryHelper
    {
       public static void QueryGeometryLocation(IGeometry feaGeom, out double px, out double py)
       {
           px = double.NaN;
           py = double.NaN;
           try
           {
               if (!feaGeom.IsEmpty)
               {
                   IPoint point = null;
                   if (feaGeom is IPoint)
                   {
                       point = feaGeom as IPoint;
                   }
                   else if (feaGeom is IPolyline)
                   {
                       point = (feaGeom as IPolyline).FromPoint;
                   }
                   else if (feaGeom is IPolygon)
                   {
                       point = (feaGeom as IArea).Centroid;
                   }
                   if (point != null)
                   {
                       px = point.X;
                       py = point.Y;
                   }
               }
           }
           catch (Exception)
           {
           }
       }
       public static string ShapeTypeName(esriGeometryType paramGT)
       {
           string str = "";
       
           return str;
       }
   

 

 

 

 

    }
}

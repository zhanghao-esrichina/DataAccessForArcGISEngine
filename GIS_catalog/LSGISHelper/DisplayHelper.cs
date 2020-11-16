using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using stdole;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Drawing;
using ESRI.ArcGIS.esriSystem;
namespace LSGISHelper
{
    public class DisplayHelper
    {
        // Methods
        public static ITransformation CreateTransformationFromHDC(IntPtr HDC, int width, int height)
        {
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(0.0, 0.0, (double)width, (double)height);
            tagRECT grect = new tagRECT();
            grect.left = 0;
            grect.top = 0;
            grect.right = width;
            grect.bottom = height;
            double dpiY = Graphics.FromHdc(HDC).DpiY;
            long num2 = (long)dpiY;
            if (num2 == 0L)
            {
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("获取设备比例尺失败");
                return null;
            }
            IDisplayTransformation transformation = new DisplayTransformationClass();
            transformation.Bounds = envelope;
            transformation.VisibleBounds = envelope;
            transformation.set_DeviceFrame(ref grect);
            transformation.Resolution = dpiY;
            return transformation;
        }

        public static void DrawGeometry(IScreenDisplay paramScreenDisplay, IGeometry paramGeom, ISymbol paramSymbol)
        {
            if ((((paramScreenDisplay != null) && (paramGeom != null)) && !paramGeom.IsEmpty) && (paramSymbol != null))
            {
                paramScreenDisplay.StartDrawing(paramScreenDisplay.hDC, -2);
                paramScreenDisplay.SetSymbol(paramSymbol);
                if (paramGeom is IPoint)
                {
                    paramScreenDisplay.DrawPoint(paramGeom);
                }
                else if (paramGeom is IPolyline)
                {
                    paramScreenDisplay.DrawPolyline(paramGeom);
                }
                else if (paramGeom is IPolygon)
                {
                    paramScreenDisplay.DrawPolygon(paramGeom);
                }
                paramScreenDisplay.FinishDrawing();
            }
        }

        public static void DrawText(IScreenDisplay paramScreenDisplay, IGeometry paramGeom, string paramText, ISymbol paramSymbol)
        {
            if ((((paramScreenDisplay != null) && (paramGeom != null)) && (!paramGeom.IsEmpty && (paramSymbol != null))) && (paramSymbol is ITextSymbol))
            {
                paramScreenDisplay.StartDrawing(paramScreenDisplay.hDC, -2);
                paramScreenDisplay.UpdateWindow();
                paramScreenDisplay.SetSymbol(paramSymbol);
                if (paramText == null)
                {
                    paramText = "";
                }
                paramScreenDisplay.DrawText(paramGeom, paramText);
                paramScreenDisplay.FinishDrawing();
            }
        }

        public static double FromPixesToRealWorld(IActiveView pView, int pixes)
        {
            if (pView == null)
            {
                return (double)pixes;
            }
            int num = pView.ExportFrame.right - pView.ExportFrame.left;
            double width = pView.Extent.Width;
            return ((pixes * width) / ((double)num));
        }
    }


}

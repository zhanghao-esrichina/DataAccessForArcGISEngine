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
using stdole;
using System.Drawing;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using ESRI.ArcGIS.Display;
namespace LSArcCatalog
{
    public partial class FeatureClassProperties : DevExpress.XtraEditors.XtraForm
    {
        public FeatureClassProperties()
        {
            InitializeComponent();
           
        }
        private IFeatureClassName m_pFcN = null;
        public IFeatureClassName FeatureClassName
        {
            get { return m_pFcN; }
            set { m_pFcN = value; }
        }
        private IGeoFeatureLayer FeatureLayer = null;
        private void LoadData()       
        {
            IName pName = m_pFcN as IName;
            IFeatureClass m_pFc = pName.Open() as IFeatureClass;
            IFeatureLayer  pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.Name = m_pFcN.FeatureDatasetName.Name;
            pFeatureLayer.FeatureClass = m_pFc;
            FeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            this.txtName.Text = (m_pFc as IDataset).Name;
            this.txtAliseName.Text = m_pFc.AliasName;
            this.txtShapeType.Text = GetShapeType(m_pFc);
            string sSR = LSGISHelper.SpatialReferenceHelper.FormatSpatialReference((m_pFc as IGeoDataset).SpatialReference);
            this.richTextBox1.Text = sSR;
            LoadField(m_pFc);
            InitAnnoClass();
        }
        private ArrayList m_annoClassList = new ArrayList();
        public void InitAnnoClass()
        {
            if (this.m_annoClassList.Count <= 0)
            {
                this.m_annoClassList.Clear();
                if (this.FeatureLayer != null)
                {
                    this.cbDisplayAnno.Checked = this.FeatureLayer.DisplayAnnotation;
                    #region 提取注记字段
                    int fldCount = this.FeatureLayer.FeatureClass.Fields.FieldCount;
                    for (int fi = 0; fi < fldCount; fi++)
                    {
                        IField curFld = this.FeatureLayer.FeatureClass.Fields.get_Field(fi);
                        ComboBoxItem cbi = new ComboBoxItem(curFld, curFld.AliasName, fi + 1);
                        this.cbAnnoFieldList.Items.Add(cbi);
                    }
                    #endregion
                    #region 提取所有可用字体
                    FontFamily[] allFamily = FontFamily.Families;
                    int familyCount = allFamily.Length;
                    for (int fi = 0; fi < familyCount; fi++)
                    {
                        FontFamily ff = allFamily[fi];
                        ComboBoxItem cbi = new ComboBoxItem(ff, ff.Name, fi + 1);
                        this.cbFontName.Items.Add(cbi);
                    }
                    #endregion
                    #region 提取注记类
                    int classCount = this.FeatureLayer.AnnotationProperties.Count;
                    for (int ci = 0; ci < classCount; ci++)
                    {
                        AnnotationPropertyItem propItem = new AnnotationPropertyItem();
                        this.FeatureLayer.AnnotationProperties.QueryItem(ci, out propItem.Properties, out propItem.PlacedElementCollection, out propItem.UnplacedElementCollection);
                        propItem.Clone();
                        this.m_annoClassList.Add(propItem);
                    }
                    classCount = this.m_annoClassList.Count;
                    for (int ci = 0; ci < classCount; ci++)
                    {
                        AnnotationPropertyItem propItem = this.m_annoClassList[ci] as AnnotationPropertyItem;
                        ComboBoxItem cbi = new ComboBoxItem(propItem, propItem.Properties.Class, ci + 1);
                        this.cbAnnoClass.Items.Add(cbi);
                    }
                    if (this.cbAnnoClass.Items.Count > 0)
                        this.cbAnnoClass.SelectedIndex = 0;
                    #endregion

                }
            }
        } 
        private void LoadField(IFeatureClass pfc)
        {
           
            for (int i = 0; i <pfc.Fields.FieldCount; i++)
            {
                ListViewItem lv = new ListViewItem();
                IField pField=pfc.Fields.get_Field(i);
                string sFieldName = pField.Name;
                lv.SubItems.Add(sFieldName);
                lv.SubItems.Add(pField.Type.ToString());
                lv.SubItems.Add(pField.AliasName);
                lv.SubItems.Add(pField.Length.ToString());
                lv.SubItems.Add(pField.Precision.ToString());

                lv.Tag = pField;
                if (i% 2 == 0)
                {
                    lv.BackColor = System.Drawing.Color.GreenYellow;
                }
                this.listView1.Items.Add(lv);
            }

            
          
        }
        private void FeatureClassProperties_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private string GetShapeType(IFeatureClass pfc)
        {
            string sS = "";
            if (pfc.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                sS = "注记类型";
            }
            else if (pfc.FeatureType == esriFeatureType.esriFTComplexEdge)
            {
                sS = "复杂边类型";
            }
            else if (pfc.FeatureType == esriFeatureType.esriFTComplexJunction)
            {
                sS = "复杂结合点类型";
            }
            else if (pfc.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
            {
                sS = "Coverage注记类型";
            }
            else if (pfc.FeatureType == esriFeatureType.esriFTSimpleJunction)
            {
                sS = "简单结合点类型";
            }
            else if (pfc.FeatureType == esriFeatureType.esriFTSimpleEdge)
            {
                sS = "简单边类型";
            }
            else if (pfc.FeatureType == esriFeatureType.esriFTSimple)
            {
                esriGeometryType eType = pfc.ShapeType;
                if (eType == esriGeometryType.esriGeometryPoint)
                {
                    sS = "点状要素";
                }
                else if (eType == esriGeometryType.esriGeometryPolyline)
                {
                    sS = "线状要素";
                }
                else if (eType == esriGeometryType.esriGeometryPolygon)
                {
                    sS = "面状要素";
                }
            }
            return sS;
        }
        private AnnotationPropertyItem CurrentAnnotateClass
        {
            get
            {
                AnnotationPropertyItem resultItem = null;
                if (this.cbAnnoClass.SelectedItem is ComboBoxItem)
                {

                    object itemObj = (this.cbAnnoClass.SelectedItem as ComboBoxItem).ItemObject;
                    resultItem = itemObj as AnnotationPropertyItem;
                }
                return resultItem;
            }
        }
        private IFontDisp CurrentFont
        {
            get
            {
                string fName = "宋体"; double fSize = 8.0; bool fBold = false;
                bool fItalic = false; bool fUnderline = false; bool fStroke = false;
                Color fColor = Color.Black;

                stdole.StdFontClass stdFont = new StdFontClass();
                ComboBoxItem fItem = this.cbFontName.SelectedItem as ComboBoxItem;
                if (fItem != null && fItem.ItemObject is FontFamily)
                {
                    fName = (fItem.ItemObject as FontFamily).Name;
                }
                if (!Double.TryParse(this.cbFontSize.Text, System.Globalization.NumberStyles.Any
                    , new System.Globalization.NumberFormatInfo(), out fSize))
                {
                    fSize = 8.0;
                }
                fBold = this.ceFontBold.Checked;
                fItalic = this.ceFontItalic.Checked;
                fUnderline = this.ceFontUnderline.Checked;
                fStroke = this.ceFontStroke.Checked;
                try
                {
                    fColor = (Color)this.ceFontColor.EditValue;
                }
                catch (Exception ex)
                {
                }
                stdFont.Name = fName;
                stdFont.Size = Convert.ToDecimal(fSize);
                stdFont.Bold = fBold;
                stdFont.Italic = fItalic;
                stdFont.Underline = fUnderline;
                stdFont.Strikethrough = fStroke;
                IFontDisp fDisp = stdFont as IFontDisp;
                return fDisp;
            }
        }
        private bool m_shouldAction = true;
        private void ChangeCurrentFont()
        {
            if (this.m_shouldAction)
            {
                if (this.CurrentAnnotateClass != null)
                {
                    ITextSymbol ts = this.CurrentAnnotateClass.LabelEngineLayerProperties.Symbol;
                    IFontDisp fDisp = this.CurrentFont;
                    if (fDisp != null)
                    {
                        ts.Font = fDisp;
                    }
                    try
                    {
                        ts.Color = LSGISHelper.ColorHelper.CreateColor((Color)this.ceFontColor.EditValue);
                    }
                    catch (Exception ex)
                    {
                    }
                    Image sampleImage = LSGISHelper.SymbolHelper.StyleToImage(ts as ISymbol, this.pbFontSample.Width, this.pbFontSample.Height);
                    this.pbFontSample.Image = sampleImage;
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ApplyAnnotate()
        {
            if (this.FeatureLayer != null)
            {
                this.FeatureLayer.AnnotationProperties.Clear();
                foreach (AnnotationPropertyItem api in this.m_annoClassList)
                {
                    this.FeatureLayer.AnnotationProperties.Add(api.Properties);
                }
                this.FeatureLayer.DisplayAnnotation = this.cbDisplayAnno.Checked;
            }
        }
        private void cbFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCurrentFont();
        }

        private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCurrentFont();
        }

        private void ceFontBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCurrentFont();
        }

        private void ceFontItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCurrentFont();
        }

        private void ceFontUnderline_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCurrentFont();
        }

        private void ceFontStroke_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCurrentFont();
        }

        private void ceFontColor_EditValueChanged(object sender, EventArgs e)
        {
            ChangeCurrentFont();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ApplyAnnotate();
        }
    }
    public class AnnotationPropertyItem
    {
        public IAnnotateLayerProperties Properties;
        public IElementCollection PlacedElementCollection;
        public IElementCollection UnplacedElementCollection;
        public AnnotationPropertyItem()
        {
            Properties = new LabelEngineLayerPropertiesClass();
            this.PlacedElementCollection = new ElementCollectionClass();
            this.UnplacedElementCollection = new ElementCollectionClass();
        }
        public ILabelEngineLayerProperties LabelEngineLayerProperties
        {
            get
            {
                return this.Properties as ILabelEngineLayerProperties;
            }
        }
        public void Clone()
        {
            this.Properties = (this.Properties as IClone).Clone() as IAnnotateLayerProperties;
        }
    }
    /// <summary>
    /// ComboBoxItem 提供一个通用的在CombBox中挂接Item的方法。
    /// 其中ItemObject 表示实际挂接的对象
    /// ItemText表示显示给用户看的说明
    /// ItemIndex表示索引
    /// </summary>
    public class ComboBoxItem
    {
        public object ItemObject;
        public string ItemText = "";
        public string ItemIndex = "";
        public ComboBoxItem(object paramObj, string paramText, int paramIndex)
            : this(paramObj, paramText, paramIndex.ToString())
        {

        }
        public ComboBoxItem(object paramObj, string pText)
            : this(paramObj, pText, "")
        {

        }
        public ComboBoxItem(object paramObj, string paramText, string paramIndex)
        {
            if (paramText == null) paramText = "";
            this.ItemObject = paramObj;
            this.ItemText = paramText;
            this.ItemIndex = paramIndex;
        }
        public override string ToString()
        {
            if (this.ItemIndex.Equals(""))
            {
                return this.ItemText;
            }
            else
            {
                string order = this.ItemIndex.PadRight(3, ' ');
                return order + " | " + this.ItemText;
            }
        }
        public override bool Equals(object obj)
        {
            if (this.ItemObject == null)
                return false;
            if (obj is ComboBoxItem)
            {
                ComboBoxItem paramItem = obj as ComboBoxItem;
                return this.ItemObject.Equals(paramItem.ItemObject);
            }
            return false;
        }
        public override int GetHashCode()
        {
            if (this.ItemObject == null)
                return base.GetHashCode();
            else return this.ItemObject.GetHashCode();
        }


    }
}
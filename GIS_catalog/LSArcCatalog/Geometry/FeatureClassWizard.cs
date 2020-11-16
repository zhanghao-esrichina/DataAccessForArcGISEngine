using System;
using System.Drawing;
using System.Collections;
using System.Text ;
using System.Collections .Generic ;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace LSArcCatalog
{
	/// <summary>
	/// FeatureClassWizard 的摘要说明。
	/// </summary>
	public class FeatureClassWizard : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel srPanel;
        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraEditors.TextEdit teAliasName;
        private System.Windows.Forms.ComboBox cbGT;
        private System.Windows.Forms.TabPage tpField;
        private System.Windows.Forms.TabPage tpGeneral;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FeatureClassWizard()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			this.m_srControl =new SpatialReferenceControl ();
            this.m_srControl.cbPrecision.Checked = true;
            this.m_fldControl =new DataTableFieldControl ();
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatureClassWizard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGT = new System.Windows.Forms.ComboBox();
            this.srPanel = new System.Windows.Forms.Panel();
            this.teAliasName = new DevExpress.XtraEditors.TextEdit();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.tpField = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teAliasName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 625);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 40);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(329, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(233, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGeneral);
            this.tabControl1.Controls.Add(this.tpField);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(417, 625);
            this.tabControl1.TabIndex = 1;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.label3);
            this.tpGeneral.Controls.Add(this.label1);
            this.tpGeneral.Controls.Add(this.cbGT);
            this.tpGeneral.Controls.Add(this.srPanel);
            this.tpGeneral.Controls.Add(this.teAliasName);
            this.tpGeneral.Controls.Add(this.teName);
            this.tpGeneral.Controls.Add(this.label2);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(409, 599);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "基本属性";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "几何类型";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "编码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbGT
            // 
            this.cbGT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGT.Location = new System.Drawing.Point(80, 56);
            this.cbGT.Name = "cbGT";
            this.cbGT.Size = new System.Drawing.Size(305, 20);
            this.cbGT.TabIndex = 3;
            // 
            // srPanel
            // 
            this.srPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.srPanel.Location = new System.Drawing.Point(0, 80);
            this.srPanel.Name = "srPanel";
            this.srPanel.Size = new System.Drawing.Size(409, 514);
            this.srPanel.TabIndex = 2;
            // 
            // teAliasName
            // 
            this.teAliasName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.teAliasName.EditValue = "";
            this.teAliasName.Location = new System.Drawing.Point(80, 32);
            this.teAliasName.Name = "teAliasName";
            this.teAliasName.Size = new System.Drawing.Size(305, 21);
            this.teAliasName.TabIndex = 1;
            // 
            // teName
            // 
            this.teName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.teName.EditValue = "";
            this.teName.Location = new System.Drawing.Point(80, 8);
            this.teName.Name = "teName";
            this.teName.Size = new System.Drawing.Size(305, 21);
            this.teName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpField
            // 
            this.tpField.Location = new System.Drawing.Point(4, 22);
            this.tpField.Name = "tpField";
            this.tpField.Size = new System.Drawing.Size(449, 599);
            this.tpField.TabIndex = 1;
            this.tpField.Text = "字段定义";
            // 
            // FeatureClassWizard
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(417, 665);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeatureClassWizard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建特性类";
            this.Load += new System.EventHandler(this.FeatureClassWizard_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teAliasName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion
        private IWorkspace m_workspace;
        public IWorkspace Workspace
        {
            get
            {
                return this.m_workspace ;
            }
            set
            {
                this.m_workspace =value;
            }
        }
        public string FeatureClassName
        {
            get
            {
                return this.teName.Text.Trim () ;
            }
            set
            {
                this.teName .Text =value;
            }
        }
        public string FeatureClassAliasName
        {
            get
            {
                return this.teAliasName.Text ;
            }
            set
            {
                this.teAliasName .Text =value;
            }
        }
        private IFields m_fields=new FieldsClass() ;
        public IFields Fields
        {
            get
            {
                return this.m_fields;
            }
            set
            {
                this.m_fields =value;
            }
        }
        private IField  m_spatialField;
        private SpatialReferenceControl m_srControl;
        private DataTableFieldControl   m_fldControl;
        private ISpatialReference m_pSr = null;
        public ISpatialReference SpatialReference
        {
            get { return m_pSr; }
            set { m_pSr = value; }
        }
        private void FeatureClassWizard_Load(object sender, System.EventArgs e)
        {
            if(this.Workspace ==null)
            {
               MessageBox.Show ("找不到工作空间！","新建特性类",
                   MessageBoxButtons.OK ,MessageBoxIcon.Error );
                this.tabControl1 .Enabled =false;
                this.btnOK .Enabled =false;                
            }
            #region 添加几何类型选项
            ComboBoxItem cbi=new ComboBoxItem (esriGeometryType.esriGeometryPoint,"点",1);
            this.cbGT .Items.Add (cbi);
            cbi=new ComboBoxItem (esriGeometryType.esriGeometryPolyline,"线",2);
            this.cbGT .Items .Add (cbi);
            cbi=new ComboBoxItem (esriGeometryType.esriGeometryPolygon,"面",3);
            this.cbGT .Items .Add (cbi);
            this.cbGT.SelectedIndex =2;
            #endregion
            #region 构建默认字段
            if(this.Fields .FindField("OBJECTID")<0)
            {
                IField oid=LSGISHelper.FieldHelper.CreateOIDField ();
                (this.Fields as IFieldsEdit).AddField (oid);
            }
            if(this.Fields .FindField ("SHAPE")<0)
            {
                m_spatialField = LSGISHelper.FieldHelper.CreateGeometryField(esriGeometryType.esriGeometryPolygon
                    ,new UnknownCoordinateSystemClass());
                (this.Fields as IFieldsEdit).AddField(m_spatialField);
            }
            else
            {
                this.m_spatialField=this.Fields .get_Field (this.Fields .FindField ("SHAPE"));
                this.m_srControl .InputSR =this.m_spatialField .GeometryDef .SpatialReference ;
                esriGeometryType aGT=this.m_spatialField .GeometryDef .GeometryType;
                if(esriGeometryType.esriGeometryPoint==aGT)this.cbGT .SelectedIndex=0;
                if(esriGeometryType.esriGeometryPolyline==aGT)this.cbGT .SelectedIndex=1;
                if(esriGeometryType.esriGeometryPolygon==aGT)this.cbGT .SelectedIndex=2;
            }
            #endregion
            this.m_srControl.InputSR = this.m_pSr;
            this.m_srControl .Dock =DockStyle.Fill ;
            
            this.srPanel .Controls .Add (this.m_srControl);
            
            this.m_fldControl.Dock =DockStyle.Fill ;
            this.m_fldControl .Fields =this.Fields ;
            this.m_fldControl .Workspace =this.Workspace ;
            this.tpField .Controls .Add (this.m_fldControl);

        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string className=this.FeatureClassName;
            #region 检查名称
            if(className.Equals (""))
            {
                MessageBox.Show ("特性类名称为空!","新建特性类",
                    MessageBoxButtons.OK ,MessageBoxIcon.Error );
                this.teName .Focus ();
                return;
            }
            className=className.ToUpper ();
            List<String> sc=LSGISHelper.WorkspaceHelper.QueryFeatureClassName(this.Workspace,true);
            if(sc.Contains (className))
            {
                MessageBox.Show ("特性类名称重复!","新建特性类",
                    MessageBoxButtons.OK ,MessageBoxIcon.Error );
                this.teName .Focus ();
                return;
            }
            #endregion            
            #region 检查投影
            if(!this.m_srControl .ApplyEdit ())
            {
               MessageBox.Show ("特性类的坐标系统设置错误!","新建特性类"
                   ,MessageBoxButtons.OK ,MessageBoxIcon.Error );
                return;
            }
            #endregion
            this.m_fldControl .OnApply ();
            #region 修改投影和几何类型
            ComboBoxItem cbi=this.cbGT .SelectedItem as ComboBoxItem ;
            IGeometryDef aGeomDef=(this.m_spatialField  as IFieldEdit).GeometryDef;
            (aGeomDef as IGeometryDefEdit).GeometryType_2 =(esriGeometryType )cbi.ItemObject ;
            (aGeomDef as IGeometryDefEdit).SpatialReference_2 =this.m_srControl .OutputSR; 
            (this.m_spatialField  as IFieldEdit).GeometryDef_2=aGeomDef;
            #endregion
            #region 将图形字段调整到最后
            (this.Fields as IFieldsEdit).DeleteField (this.m_spatialField);
            (this.Fields as IFieldsEdit ).AddField (this.m_spatialField);
            #endregion
            this.DialogResult =DialogResult.OK ;
            this.Cursor = Cursors.Arrow;
            this.Close ();

        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult =DialogResult.Cancel ;
            this.Close ();
        }

	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using LSCommonHelper;
using LSGISHelper;
namespace LSArcCatalog
{
	/// <summary>
	/// DatasetProperties 的摘要说明。
	/// </summary>
	public class SpatialReferenceControl : System.Windows.Forms.UserControl
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox zGroup;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit textEdit7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit textEdit10;
        private DevExpress.XtraEditors.TextEdit textEdit11;
        private System.Windows.Forms.GroupBox mGroup;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit textEdit8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.TextEdit textEdit9;
        private DevExpress.XtraEditors.TextEdit textEdit12;
        private System.Windows.Forms.RichTextBox tbSR;
        private DevExpress.XtraEditors.TextEdit teXMin;
        private DevExpress.XtraEditors.TextEdit teXMax;
        private DevExpress.XtraEditors.TextEdit teYMax;
        private DevExpress.XtraEditors.TextEdit teYMin;
        private DevExpress.XtraEditors.TextEdit teXYUnit;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private System.Windows.Forms.GroupBox xyGroup;
        public CheckBox cbPrecision;
        private Label label12;
        private GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtZYJX;
        private DevExpress.XtraEditors.SimpleButton cmdEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SpatialReferenceControl()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();
            this.cbPrecision.Checked = true;
			// TODO: 在 InitializeComponent 调用后添加任何初始化
            this.teXYUnit.Text = "10000";
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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.tbSR = new System.Windows.Forms.RichTextBox();
            this.xyGroup = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.teXYUnit = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.teYMax = new DevExpress.XtraEditors.TextEdit();
            this.teYMin = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.teXMax = new DevExpress.XtraEditors.TextEdit();
            this.teXMin = new DevExpress.XtraEditors.TextEdit();
            this.zGroup = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textEdit7 = new DevExpress.XtraEditors.TextEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textEdit10 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit11 = new DevExpress.XtraEditors.TextEdit();
            this.mGroup = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textEdit8 = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textEdit9 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit12 = new DevExpress.XtraEditors.TextEdit();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.cbPrecision = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmdEdit = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtZYJX = new DevExpress.XtraEditors.TextEdit();
            this.xyGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teXYUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXMin.Properties)).BeginInit();
            this.zGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit10.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit11.Properties)).BeginInit();
            this.mGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit8.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit9.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit12.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtZYJX.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSR
            // 
            this.tbSR.Location = new System.Drawing.Point(8, 8);
            this.tbSR.Name = "tbSR";
            this.tbSR.Size = new System.Drawing.Size(328, 143);
            this.tbSR.TabIndex = 1;
            this.tbSR.Text = "";
            // 
            // xyGroup
            // 
            this.xyGroup.Controls.Add(this.label5);
            this.xyGroup.Controls.Add(this.teXYUnit);
            this.xyGroup.Controls.Add(this.label3);
            this.xyGroup.Controls.Add(this.label4);
            this.xyGroup.Controls.Add(this.teYMax);
            this.xyGroup.Controls.Add(this.teYMin);
            this.xyGroup.Controls.Add(this.label2);
            this.xyGroup.Controls.Add(this.label1);
            this.xyGroup.Controls.Add(this.teXMax);
            this.xyGroup.Controls.Add(this.teXMin);
            this.xyGroup.Location = new System.Drawing.Point(8, 283);
            this.xyGroup.Name = "xyGroup";
            this.xyGroup.Size = new System.Drawing.Size(392, 88);
            this.xyGroup.TabIndex = 2;
            this.xyGroup.TabStop = false;
            this.xyGroup.Text = "XY值域";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "XY Unit";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teXYUnit
            // 
            this.teXYUnit.EditValue = "10000";
            this.teXYUnit.Location = new System.Drawing.Point(64, 64);
            this.teXYUnit.Name = "teXYUnit";
            this.teXYUnit.Size = new System.Drawing.Size(128, 21);
            this.teXYUnit.TabIndex = 8;
            this.teXYUnit.EditValueChanged += new System.EventHandler(this.OnTextEditValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(200, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Y max";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y min";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teYMax
            // 
            this.teYMax.EditValue = "";
            this.teYMax.Location = new System.Drawing.Point(240, 41);
            this.teYMax.Name = "teYMax";
            this.teYMax.Size = new System.Drawing.Size(144, 21);
            this.teYMax.TabIndex = 5;
            this.teYMax.EditValueChanged += new System.EventHandler(this.OnTextEditValueChanged);
            // 
            // teYMin
            // 
            this.teYMin.EditValue = "";
            this.teYMin.Location = new System.Drawing.Point(48, 41);
            this.teYMin.Name = "teYMin";
            this.teYMin.Size = new System.Drawing.Size(144, 21);
            this.teYMin.TabIndex = 4;
            this.teYMin.EditValueChanged += new System.EventHandler(this.OnTextEditValueChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(200, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "X max";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "X min";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teXMax
            // 
            this.teXMax.EditValue = "";
            this.teXMax.Location = new System.Drawing.Point(240, 16);
            this.teXMax.Name = "teXMax";
            this.teXMax.Size = new System.Drawing.Size(144, 21);
            this.teXMax.TabIndex = 1;
            this.teXMax.EditValueChanged += new System.EventHandler(this.OnTextEditValueChanged);
            // 
            // teXMin
            // 
            this.teXMin.EditValue = "";
            this.teXMin.Location = new System.Drawing.Point(48, 16);
            this.teXMin.Name = "teXMin";
            this.teXMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teXMin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teXMin.Size = new System.Drawing.Size(144, 21);
            this.teXMin.TabIndex = 0;
            this.teXMin.EditValueChanged += new System.EventHandler(this.OnTextEditValueChanged);
            // 
            // zGroup
            // 
            this.zGroup.Controls.Add(this.label6);
            this.zGroup.Controls.Add(this.textEdit7);
            this.zGroup.Controls.Add(this.label9);
            this.zGroup.Controls.Add(this.label10);
            this.zGroup.Controls.Add(this.textEdit10);
            this.zGroup.Controls.Add(this.textEdit11);
            this.zGroup.Enabled = false;
            this.zGroup.Location = new System.Drawing.Point(8, 371);
            this.zGroup.Name = "zGroup";
            this.zGroup.Size = new System.Drawing.Size(392, 64);
            this.zGroup.TabIndex = 3;
            this.zGroup.TabStop = false;
            this.zGroup.Text = "Z值域";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Z Unit";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textEdit7
            // 
            this.textEdit7.EditValue = "10000";
            this.textEdit7.Location = new System.Drawing.Point(64, 40);
            this.textEdit7.Name = "textEdit7";
            this.textEdit7.Size = new System.Drawing.Size(128, 21);
            this.textEdit7.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(200, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 23);
            this.label9.TabIndex = 3;
            this.label9.Text = "Z max";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 23);
            this.label10.TabIndex = 2;
            this.label10.Text = "Z min";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textEdit10
            // 
            this.textEdit10.EditValue = "0";
            this.textEdit10.Location = new System.Drawing.Point(240, 16);
            this.textEdit10.Name = "textEdit10";
            this.textEdit10.Size = new System.Drawing.Size(144, 21);
            this.textEdit10.TabIndex = 1;
            // 
            // textEdit11
            // 
            this.textEdit11.EditValue = "0";
            this.textEdit11.Location = new System.Drawing.Point(48, 16);
            this.textEdit11.Name = "textEdit11";
            this.textEdit11.Size = new System.Drawing.Size(144, 21);
            this.textEdit11.TabIndex = 0;
            // 
            // mGroup
            // 
            this.mGroup.Controls.Add(this.label7);
            this.mGroup.Controls.Add(this.textEdit8);
            this.mGroup.Controls.Add(this.label8);
            this.mGroup.Controls.Add(this.label11);
            this.mGroup.Controls.Add(this.textEdit9);
            this.mGroup.Controls.Add(this.textEdit12);
            this.mGroup.Enabled = false;
            this.mGroup.Location = new System.Drawing.Point(8, 435);
            this.mGroup.Name = "mGroup";
            this.mGroup.Size = new System.Drawing.Size(392, 64);
            this.mGroup.TabIndex = 4;
            this.mGroup.TabStop = false;
            this.mGroup.Text = "M值域";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "M Unit";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textEdit8
            // 
            this.textEdit8.EditValue = "10000";
            this.textEdit8.Location = new System.Drawing.Point(64, 40);
            this.textEdit8.Name = "textEdit8";
            this.textEdit8.Size = new System.Drawing.Size(128, 21);
            this.textEdit8.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(200, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 23);
            this.label8.TabIndex = 3;
            this.label8.Text = "M max";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 23);
            this.label11.TabIndex = 2;
            this.label11.Text = "M min";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textEdit9
            // 
            this.textEdit9.EditValue = "0";
            this.textEdit9.Location = new System.Drawing.Point(240, 16);
            this.textEdit9.Name = "textEdit9";
            this.textEdit9.Size = new System.Drawing.Size(144, 21);
            this.textEdit9.TabIndex = 1;
            // 
            // textEdit12
            // 
            this.textEdit12.EditValue = "0";
            this.textEdit12.Location = new System.Drawing.Point(48, 16);
            this.textEdit12.Name = "textEdit12";
            this.textEdit12.Size = new System.Drawing.Size(144, 21);
            this.textEdit12.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(344, 8);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(64, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(344, 37);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbPrecision
            // 
            this.cbPrecision.AutoSize = true;
            this.cbPrecision.Checked = true;
            this.cbPrecision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrecision.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbPrecision.Location = new System.Drawing.Point(6, 154);
            this.cbPrecision.Name = "cbPrecision";
            this.cbPrecision.Size = new System.Drawing.Size(102, 17);
            this.cbPrecision.TabIndex = 10;
            this.cbPrecision.Text = "高精度坐标系";
            this.cbPrecision.UseVisualStyleBackColor = true;
            this.cbPrecision.CheckedChanged += new System.EventHandler(this.cbPrecision_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.GreenYellow;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(103, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(305, 24);
            this.label12.TabIndex = 11;
            this.label12.Text = "(ArcGIS92开始使用高精度数据库,如果目标数据库是使用\nArcGIS92以前的版本生成的。请不要选中这个选项)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.cmdEdit);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.txtZYJX);
            this.groupBox1.Location = new System.Drawing.Point(8, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 96);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "中央经线设置";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(10, 66);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(372, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "注意：请填写要修改的、正确的中央经线，点击修改即可（单位：度）";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(320, 28);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(64, 23);
            this.cmdEdit.TabIndex = 2;
            this.cmdEdit.Text = "修改";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "中央经线";
            // 
            // txtZYJX
            // 
            this.txtZYJX.Location = new System.Drawing.Point(64, 30);
            this.txtZYJX.Name = "txtZYJX";
            this.txtZYJX.Size = new System.Drawing.Size(239, 21);
            this.txtZYJX.TabIndex = 0;
            // 
            // SpatialReferenceControl
            // 
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbPrecision);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.mGroup);
            this.Controls.Add(this.zGroup);
            this.Controls.Add(this.xyGroup);
            this.Controls.Add(this.tbSR);
            this.Name = "SpatialReferenceControl";
            this.Size = new System.Drawing.Size(416, 507);
            this.Load += new System.EventHandler(this.SpatialReferenceControl_Load);
            this.xyGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teXYUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXMin.Properties)).EndInit();
            this.zGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit10.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit11.Properties)).EndInit();
            this.mGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit8.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit9.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit12.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtZYJX.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion
        private ISpatialReference m_inputSR;
        private ISpatialReference m_outputSR;
        public bool HignPrecision
        {
            get
            {
                return this.cbPrecision.Checked;
            }
            set
            {
                this.cbPrecision.Checked = value;
            }
        }
        public ISpatialReference InputSR
        {
            get
            {
                return this.m_inputSR ;
            }
            set
            {
              this.m_inputSR=value ;
              this.DispatchSR (this.m_inputSR );
            }
        }
        public ISpatialReference OutputSR
        {
            get
            {
                return this.m_outputSR ;
            }
        }

        private string sXYUnit = "";
        public string XYUnit
        {
            get { return sXYUnit; }
            set { sXYUnit = value; }
        }

        public bool ApplyEdit()
        {
            try
            {
                double xmin = 0, ymin = 0, xmax = 0, ymax = 0, xyunit = 0;
                xmin = this.ParseText(this.teXMin.EditValue.ToString());
                ymin = this.ParseText(this.teYMin.EditValue.ToString());
                xmax = this.ParseText(this.teXMax.EditValue.ToString());
                ymax = this.ParseText(this.teYMax.EditValue.ToString());
                xyunit = this.ParseText(this.teXYUnit.EditValue.ToString());
                if (xmin >= xmax || ymin >= ymax || xyunit <= 0)
                {
                    MessageBox.Show("XY数据范围填写错误", "生成坐标系",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (this.m_outputSR == null)
                {
                    this.m_outputSR = new UnknownCoordinateSystemClass();
                }
                this.m_outputSR.SetDomain(xmin, xmax, ymin, ymax);
                this.m_outputSR.SetFalseOriginAndUnits(xmin, ymin, xyunit);

                IControlPrecision2 cp = this.m_outputSR as IControlPrecision2;
                if (cp != null) cp.IsHighPrecision = this.cbPrecision.Checked;

                return true;
            }
            catch (Exception ex) { } 
            return false;
        }
        private void DispatchSR(ISpatialReference sr)
        {
            if(sr!=null)
            {

                IControlPrecision2 cp = (sr as IControlPrecision2);
                if (cp!=null)
                {
                    this.cbPrecision.Checked = cp.IsHighPrecision;
                }
                string aLine=LSGISHelper.SpatialReferenceHelper.FormatSpatialReference (sr);
                this.tbSR .Text =aLine;
                if(sr is IProjectedCoordinateSystem 
                    ||sr is UnknownCoordinateSystemClass)
                {
                    this.xyGroup .Enabled =true;
                    this.m_shouldAction =false;
                    double xmin=0,ymin=0,xmax=0,ymax=0,xyunits=0;
                    try
                    {
                        sr.GetDomain(out xmin,out xmax,out ymin,out ymax);
                        sr.GetFalseOriginAndUnits(out xmin, out ymin, out xyunits);
                    }
                    catch(Exception ex)
                    {
                        xmin=0;ymin=0;xyunits=1000;
                        xmax=DataRangeHelper.GetMax (xmin,xyunits);
                        ymax=DataRangeHelper.GetMax (ymin,xyunits);
                    }
                    this.teXMin .Text =xmin.ToString ();
                    this.teXMax .Text =xmax.ToString ();
                    this.teYMin.Text =ymin.ToString ();
                    this.teYMax .Text =ymax.ToString ();
                    this.teXYUnit .Text =xyunits.ToString ();
                    this.m_shouldAction =true;
                }
                else
                {
                    this.xyGroup .Enabled =false;
                }

              
            }
        }
        #region 坐标系按钮
        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            string sSRPath = Application.StartupPath+@"\sr";
            ISpatialReference sr = LSGISHelper.SpatialReferenceHelper.SelectSR(sSRPath);
            if(sr!=null)
            {
                this.sXYUnit = this.teXYUnit.Text.Trim();
                       
                this.DispatchSR (sr);
                this.m_outputSR = sr;
                IProjectedCoordinateSystem prjSR = sr as IProjectedCoordinateSystem;
                if (prjSR != null)
                {
                    this.txtZYJX.Text = prjSR.get_CentralMeridian(true).ToString();
                    this.txtZYJX.Enabled = true;
                    this.cmdEdit.Enabled = true;
                }
                else
                {
                    this.txtZYJX.Text = "";
                    this.txtZYJX.Enabled = false;
                    this.cmdEdit.Enabled = false;
                }
            }
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            this.m_outputSR =new UnknownCoordinateSystemClass ();
            this.DispatchSR (this.m_outputSR );
        }
        #endregion

        private void SpatialReferenceControl_Load(object sender, System.EventArgs e)
        {
            if(this.m_inputSR ==null)
            {
                this.m_inputSR =new  UnknownCoordinateSystemClass ();
                IControlPrecision2 cp = this.m_inputSR as IControlPrecision2;
                if (cp != null) cp.IsHighPrecision = this.cbPrecision.Checked;
                this.DispatchSR (this.InputSR);
                this.teXYUnit.Text = "10000";
                this.cmdEdit.Enabled = false;
                this.txtZYJX.Enabled = false;
                this.txtZYJX.Properties.MaxLength = 6;
            }
            else
            {
                DispatchSR(this.m_inputSR);
            }
        }
	    private bool m_shouldAction=true;
        private double ParseText(string pText)
        {
            double result=double.NaN ;
            double.TryParse (pText,System.Globalization .NumberStyles.Any 
                ,new System .Globalization .NumberFormatInfo (),out result);
            return result;
        }
        private void OnTextEditValueChanged(object sender, System.EventArgs e)
        {
            if(this.m_shouldAction )
            {
                this.m_shouldAction =false;
                DevExpress.XtraEditors.TextEdit te=sender as DevExpress 
                    .XtraEditors .TextEdit;
                string strValue=te.EditValue.ToString () ;
                double newValue=double.NaN ;
                if(double.TryParse (strValue,System.Globalization .NumberStyles.Any 
                    ,new System.Globalization .NumberFormatInfo (),out newValue))
                {
                    if(te==this.teXMin)
                    {  
                        double xyunit=this.ParseText (this.teXYUnit .EditValue .ToString ());
                        double xmax=DataRangeHelper.GetMax (newValue,xyunit);
                        this.teXMax .Text=xmax.ToString ();
                    }
                    else if(te==this.teXMax )
                    {
                        double xmin=this.ParseText (this.teXMin.EditValue.ToString ());
                        double xyunit=DataRangeHelper.GetUnits(xmin,newValue);
                        double ymin=this.ParseText (this.teYMin.EditValue .ToString ());
                        double ymax=DataRangeHelper.GetMax (ymin,xyunit);
                        this.teXYUnit .Text =xyunit.ToString ();
                        this.teYMax .Text =ymax.ToString ();
                    }
                    else if(te==this.teYMin)
                    {
                        double xyunit=this.ParseText (this.teXYUnit .EditValue .ToString ());
                        double ymax=DataRangeHelper.GetMax (newValue,xyunit);
                        this.teYMax .Text=ymax.ToString ();
                    }
                    else if(te==this.teYMax)
                    {
                        double ymin=this.ParseText (this.teYMin .EditValue.ToString ());
                        double xyunit=DataRangeHelper.GetUnits (ymin,newValue);
                        double xmin=this.ParseText (this.teXMin.EditValue .ToString ());
                        double xmax=DataRangeHelper.GetMax (xmin,xyunit);
                        this.teXMax .Text =xmax.ToString ();
                        this.teXYUnit .Text =xyunit.ToString ();
                    }
                    else if(te==this.teXYUnit)
                    {
                        double ymin=this.ParseText (this.teYMin .EditValue.ToString ());                        
                        double xmin=this.ParseText (this.teXMin.EditValue .ToString ());
                        double ymax=DataRangeHelper.GetMax (ymin,newValue);
                        double xmax=DataRangeHelper.GetMax (xmin,newValue);
                        this.teYMax.Text =ymax.ToString ();
                        this.teXMax .Text =xmax.ToString ();
                    }
                }                
                this.m_shouldAction =true;
            }
        }

        private void cbPrecision_CheckedChanged(object sender, EventArgs e)
        {
            this.teXMax.Enabled = !this.cbPrecision.Checked ;
            this.teXMin.Enabled = !this.cbPrecision.Checked;
            this.teYMax.Enabled = !this.cbPrecision.Checked;
            this.teYMin.Enabled = !this.cbPrecision.Checked;            
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string ss = this.txtZYJX.Text.Trim();
                if (ss == "")
                {
                    LSCommonHelper.MessageBoxHelper.ShowMessageBox("请填写需要修改的中央经度线");
                }
                double s = ParseText(ss);
                IProjectedCoordinateSystem prjSR = this.m_outputSR as IProjectedCoordinateSystem;
                prjSR.set_CentralMeridian(true, s);
                this.DispatchSR(this.m_outputSR);
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("修改完毕");
            }
            catch (Exception ex)
            {
                LSCommonHelper.MessageBoxHelper.ShowErrorMessageBox(ex, "修改中央经线");
            }
        }

       

    }
}

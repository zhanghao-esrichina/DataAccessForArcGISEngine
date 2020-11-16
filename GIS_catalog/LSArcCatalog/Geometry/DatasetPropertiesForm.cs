using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
namespace LSArcCatalog
{
	/// <summary>
	/// DatasetPropertiesForm 的摘要说明。
	/// </summary>
	public class DatasetPropertiesForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit teName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel srPanel;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DatasetPropertiesForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.srPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 564);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 40);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(265, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // teName
            // 
            this.teName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.teName.EditValue = "";
            this.teName.Location = new System.Drawing.Point(80, 8);
            this.teName.Name = "teName";
            this.teName.Size = new System.Drawing.Size(289, 21);
            this.teName.TabIndex = 1;
            // 
            // srPanel
            // 
            this.srPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.srPanel.Location = new System.Drawing.Point(0, 32);
            this.srPanel.Name = "srPanel";
            this.srPanel.Size = new System.Drawing.Size(433, 534);
            this.srPanel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据集名称";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DatasetPropertiesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(433, 604);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.srPanel);
            this.Controls.Add(this.teName);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatasetPropertiesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据集属性";
            this.Load += new System.EventHandler(this.DatasetPropertiesForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion
        private SpatialReferenceControl m_srControl=new SpatialReferenceControl();
        private bool m_hignPrecision = true;
        public bool HignPrecision
        {
            get
            {
                return this.m_hignPrecision;
            }
            set
            {
                this.m_hignPrecision = value;
            }
        }
        private void DatasetPropertiesForm_Load(object sender, System.EventArgs e)
        { 
            this.m_srControl .Dock =DockStyle.Fill ;
            this.m_srControl.HignPrecision = this.HignPrecision;
            this.m_srControl .Visible =true;            
            this.srPanel .Controls.Add (this.m_srControl );
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel ;
            this.Close ();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if(this.teName .Text .Trim ().Equals (""))
            {
                MessageBox.Show ("数据集名称不能为空!","数据集属性"
                    ,MessageBoxButtons.OK ,MessageBoxIcon.Error );
                this.teName .Focus ();
                return;                
            }
            if(this.m_srControl .ApplyEdit ())
            {
                this.DialogResult =DialogResult.OK ;
                this.Close ();
            }
            else
            {
                MessageBox.Show ("坐标系参数错误!","数据集属性"
                    ,MessageBoxButtons.OK ,MessageBoxIcon.Error );
                return;
            }
        }
     
        public string FeatureDatasetName
        {
            get
            {
                return this.teName .Text ;
            }
            set
            {
                this.teName .Text=value ;
            }
        }
        public ISpatialReference SpatialReference
        {
            get
            {
                return this.m_srControl.OutputSR ;
            }
            set
            {
                this.m_srControl .InputSR =value;
            }
        }
	}
}

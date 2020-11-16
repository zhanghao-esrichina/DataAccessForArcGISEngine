using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
namespace LSArcCatalog
{
	/// <summary>
	/// DataTableWizard 的摘要说明。
	/// </summary>
	public class DataTableWizard : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel fieldPanel;
        private DevExpress.XtraEditors.TextEdit teAliasName;
        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataTableWizard()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableWizard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.teAliasName = new DevExpress.XtraEditors.TextEdit();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.fieldPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teAliasName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 382);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 40);
            this.panel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(192, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.teAliasName);
            this.panel2.Controls.Add(this.teName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(368, 56);
            this.panel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "表名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "表编码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teAliasName
            // 
            this.teAliasName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.teAliasName.EditValue = "";
            this.teAliasName.Location = new System.Drawing.Point(72, 32);
            this.teAliasName.Name = "teAliasName";
            this.teAliasName.Size = new System.Drawing.Size(280, 20);
            this.teAliasName.TabIndex = 3;
            // 
            // teName
            // 
            this.teName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.teName.EditValue = "";
            this.teName.Location = new System.Drawing.Point(72, 8);
            this.teName.Name = "teName";
            this.teName.Size = new System.Drawing.Size(280, 20);
            this.teName.TabIndex = 2;
            // 
            // fieldPanel
            // 
            this.fieldPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPanel.Location = new System.Drawing.Point(0, 56);
            this.fieldPanel.Name = "fieldPanel";
            this.fieldPanel.Size = new System.Drawing.Size(368, 326);
            this.fieldPanel.TabIndex = 4;
            // 
            // DataTableWizard
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(368, 422);
            this.Controls.Add(this.fieldPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataTableWizard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建表";
            this.Load += new System.EventHandler(this.DataTableWizard_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
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
        private DataTableFieldControl m_fldControl;
        public String TableName
        {
            get
            {
                return this.teName .Text.Trim () ;
            }
            set
            {
                this.teName .Text =value;
            }
        }
        public String TableAliasName
        {
            get
            {
                return this.teAliasName .Text ;
            }
            set
            {
                this.teAliasName .Text =value;
            }
        }
        private IFields m_flds;
        public IFields Fields
        {
            get
            {
                if(this.m_flds ==null)
                    this.m_flds =new FieldsClass();
                return this.m_flds;
            }
            set
            {
                this.m_flds=value;
            }
        }
        private void DataTableWizard_Load(object sender, System.EventArgs e)
        {            
            this.m_fldControl .Dock =DockStyle.Fill ;
            this.m_fldControl .ApplyVisible =false;
            this.m_fldControl .CancelVisible =false;  
            if(this.Fields.FindField ("OBJECTID")<0)
            {
                IField oid = LSGISHelper.FieldHelper.CreateOIDField();
                (this.Fields as IFieldsEdit ).AddField (oid);
            }
            this.m_fldControl .Fields =this.Fields;
            this.m_fldControl .Workspace =this.Workspace ;
            this.fieldPanel .Controls.Add (this.m_fldControl );
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult =DialogResult.Cancel ;
            this.Close ();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if(this.TableName .Equals (""))
            {
                MessageBox.Show ("表的名称不能为空!","新建表"
                    ,MessageBoxButtons.OK ,MessageBoxIcon.Error );
                return;
            }
            this.m_fldControl .OnApply ();
            this.Fields =this.m_fldControl .Fields ;
            this.DialogResult =DialogResult.OK ;
            this.Close ();
        }

        

        
	}
}

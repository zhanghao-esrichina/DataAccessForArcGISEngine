using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
namespace LSArcCatalog
{
	/// <summary>
	/// DataFieldForm 的摘要说明。
	/// </summary>
	public class DataFieldForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit teFieldName;
        private DevExpress.XtraEditors.TextEdit teAliasName;
        private DevExpress.XtraEditors.TextEdit teLength;
        private DevExpress.XtraEditors.TextEdit teScale;
        private DevExpress.XtraEditors.TextEdit teDefault;
        private DevExpress.XtraEditors.CheckEdit ceNullable;
        private System.Windows.Forms.ComboBox cbFieldType;
        private System.Windows.Forms.ComboBox cbDomain;
        private System.Windows.Forms.Label lbError;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataFieldForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
            this.m_fieldList =new ArrayList ();
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.teFieldName = new DevExpress.XtraEditors.TextEdit();
            this.teAliasName = new DevExpress.XtraEditors.TextEdit();
            this.teLength = new DevExpress.XtraEditors.TextEdit();
            this.teScale = new DevExpress.XtraEditors.TextEdit();
            this.teDefault = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ceNullable = new DevExpress.XtraEditors.CheckEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.cbFieldType = new System.Windows.Forms.ComboBox();
            this.cbDomain = new System.Windows.Forms.ComboBox();
            this.lbError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.teFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAliasName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teScale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceNullable.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(248, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(328, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // teFieldName
            // 
            this.teFieldName.EditValue = "";
            this.teFieldName.Location = new System.Drawing.Point(88, 16);
            this.teFieldName.Name = "teFieldName";
            this.teFieldName.Size = new System.Drawing.Size(312, 21);
            this.teFieldName.TabIndex = 2;
            this.teFieldName.EditValueChanged += new System.EventHandler(this.teFieldName_EditValueChanged);
            // 
            // teAliasName
            // 
            this.teAliasName.EditValue = "";
            this.teAliasName.Location = new System.Drawing.Point(88, 40);
            this.teAliasName.Name = "teAliasName";
            this.teAliasName.Size = new System.Drawing.Size(312, 21);
            this.teAliasName.TabIndex = 3;
            // 
            // teLength
            // 
            this.teLength.EditValue = "";
            this.teLength.Location = new System.Drawing.Point(88, 88);
            this.teLength.Name = "teLength";
            this.teLength.Size = new System.Drawing.Size(312, 21);
            this.teLength.TabIndex = 5;
            // 
            // teScale
            // 
            this.teScale.EditValue = "";
            this.teScale.Location = new System.Drawing.Point(88, 112);
            this.teScale.Name = "teScale";
            this.teScale.Size = new System.Drawing.Size(312, 21);
            this.teScale.TabIndex = 6;
            // 
            // teDefault
            // 
            this.teDefault.EditValue = "";
            this.teDefault.Location = new System.Drawing.Point(88, 136);
            this.teDefault.Name = "teDefault";
            this.teDefault.Size = new System.Drawing.Size(312, 21);
            this.teDefault.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "字段代码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "字段名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "默认值";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "精度";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "长度";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "类型";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ceNullable
            // 
            this.ceNullable.EditValue = true;
            this.ceNullable.Location = new System.Drawing.Point(88, 160);
            this.ceNullable.Name = "ceNullable";
            this.ceNullable.Properties.Caption = "允许为空";
            this.ceNullable.Size = new System.Drawing.Size(112, 19);
            this.ceNullable.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 23);
            this.label7.TabIndex = 16;
            this.label7.Text = "所在域";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbFieldType
            // 
            this.cbFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFieldType.Location = new System.Drawing.Point(88, 64);
            this.cbFieldType.Name = "cbFieldType";
            this.cbFieldType.Size = new System.Drawing.Size(312, 20);
            this.cbFieldType.TabIndex = 17;
            this.cbFieldType.SelectedIndexChanged += new System.EventHandler(this.cbFieldType_SelectedIndexChanged);
            // 
            // cbDomain
            // 
            this.cbDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDomain.Location = new System.Drawing.Point(88, 192);
            this.cbDomain.Name = "cbDomain";
            this.cbDomain.Size = new System.Drawing.Size(312, 20);
            this.cbDomain.TabIndex = 18;
            // 
            // lbError
            // 
            this.lbError.BackColor = System.Drawing.SystemColors.Control;
            this.lbError.ForeColor = System.Drawing.Color.Yellow;
            this.lbError.Location = new System.Drawing.Point(16, 232);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(208, 23);
            this.lbError.TabIndex = 19;
            this.lbError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataFieldForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 270);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.cbDomain);
            this.Controls.Add(this.cbFieldType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ceNullable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.teDefault);
            this.Controls.Add(this.teScale);
            this.Controls.Add(this.teLength);
            this.Controls.Add(this.teAliasName);
            this.Controls.Add(this.teFieldName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataFieldForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字段属性";
            this.Load += new System.EventHandler(this.DataFieldForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.teFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAliasName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceNullable.Properties)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion
        #region 参数
        private IWorkspace m_workspace;
        private ArrayList  m_fieldList;
        private IField m_curField;
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
        public ArrayList FieldList
        {
            get
            {
                return this.m_fieldList  ;
            }            
        }
        public IField ResultField
        {
            get
            {
                return this.m_curField;
            }
            set
            {
                 this.m_curField=value;
            }
        }
        private IFeatureClass m_featureclass;
        public IFeatureClass FeatureClass
        {
            get
            {
                return this.m_featureclass;
            }
            set
            {
                this.m_featureclass = value;
            }
        }
        #endregion
        private void LoadFieldType(esriFieldType pFT)
        {
            int order=this.cbFieldType .Items .Count ;
            String typeText=LSGISHelper.FieldHelper.QueryFieldTypeName(pFT);
            ComboBoxItem cbi=new ComboBoxItem (pFT,typeText,order);
            this.cbFieldType  .Items .Add (cbi);            
        }
        private void LoadDomain()
        {
            this.cbDomain  .Items .Clear ();
            this.cbDomain .Items .Add ("");
            ComboBoxItem selFT=this.cbFieldType .SelectedItem as ComboBoxItem ;
            if(selFT!=null)
            {
                esriFieldType esriFT=(esriFieldType)selFT.ItemObject ;
                IEnumDomain ed=(this.Workspace as IWorkspaceDomains ).get_DomainsByFieldType (esriFT);
                IDomain aDomain=ed.Next ();
                while(aDomain!=null)
                {
                    this.LoadDomain (aDomain);
                    aDomain=ed.Next ();
                }
            }
        }
        private void LoadDomain(IDomain pDomain)
        {
            int order=this.cbDomain .Items.Count +1;
            ComboBoxItem cbi=new ComboBoxItem (pDomain,pDomain.Name,order);
            this.cbDomain .Items .Add (cbi);
        }
        private int FindFT(esriFieldType pFT)
        {
            int fc=this.cbFieldType .Items.Count ;
            for(int fi=0;fi<fc;fi++)
            {
                ComboBoxItem cbi=this.cbFieldType .Items [fi] 
                    as ComboBoxItem;
                if(cbi.ItemObject .Equals (pFT))
                {
                    return fi;
                }
            }
            return -1;
        }
        private int FindDomain(IDomain pDomain)
        {
            int dc=this.cbDomain .Items .Count ;
            for(int di=0;di<dc;di++)
            {
                ComboBoxItem cbi=this.cbDomain .Items [di] 
                    as ComboBoxItem;
                if(cbi!=null&&cbi.ItemObject .Equals (pDomain))
                {
                    return di;
                }
            }
            return 0;
        }
        private void DataFieldForm_Load(object sender, System.EventArgs e)
        {
            #region 载入字段类型
            this.cbFieldType  .Items .Clear ();
            this.LoadFieldType(esriFieldType.esriFieldTypeBlob);
            this.LoadFieldType (esriFieldType.esriFieldTypeDate);
            this.LoadFieldType (esriFieldType.esriFieldTypeDouble);
            this.LoadFieldType (esriFieldType.esriFieldTypeGeometry);
            this.LoadFieldType (esriFieldType.esriFieldTypeGlobalID);
            this.LoadFieldType (esriFieldType.esriFieldTypeGUID);
            this.LoadFieldType (esriFieldType.esriFieldTypeInteger);
            this.LoadFieldType (esriFieldType.esriFieldTypeOID);
            this.LoadFieldType (esriFieldType.esriFieldTypeRaster);
            this.LoadFieldType (esriFieldType.esriFieldTypeSingle);
            this.LoadFieldType (esriFieldType.esriFieldTypeSmallInteger);
            this.LoadFieldType (esriFieldType.esriFieldTypeString);
            this.cbFieldType .SelectedIndex =11;//默认类型为String

            #endregion            
            this.lbError.Visible = false;
            if (this.ResultField != null)
            {
                this.lbError.Visible = true;
                this.teFieldName .Text =this.ResultField .Name ;
                this.teFieldName .Enabled =false;
                this.teAliasName .Text =this.ResultField .AliasName ;
                this.cbFieldType .SelectedIndex =this.FindFT (this.ResultField .Type );
                this.cbFieldType .Enabled =false;
                this.teDefault .Text =this.ResultField .DefaultValue .ToString ();
                this.teDefault .Enabled =false;
                this.teLength.Text = LSGISHelper.FieldHelper.QueryFieldLength(this.ResultField).ToString();
                this.teLength .Enabled =false;
                this.teScale.Text = LSGISHelper.FieldHelper.QueryFieldPrecision(this.ResultField).ToString();
                this.teScale .Enabled =false;
                this.ceNullable .Checked =this.ResultField .IsNullable ; 
                this.ceNullable .Enabled =false;
                if(this.ResultField .Domain !=null)
                {
                    int di=this.FindDomain (this.ResultField .Domain );
                    this.cbDomain .SelectedIndex =di;
                }
            }
        }
        #region 确认和取消
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (this.lbError.Visible == false)
            {
                String aFieldName = this.teFieldName.Text.Trim();
                if (aFieldName.Trim().Equals(""))
                {
                    MessageBox.Show("字段名称不能为空!");
                    this.teFieldName.Focus();
                    return;
                }
                #region 字段长度
                int iLen = -1;
                if (!this.teLength.Text.Trim().Equals(""))
                {
                    string strLen = this.teLength.Text.Trim();
                    double aLen = Double.NaN;
                    if (!double.TryParse(strLen, System.Globalization.NumberStyles.Any
                        , new System.Globalization.NumberFormatInfo(), out aLen))
                    {
                        MessageBox.Show("字段长度格式错误!");
                        this.teLength.Focus();
                        return;
                    }
                    iLen = (int)aLen;
                }
                #endregion
                #region 字段精度
                int iScale = -1;
                if (!this.teScale.Text.Trim().Equals(""))
                {
                    string strScale = this.teScale.Text.Trim();
                    double aScale = double.NaN;
                    if (!double.TryParse(strScale, System.Globalization.NumberStyles.Any
                        , new System.Globalization.NumberFormatInfo(), out aScale))
                    {
                        MessageBox.Show("字段精度格式错误!");
                        this.teScale.Focus();
                        return;
                    }
                    iScale = (int)aScale;
                }
                #endregion
                IField aField = null;
                if (this.ResultField != null)
                {
                    aField = this.m_curField;
                }
                else
                {
                    aField = new FieldClass();
                    this.ResultField = aField;
                }
                #region 设置名称别名类型和默认值
                IFieldEdit aFieldEdit = aField as IFieldEdit;
                //名称
                aFieldEdit.Name_2 = aFieldName;
                //别名
                String aliasName = this.teAliasName.Text.Trim();
                if (aliasName.Equals("")) aFieldEdit.AliasName_2 = aFieldName;
                else aFieldEdit.AliasName_2 = aliasName;
                //类型
                ComboBoxItem cbi = this.cbFieldType.SelectedItem as ComboBoxItem;
                esriFieldType aFT = (esriFieldType)cbi.ItemObject;
                aFieldEdit.Type_2 = aFT;
                if (!this.teDefault.Text.Equals(""))
                {
                    try
                    {
                        aFieldEdit.DefaultValue_2 = this.teDefault.Text;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                #endregion
                #region 设置长度和精度
                if (iLen >= 0)
                {
                    if (aFT == esriFieldType.esriFieldTypeString)
                    {
                        aFieldEdit.Length_2 = iLen;
                    }
                    else
                    {
                        aFieldEdit.Precision_2 = iLen;
                    }
                }
                if (iScale >= 0)
                {
                    aFieldEdit.Scale_2 = iScale;
                }
                #endregion
                aFieldEdit.IsNullable_2 = this.ceNullable.Checked;
                #region 设置域
                ComboBoxItem aDomainItem = this.cbDomain.SelectedItem as ComboBoxItem;
                if (aDomainItem != null)
                {
                    aFieldEdit.Domain_2 = aDomainItem.ItemObject as IDomain;
                }
                #endregion
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                IClassSchemaEdit aClassEdit = m_featureclass as IClassSchemaEdit;
                if (this.teAliasName.Text.Trim() != "")
                {
                    if (this.teFieldName.Text.Trim() != this.teAliasName.Text.Trim())
                    {
                        if (aClassEdit != null)
                        {
                            try
                            {
                                aClassEdit.AlterFieldAliasName(this.teFieldName.Text.Trim(), this.teAliasName.Text.Trim());
                            }
                            catch { }
                        }

                    }
                }
                LSCommonHelper.MessageBoxHelper.ShowMessageBox("修改完毕");
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }   
        #endregion
        private void cbFieldType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.LoadDomain();
            ComboBoxItem cbi=this.cbFieldType .SelectedItem  as ComboBoxItem;
            if(cbi!=null)
            {
                esriFieldType aFt=(esriFieldType)cbi.ItemObject ;
                if(esriFieldType.esriFieldTypeBlob.Equals (aFt)
                    ||esriFieldType.esriFieldTypeDate.Equals (aFt)
                    ||esriFieldType.esriFieldTypeGeometry.Equals (aFt)
                    ||esriFieldType.esriFieldTypeGlobalID.Equals (aFt)
                    ||esriFieldType.esriFieldTypeGUID.Equals (aFt)
                    ||esriFieldType.esriFieldTypeOID.Equals (aFt)
                    ||esriFieldType.esriFieldTypeRaster.Equals (aFt))
                {
                    this.teLength .Text ="";
                    this.teScale .Text ="";
                    this.teLength .Enabled =false;
                    this.teScale .Enabled =false;
                }
                else if (esriFieldType.esriFieldTypeSmallInteger.Equals(aFt))
                {
                    this.teLength.Enabled = true;
                    this.teScale.Enabled = false;
                    this.teLength.Text = "4";
                    this.teScale.Text = "";
                }
                else if (esriFieldType.esriFieldTypeInteger.Equals(aFt))
                {
                    this.teLength.Enabled = true;
                    this.teScale.Enabled = false;
                    this.teLength.Text = "10";
                    this.teScale.Text = "";
                }
                else if(esriFieldType.esriFieldTypeDouble.Equals (aFt))
                {
                    this.teLength .Enabled =true;
                    this.teScale .Enabled =true;
                    this.teLength .Text ="38";
                    this.teScale .Text ="8";
                }
                else if(esriFieldType.esriFieldTypeSingle.Equals(aFt))
                {
                    this.teLength .Enabled =true;
                    this.teScale .Enabled =true;
                    this.teLength .Text ="15";
                    this.teScale .Text ="2";
                }
                else if(esriFieldType.esriFieldTypeString.Equals (aFt))
                {
                    this.teLength .Enabled =true;
                    this.teScale .Enabled =false;
                    this.teLength .Text ="50";
                    this.teScale .Text ="";
                }                
            }
        }
        private void ShowError(String msg)
        {
            if(msg==null||msg.Trim ().Equals (""))
            {
                this.lbError .BackColor =SystemColors.Control ;
                this.lbError .Text ="";
            }
            else
            {
                this.lbError .BackColor =Color.Red ;
                this.lbError .Text =msg;
            }
        }
        private void teFieldName_EditValueChanged(object sender, System.EventArgs e)
        {
            this.ShowError ("");
            String newFieldName=this.teFieldName .Text .Trim ().ToUpper ();
            if(newFieldName.Equals (""))
            {
                this.ShowError ("字段名称不能为空");
            }
            else
            {
                foreach(IField aField in this.FieldList )
                {
                    String aName=aField.Name .ToUpper ();
                    if(newFieldName.Equals (aName))
                    {
                        this.ShowError ("已经存在同名称的字段!");
                        break;
                    }             
                }
            }
        }
	}
}

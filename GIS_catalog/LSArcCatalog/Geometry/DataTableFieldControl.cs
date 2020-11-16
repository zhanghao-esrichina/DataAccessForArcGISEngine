using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace LSArcCatalog
{
	/// <summary>
	/// DataTableFieldControl 的摘要说明。
	/// </summary>
	public class DataTableFieldControl : System.Windows.Forms.UserControl
	{
        private DevExpress.XtraEditors.SimpleButton btnDiscardField;
        private DevExpress.XtraEditors.SimpleButton btnApplyField;
        private System.Windows.Forms.ListView lvField;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ContextMenu ctxMenuField;
        private System.Windows.Forms.MenuItem miFieldNew;
        private System.Windows.Forms.MenuItem miFieldDelete;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miFieldModify;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataTableFieldControl()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			this.m_fieldCache =new HistoryObjectCache ();
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
            this.btnDiscardField = new DevExpress.XtraEditors.SimpleButton();
            this.btnApplyField = new DevExpress.XtraEditors.SimpleButton();
            this.lvField = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.ctxMenuField = new System.Windows.Forms.ContextMenu();
            this.miFieldNew = new System.Windows.Forms.MenuItem();
            this.miFieldDelete = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miFieldModify = new System.Windows.Forms.MenuItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnDiscardField
            // 
            this.btnDiscardField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiscardField.Enabled = false;
            this.btnDiscardField.Location = new System.Drawing.Point(256, 368);
            this.btnDiscardField.Name = "btnDiscardField";
            this.btnDiscardField.Size = new System.Drawing.Size(75, 23);
            this.btnDiscardField.TabIndex = 8;
            this.btnDiscardField.Text = "取消修改";
            this.btnDiscardField.Click += new System.EventHandler(this.btnDiscardField_Click);
            // 
            // btnApplyField
            // 
            this.btnApplyField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyField.Enabled = false;
            this.btnApplyField.Location = new System.Drawing.Point(352, 368);
            this.btnApplyField.Name = "btnApplyField";
            this.btnApplyField.Size = new System.Drawing.Size(75, 23);
            this.btnApplyField.TabIndex = 7;
            this.btnApplyField.Text = "应用修改";
            this.btnApplyField.Click += new System.EventHandler(this.btnApplyField_Click);
            // 
            // lvField
            // 
            this.lvField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvField.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvField.ContextMenu = this.ctxMenuField;
            this.lvField.FullRowSelect = true;
            this.lvField.GridLines = true;
            this.lvField.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvField.HideSelection = false;
            this.lvField.Location = new System.Drawing.Point(0, 0);
            this.lvField.Name = "lvField";
            this.lvField.Size = new System.Drawing.Size(432, 360);
            this.lvField.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvField.TabIndex = 6;
            this.lvField.UseCompatibleStateImageBehavior = false;
            this.lvField.View = System.Windows.Forms.View.Details;
            this.lvField.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvField_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "字段代码";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "字段名称";
            this.columnHeader2.Width = 89;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "字段类型";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 95;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "字段长度";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "字段精度";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "允许为空";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 68;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "默认值";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 128;
            // 
            // ctxMenuField
            // 
            this.ctxMenuField.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFieldNew,
            this.miFieldDelete,
            this.menuItem4,
            this.miFieldModify});
            this.ctxMenuField.Popup += new System.EventHandler(this.ctxMenuField_Popup);
            // 
            // miFieldNew
            // 
            this.miFieldNew.Index = 0;
            this.miFieldNew.Text = "新建字段";
            this.miFieldNew.Click += new System.EventHandler(this.miFieldNew_Click);
            // 
            // miFieldDelete
            // 
            this.miFieldDelete.Index = 1;
            this.miFieldDelete.Text = "删除字段";
            this.miFieldDelete.Click += new System.EventHandler(this.miFieldDelete_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // miFieldModify
            // 
            this.miFieldModify.Index = 3;
            this.miFieldModify.Text = "修改字段";
            this.miFieldModify.Click += new System.EventHandler(this.miFieldModify_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(8, 368);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "导入";
            // 
            // DataTableFieldControl
            // 
            this.Controls.Add(this.btnDiscardField);
            this.Controls.Add(this.btnApplyField);
            this.Controls.Add(this.lvField);
            this.Controls.Add(this.simpleButton1);
            this.Name = "DataTableFieldControl";
            this.Size = new System.Drawing.Size(432, 392);
            this.ResumeLayout(false);

        }
		#endregion
        public event DataTableFieldChangedEventHandler OnDataTableFieldChanged ;
        public bool ApplyVisible
        {
            get
            {
                return this.btnApplyField .Visible ;
            }
            set
            {
                this.btnApplyField .Visible =value;
            }
        }
        public bool CancelVisible
        {
            get
            {
                return this.btnDiscardField .Visible ;
            }
            set
            {
                this.btnDiscardField .Visible =value;
            }
        }
        public bool FieldChanged
        {
            get
            {
                return this.m_fieldCache .Changed ;
            }
        }
        private IWorkspace m_workspace;
        public IWorkspace Workspace
        {
            get
            {
                if(this.DispTable !=null)
                {
                    this.m_workspace =(this.DispTable as IDataset).Workspace ;
                }
                return this.m_workspace ;
            }
            set
            {
                this.m_workspace =value;
            }
        }
        private ITable m_dispTable;
        public ITable DispTable
        {
            get
            {
                return this.m_dispTable;
            }
            set
            {
                this.m_dispTable=value; 
                this.Fields  =this.m_dispTable .Fields ;
            }
        }        
        public IFeatureClass FeatureClass
        {
            get
            {
                return this.DispTable as IFeatureClass ;
            }
        }
        private IFields m_fields;
        public IFields Fields
        {
            get
            {
                if(this.m_dispTable !=null)
                    return this.m_dispTable .Fields ;
                else if(this.m_fields==null)
                {
                    this.m_fields =new FieldsClass ();
                }
                return this.m_fields ;
            }
            set
            {
                this.m_fields =value;
                if(this.m_fields !=null)
                {
                    this.ReadField ();
                }
            }
        }
        private HistoryObjectCache m_fieldCache;        
        public void ReadField()
        {
            this.lvField .Items.Clear ();
            this.m_fieldCache .ClearCache ();
            int fc=this .Fields .FieldCount ;
            for(int fi=0;fi<fc;fi++)
            {
                IField aField=this.Fields .get_Field (fi);
                aField=(aField as IClone).Clone() as IField;
                this.m_fieldCache .PutOrigin (aField);
                ListViewItem aItem=new ListViewItem();
                this.UpdateFieldListViewItem (aItem,aField);
                this.lvField .Items .Add (aItem);                  
            }
            this.EnableFieldApply();
            
        }        
        private void UpdateFieldListViewItem(ListViewItem aItem ,IField aField)
        {
            aItem.SubItems .Clear ();
            aItem.Text =aField.Name ;            
            aItem.SubItems .Add (aField.AliasName );
            aItem.SubItems .Add (LSGISHelper.FieldHelper.QueryFieldTypeName(aField.Type));
            aItem.SubItems.Add(LSGISHelper.FieldHelper.QueryFieldLength(aField).ToString());
            aItem.SubItems.Add(LSGISHelper.FieldHelper.QueryFieldPrecision(aField).ToString());
            aItem.SubItems .Add (aField.IsNullable.ToString ());
            aItem.SubItems .Add (aField.DefaultValue .ToString ());
            aItem.Tag =aField;
        }
        private void EnableFieldApply()
        {
            bool changed=this.m_fieldCache .Changed ;
            this.btnDiscardField .Enabled =changed;
            this.btnApplyField .Enabled =changed;
            if(this.OnDataTableFieldChanged !=null)
            {
                this.OnDataTableFieldChanged(this,new EventArgs());
            }
        }
        #region 字段管理
        private ArrayList FetchValidFieldFromHistory()
        {
            ArrayList aList=new ArrayList ();
            int fc=this.m_fieldCache .ObjectCount ;
            for(int fi=0;fi<fc;fi++)
            {
                HistoryObject hisObj=this.m_fieldCache .GetObjectAt (fi);
                if(hisObj.HistoryState !=HistoryState.Deleted)
                {
                    aList.Add (hisObj.ContentObject);
                }
            }
            return aList;
        }
        private void miFieldNew_Click(object sender, System.EventArgs e)
        {          
            DataFieldForm aForm=new DataFieldForm ();
            aForm.Workspace =this.Workspace ;
            aForm.FeatureClass = this.DispTable as IFeatureClass;
            ArrayList aList=this.FetchValidFieldFromHistory();
            aForm.FieldList .AddRange (aList);
            aForm.ShowInTaskbar =false;
            if(aForm.ShowDialog (this)==DialogResult.OK )
            {
                IField aField =aForm.ResultField ;  
                this.m_fieldCache .AddNew (aField);
                ListViewItem aItem=new ListViewItem ();
                this.UpdateFieldListViewItem (aItem,aField);
                this.lvField .Items .Add (aItem);
            }
            this.EnableFieldApply();
        }
        #endregion
        #region 右键菜单
        private void ctxMenuField_Popup(object sender, System.EventArgs e)
        {
            if(this.lvField .Items .Count <=0)
            {
                this.miFieldDelete .Enabled =false;
                this.miFieldModify .Enabled =false;
            }
            else
            {
                this.miFieldDelete .Enabled =true;
                this.miFieldModify .Enabled =true;
            }
        }
        private void miFieldDelete_Click(object sender, System.EventArgs e)
        {
            if(this.lvField .SelectedItems .Count <=0)
            {
                MessageBox.Show ("必须首先选中需要删除的字段!");
                return;
            }
            else
            {
                DialogResult dr=MessageBox.Show ("即将删除被选中的字段,是否继续?","删除字段"
                    ,MessageBoxButtons.YesNo ,MessageBoxIcon.Question  );
                if(DialogResult.Yes ==dr)
                {
                    string  aLenField="",aAreaField="";
                    if(this.FeatureClass !=null)
                    {
                        try
                        {
                            if(this.FeatureClass .LengthField!=null)
                            {
                                aLenField=this.FeatureClass .LengthField .Name ;
                            }
                            if(this.FeatureClass .AreaField !=null)
                            {
                                aAreaField=this.FeatureClass .AreaField .Name ;
                            }
                        }
                        catch(Exception ex)
                        {
                        }
                    }
                    int iCount=this.lvField .SelectedItems .Count ;
                    ArrayList aList=new ArrayList ();
                    for(int ii=0;ii<iCount;ii++)
                    {
                        ListViewItem aItem=this.lvField .SelectedItems [ii];
                        IField delField=aItem.Tag as IField;
                        if(delField.Type ==esriFieldType.esriFieldTypeOID
                            ||delField.Type ==esriFieldType.esriFieldTypeGeometry
                            ||delField.Name .Equals (aLenField)
                            ||delField.Name .Equals (aAreaField))
                        {
                            MessageBox.Show ("ArcGIS默认的表内编号字段和图形字段是必须的，不能删除!"
                                +"\nArcGIS默认的图形长度和图形面积字段是必须的，不能删除!","删除字段"
                                ,MessageBoxButtons.OK ,MessageBoxIcon.Error );
                        }
                        else
                        {
                            this.m_fieldCache .Delete (delField );
                            aList.Add (aItem);
                        }
                    }
                    foreach(ListViewItem aItem in aList)
                    {
                        this.lvField .Items.Remove (aItem);
                    }
                    this.EnableFieldApply();
                }
            }
        }
        private void miFieldModify_Click(object sender, System.EventArgs e)
        {
            if(this.lvField .SelectedItems .Count <=0)
            {
                MessageBox.Show ("必须选中需要修改的字段!");
                return;
            }
            if(this.lvField .SelectedItems .Count >1)
            {
                MessageBox.Show ("选中字段数目过多!");
                return;
            }
            ListViewItem aItem=this.lvField .SelectedItems [0];
            IField aField=aItem.Tag as IField;
            DataFieldForm aForm=new DataFieldForm ();
            aForm.Workspace =this.Workspace ;
            ArrayList aList=this.FetchValidFieldFromHistory();
            aForm.ResultField =aField;
            aForm.FieldList .AddRange (aList);
            aForm.ShowInTaskbar =false;
            if(aForm.ShowDialog (this)==DialogResult.OK )
            {
                IField newField =aForm.ResultField ;  
                this.m_fieldCache .Replace(aField,newField);
                this.UpdateFieldListViewItem (aItem,newField);
            }
            this.EnableFieldApply();
        }
        #endregion
        #region 应用字段修改和取消
        public void OnApply()
        {
            try
            {
                int fcCount = this.Fields.FieldCount;
                int delCount = 0;
                for (int fi = 0; fi < this.m_fieldCache.ObjectCount; fi++)
                {
                    HistoryObject hisObj = this.m_fieldCache.GetObjectAt(fi);
                    IField newField = hisObj.ContentObject as IField;

                    if (fi < fcCount)
                    {
                        IField aField = this.Fields.get_Field(fi - delCount);
                        if (hisObj.HistoryState == HistoryState.Updated)
                        {
                            string fldName = aField.Name;
                            try
                            {
                                IClassSchemaEdit aSchemaEdit = this.DispTable as IClassSchemaEdit;
                                if (aSchemaEdit != null)
                                {
                                    aSchemaEdit.AlterFieldAliasName(fldName, newField.AliasName);
                                    aSchemaEdit.AlterDomain(fldName, newField.Domain);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                            (aField as IFieldEdit).AliasName_2 = newField.AliasName;
                            (aField as IFieldEdit).Domain_2 = newField.Domain;
                        }
                        else if (hisObj.HistoryState == HistoryState.Deleted)
                        {
                            if (this.DispTable != null)
                            {
                                this.DispTable.DeleteField(aField);
                            }
                            else
                                (this.m_fields as IFieldsEdit).DeleteField(aField);
                            delCount++;//因为减少了一个字段了。
                        }
                    }
                    else
                    {
                        if (this.DispTable != null)
                        {
                            (this.DispTable).AddField(newField);
                        }
                        else
                        {
                            (this.m_fields as IFieldsEdit).AddField(newField);
                        }
                    }
                }

                this.ReadField();
            }
            catch { LSCommonHelper.MessageBoxHelper.ShowMessageBox("请确认没有其他工具连接该数据库"); }
        }
        
        public void OnCancel()
        {
            this.ReadField();
        }
        private void btnApplyField_Click(object sender, System.EventArgs e)
        {
            this.OnApply();
        }
        private void btnDiscardField_Click(object sender, System.EventArgs e)
        {
            this.OnCancel ();
        }
        #endregion        

        private void lvField_ColumnClick(object sender, ColumnClickEventArgs e)
        {
           
        }
	}
    public delegate void DataTableFieldChangedEventHandler(object sender,EventArgs args);
}

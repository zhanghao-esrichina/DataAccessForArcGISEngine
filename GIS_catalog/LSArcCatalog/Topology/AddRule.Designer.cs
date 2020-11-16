namespace LSArcCatalog.Topology
{
    partial class AddRule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbofc1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cborule = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbofc2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbofc1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cborule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbofc2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "参与要素类";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 93);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "拓扑规则";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(26, 169);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "参与要素类";
            // 
            // cbofc1
            // 
            this.cbofc1.Location = new System.Drawing.Point(103, 25);
            this.cbofc1.Name = "cbofc1";
            this.cbofc1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbofc1.Size = new System.Drawing.Size(204, 21);
            this.cbofc1.TabIndex = 3;
            // 
            // cborule
            // 
            this.cborule.Location = new System.Drawing.Point(103, 90);
            this.cborule.Name = "cborule";
            this.cborule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cborule.Properties.Items.AddRange(new object[] {
            "esriTRTAreaNoOverlap",
            "esriTRTLineCoveredByAreaBoundary",
            "esriTRTAreaBoundaryCoveredByLine",
            "esriTRTLineInsideArea",
            "esriTRTLineNoIntersection",
            "esriTRTLineNoDangles"});
            this.cborule.Size = new System.Drawing.Size(204, 21);
            this.cborule.TabIndex = 4;
            // 
            // cbofc2
            // 
            this.cbofc2.Location = new System.Drawing.Point(103, 166);
            this.cbofc2.Name = "cbofc2";
            this.cbofc2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbofc2.Size = new System.Drawing.Size(204, 21);
            this.cbofc2.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 8);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(149, 236);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(244, 236);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // AddRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 271);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbofc2);
            this.Controls.Add(this.cborule);
            this.Controls.Add(this.cbofc1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRule";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加规则";
            this.Load += new System.EventHandler(this.AddRule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbofc1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cborule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbofc2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cbofc1;
        private DevExpress.XtraEditors.ComboBoxEdit cborule;
        private DevExpress.XtraEditors.ComboBoxEdit cbofc2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}
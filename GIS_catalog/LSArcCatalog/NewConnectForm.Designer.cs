namespace LSArcCatalog
{
    partial class NewConnectForm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.changeVersion = new System.Windows.Forms.Button();
            this.saveVersion = new System.Windows.Forms.CheckBox();
            this.currentVersion = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.database = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.serviceLabel = new System.Windows.Forms.Label();
            this.service = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.server = new System.Windows.Forms.TextBox();
            this.enter = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.testConnection = new System.Windows.Forms.Button();
            this.saveUserNameAndPassword = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.quit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupBox3);
            this.panelControl1.Controls.Add(this.groupBox1);
            this.panelControl1.Controls.Add(this.enter);
            this.panelControl1.Controls.Add(this.groupBox2);
            this.panelControl1.Controls.Add(this.quit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(332, 356);
            this.panelControl1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.changeVersion);
            this.groupBox3.Controls.Add(this.saveVersion);
            this.groupBox3.Controls.Add(this.currentVersion);
            this.groupBox3.Location = new System.Drawing.Point(5, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 64);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "版本";
            // 
            // changeVersion
            // 
            this.changeVersion.Location = new System.Drawing.Point(232, 24);
            this.changeVersion.Name = "changeVersion";
            this.changeVersion.Size = new System.Drawing.Size(75, 23);
            this.changeVersion.TabIndex = 4;
            this.changeVersion.Text = "切换";
            this.changeVersion.Visible = false;
            this.changeVersion.Click += new System.EventHandler(this.changeVersion_Click);
            // 
            // saveVersion
            // 
            this.saveVersion.Location = new System.Drawing.Point(16, 23);
            this.saveVersion.Name = "saveVersion";
            this.saveVersion.Size = new System.Drawing.Size(104, 24);
            this.saveVersion.TabIndex = 0;
            this.saveVersion.Text = "保存版本";
            // 
            // currentVersion
            // 
            this.currentVersion.Location = new System.Drawing.Point(119, 24);
            this.currentVersion.Name = "currentVersion";
            this.currentVersion.Size = new System.Drawing.Size(100, 23);
            this.currentVersion.TabIndex = 3;
            this.currentVersion.Text = "sde.Default";
            this.currentVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.database);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.serviceLabel);
            this.groupBox1.Controls.Add(this.service);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.server);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 136);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(88, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "如果数据库服务器支持数据库的概念";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // database
            // 
            this.database.Location = new System.Drawing.Point(88, 72);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(216, 21);
            this.database.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据库";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // serviceLabel
            // 
            this.serviceLabel.Location = new System.Drawing.Point(16, 48);
            this.serviceLabel.Name = "serviceLabel";
            this.serviceLabel.Size = new System.Drawing.Size(56, 23);
            this.serviceLabel.TabIndex = 3;
            this.serviceLabel.Text = "服务";
            this.serviceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // service
            // 
            this.service.Location = new System.Drawing.Point(88, 48);
            this.service.Name = "service";
            this.service.Size = new System.Drawing.Size(216, 21);
            this.service.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(88, 24);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(216, 21);
            this.server.TabIndex = 0;
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(150, 323);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(75, 23);
            this.enter.TabIndex = 8;
            this.enter.Text = "确定";
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.testConnection);
            this.groupBox2.Controls.Add(this.saveUserNameAndPassword);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.password);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.userName);
            this.groupBox2.Location = new System.Drawing.Point(5, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 112);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "用户";
            // 
            // testConnection
            // 
            this.testConnection.Location = new System.Drawing.Point(232, 72);
            this.testConnection.Name = "testConnection";
            this.testConnection.Size = new System.Drawing.Size(75, 23);
            this.testConnection.TabIndex = 9;
            this.testConnection.Text = "测试连接";
            this.testConnection.Click += new System.EventHandler(this.testConnection_Click);
            // 
            // saveUserNameAndPassword
            // 
            this.saveUserNameAndPassword.Location = new System.Drawing.Point(32, 72);
            this.saveUserNameAndPassword.Name = "saveUserNameAndPassword";
            this.saveUserNameAndPassword.Size = new System.Drawing.Size(120, 24);
            this.saveUserNameAndPassword.TabIndex = 8;
            this.saveUserNameAndPassword.Text = "保存用户名/口令";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "口令";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(104, 40);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(200, 21);
            this.password.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "用户名";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(104, 16);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(200, 21);
            this.userName.TabIndex = 4;
            // 
            // quit
            // 
            this.quit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.quit.Location = new System.Drawing.Point(237, 323);
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(75, 23);
            this.quit.TabIndex = 9;
            this.quit.Text = "取消";
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // NewConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 356);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewConnectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "建立新连接";
            this.Load += new System.EventHandler(this.NewConnectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button changeVersion;
        private System.Windows.Forms.CheckBox saveVersion;
        private System.Windows.Forms.Label currentVersion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox database;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label serviceLabel;
        private System.Windows.Forms.TextBox service;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button testConnection;
        private System.Windows.Forms.CheckBox saveUserNameAndPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Button quit;
    }
}
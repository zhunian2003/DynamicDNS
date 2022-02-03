namespace DynamicDNS.Settings {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnBoot = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbeRecord = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.tbeTokenID = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.tbeToken = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.tbeInterval = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.tbeSubDomain = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.tbeDomain = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.tbePwd = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.tbeAccount = new DynamicDNS.Settings.Controls.TextBoxEdit();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstall.Location = new System.Drawing.Point(33, 25);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(106, 42);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.TabStop = false;
            this.btnInstall.Text = "安装服务";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUninstall.Location = new System.Drawing.Point(136, 25);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(106, 42);
            this.btnUninstall.TabIndex = 1;
            this.btnUninstall.TabStop = false;
            this.btnUninstall.Text = "卸载服务";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnBoot
            // 
            this.btnBoot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBoot.Location = new System.Drawing.Point(239, 25);
            this.btnBoot.Name = "btnBoot";
            this.btnBoot.Size = new System.Drawing.Size(106, 42);
            this.btnBoot.TabIndex = 2;
            this.btnBoot.TabStop = false;
            this.btnBoot.Text = "启动服务";
            this.btnBoot.UseVisualStyleBackColor = true;
            this.btnBoot.Click += new System.EventHandler(this.btnBoot_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(340, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 42);
            this.btnSave.TabIndex = 3;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbeRecord
            // 
            this.tbeRecord.BackColor = System.Drawing.Color.White;
            this.tbeRecord.Location = new System.Drawing.Point(33, 290);
            this.tbeRecord.Margin = new System.Windows.Forms.Padding(2);
            this.tbeRecord.Name = "tbeRecord";
            this.tbeRecord.PasswordMode = false;
            this.tbeRecord.Placeholder = "记录类型(暂只支持A记录和AAAA记录)";
            this.tbeRecord.Size = new System.Drawing.Size(413, 34);
            this.tbeRecord.TabIndex = 13;
            // 
            // tbeTokenID
            // 
            this.tbeTokenID.BackColor = System.Drawing.Color.White;
            this.tbeTokenID.Location = new System.Drawing.Point(33, 136);
            this.tbeTokenID.Margin = new System.Windows.Forms.Padding(2);
            this.tbeTokenID.Name = "tbeTokenID";
            this.tbeTokenID.PasswordMode = false;
            this.tbeTokenID.Placeholder = "您的DNSPod TokenID";
            this.tbeTokenID.Size = new System.Drawing.Size(413, 34);
            this.tbeTokenID.TabIndex = 12;
            this.tbeTokenID.Visible = false;
            // 
            // tbeToken
            // 
            this.tbeToken.BackColor = System.Drawing.Color.White;
            this.tbeToken.Location = new System.Drawing.Point(33, 86);
            this.tbeToken.Margin = new System.Windows.Forms.Padding(2);
            this.tbeToken.Name = "tbeToken";
            this.tbeToken.PasswordMode = false;
            this.tbeToken.Placeholder = "您的DNSPod Token";
            this.tbeToken.Size = new System.Drawing.Size(413, 34);
            this.tbeToken.TabIndex = 11;
            this.tbeToken.Visible = false;
            // 
            // tbeInterval
            // 
            this.tbeInterval.BackColor = System.Drawing.Color.White;
            this.tbeInterval.Location = new System.Drawing.Point(33, 343);
            this.tbeInterval.Margin = new System.Windows.Forms.Padding(2);
            this.tbeInterval.Name = "tbeInterval";
            this.tbeInterval.PasswordMode = false;
            this.tbeInterval.Placeholder = "刷新间隔(分钟，默认2分钟)";
            this.tbeInterval.Size = new System.Drawing.Size(413, 34);
            this.tbeInterval.TabIndex = 10;
            // 
            // tbeSubDomain
            // 
            this.tbeSubDomain.BackColor = System.Drawing.Color.White;
            this.tbeSubDomain.Location = new System.Drawing.Point(33, 237);
            this.tbeSubDomain.Margin = new System.Windows.Forms.Padding(2);
            this.tbeSubDomain.Name = "tbeSubDomain";
            this.tbeSubDomain.PasswordMode = false;
            this.tbeSubDomain.Placeholder = "主机头(默认*)";
            this.tbeSubDomain.Size = new System.Drawing.Size(413, 34);
            this.tbeSubDomain.TabIndex = 9;
            // 
            // tbeDomain
            // 
            this.tbeDomain.BackColor = System.Drawing.Color.White;
            this.tbeDomain.Location = new System.Drawing.Point(33, 186);
            this.tbeDomain.Margin = new System.Windows.Forms.Padding(2);
            this.tbeDomain.Name = "tbeDomain";
            this.tbeDomain.PasswordMode = false;
            this.tbeDomain.Placeholder = "域名";
            this.tbeDomain.Size = new System.Drawing.Size(413, 34);
            this.tbeDomain.TabIndex = 8;
            // 
            // tbePwd
            // 
            this.tbePwd.BackColor = System.Drawing.Color.White;
            this.tbePwd.Location = new System.Drawing.Point(33, 136);
            this.tbePwd.Margin = new System.Windows.Forms.Padding(2);
            this.tbePwd.Name = "tbePwd";
            this.tbePwd.PasswordMode = true;
            this.tbePwd.Placeholder = "您的DNSPod密码";
            this.tbePwd.Size = new System.Drawing.Size(413, 34);
            this.tbePwd.TabIndex = 7;
            // 
            // tbeAccount
            // 
            this.tbeAccount.BackColor = System.Drawing.Color.White;
            this.tbeAccount.Location = new System.Drawing.Point(33, 86);
            this.tbeAccount.Margin = new System.Windows.Forms.Padding(2);
            this.tbeAccount.Name = "tbeAccount";
            this.tbeAccount.PasswordMode = false;
            this.tbeAccount.Placeholder = "您的DNSPod帐号";
            this.tbeAccount.Size = new System.Drawing.Size(413, 34);
            this.tbeAccount.TabIndex = 6;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 397);
            this.Controls.Add(this.tbeRecord);
            this.Controls.Add(this.tbeTokenID);
            this.Controls.Add(this.tbeToken);
            this.Controls.Add(this.tbeInterval);
            this.Controls.Add(this.tbeSubDomain);
            this.Controls.Add(this.tbeDomain);
            this.Controls.Add(this.tbePwd);
            this.Controls.Add(this.tbeAccount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBoot);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.btnInstall);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "动态DNS配置(V2022.02.03)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnBoot;
        private System.Windows.Forms.Button btnSave;
        private Controls.TextBoxEdit tbeAccount;
        private Controls.TextBoxEdit tbePwd;
        private Controls.TextBoxEdit tbeDomain;
        private Controls.TextBoxEdit tbeSubDomain;
        private Controls.TextBoxEdit tbeInterval;
        private Controls.TextBoxEdit tbeToken;
        private Controls.TextBoxEdit tbeTokenID;
        private Controls.TextBoxEdit tbeRecord;
    }
}
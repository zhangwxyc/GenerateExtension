namespace EntityGenerator
{
	public partial class Login : global::System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::EntityGenerator.Login));
			this.label6 = new global::System.Windows.Forms.Label();
			this.ckbSystemUser = new global::System.Windows.Forms.CheckBox();
			this.txtServer = new global::System.Windows.Forms.TextBox();
			this.btnClear = new global::System.Windows.Forms.Button();
			this.btnLogin = new global::System.Windows.Forms.Button();
			this.txtPassword = new global::System.Windows.Forms.TextBox();
			this.txtUser = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtDatabase = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(36, 64);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(53, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "数据库：";
			this.ckbSystemUser.AutoSize = true;
			this.ckbSystemUser.Location = new global::System.Drawing.Point(38, 146);
			this.ckbSystemUser.Name = "ckbSystemUser";
			this.ckbSystemUser.Size = new global::System.Drawing.Size(114, 16);
			this.ckbSystemUser.TabIndex = 4;
			this.ckbSystemUser.Text = "Windows身份验证";
			this.ckbSystemUser.UseVisualStyleBackColor = true;
			this.ckbSystemUser.CheckedChanged += new global::System.EventHandler(this.ckbSystemUser_CheckedChanged);
			this.txtServer.Location = new global::System.Drawing.Point(95, 28);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new global::System.Drawing.Size(157, 21);
			this.txtServer.TabIndex = 0;
			this.btnClear.Location = new global::System.Drawing.Point(75, 175);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new global::System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "清空";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new global::System.EventHandler(this.btnClear_Click);
			this.btnLogin.Location = new global::System.Drawing.Point(156, 175);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new global::System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 5;
			this.btnLogin.Text = "登录";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new global::System.EventHandler(this.btnLogin_Click);
			this.txtPassword.Location = new global::System.Drawing.Point(95, 119);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new global::System.Drawing.Size(157, 21);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtUser.Location = new global::System.Drawing.Point(95, 86);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new global::System.Drawing.Size(157, 21);
			this.txtUser.TabIndex = 2;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(38, 122);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(53, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "密  码：";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(36, 90);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(53, 12);
			this.label2.TabIndex = 9;
			this.label2.Text = "用户名：";
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(36, 31);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(53, 12);
			this.label1.TabIndex = 7;
			this.label1.Text = "服务器：";
			this.txtDatabase.Location = new global::System.Drawing.Point(95, 59);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.Size = new global::System.Drawing.Size(157, 21);
			this.txtDatabase.TabIndex = 1;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(297, 222);
			base.Controls.Add(this.txtDatabase);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.ckbSystemUser);
			base.Controls.Add(this.txtServer);
			base.Controls.Add(this.btnClear);
			base.Controls.Add(this.btnLogin);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.txtUser);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "Login";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "登录";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.Windows.Forms.Button btnClear;

		private global::System.Windows.Forms.Button btnLogin;

		private global::System.Windows.Forms.CheckBox ckbSystemUser;

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Label label2;

		private global::System.Windows.Forms.Label label3;

		private global::System.Windows.Forms.Label label6;

		private global::System.Windows.Forms.TextBox txtDatabase;

		private global::System.Windows.Forms.TextBox txtPassword;

		private global::System.Windows.Forms.TextBox txtServer;

		private global::System.Windows.Forms.TextBox txtUser;
	}
}

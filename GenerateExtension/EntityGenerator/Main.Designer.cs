namespace EntityGenerator
{
	public partial class Main : global::System.Windows.Forms.Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::EntityGenerator.Main));
			this.label4 = new global::System.Windows.Forms.Label();
			this.cmbDatabase = new global::System.Windows.Forms.ComboBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.lbTables = new global::System.Windows.Forms.ListBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtNamespace = new global::System.Windows.Forms.TextBox();
			this.txtContent = new global::System.Windows.Forms.RichTextBox();
			this.btnGenerateFile = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txtGeneratePath = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(12, 16);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(89, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "请选择数据库：";
			this.cmbDatabase.FormattingEnabled = true;
			this.cmbDatabase.Location = new global::System.Drawing.Point(13, 31);
			this.cmbDatabase.Name = "cmbDatabase";
			this.cmbDatabase.Size = new global::System.Drawing.Size(180, 20);
			this.cmbDatabase.TabIndex = 2;
			this.cmbDatabase.TextChanged += new global::System.EventHandler(this.cmbDatabase_TextChanged);
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(14, 60);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(89, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "请选择数据表：";
			this.lbTables.FormattingEnabled = true;
			this.lbTables.ItemHeight = 12;
			this.lbTables.Location = new global::System.Drawing.Point(14, 75);
			this.lbTables.Name = "lbTables";
			this.lbTables.Size = new global::System.Drawing.Size(181, 160);
			this.lbTables.TabIndex = 4;
			this.lbTables.Click += new global::System.EventHandler(this.lbTables_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(14, 245);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(65, 12);
			this.label1.TabIndex = 8;
			this.label1.Text = "命名空间：";
			this.txtNamespace.Location = new global::System.Drawing.Point(14, 260);
			this.txtNamespace.Name = "txtNamespace";
			this.txtNamespace.Size = new global::System.Drawing.Size(178, 21);
			this.txtNamespace.TabIndex = 9;
			this.txtNamespace.Text = "DefaultNamespace";
			this.txtContent.Location = new global::System.Drawing.Point(211, 12);
			this.txtContent.Name = "txtContent";
			this.txtContent.Size = new global::System.Drawing.Size(488, 421);
			this.txtContent.TabIndex = 10;
			this.txtContent.Text = "";
			this.btnGenerateFile.Location = new global::System.Drawing.Point(109, 343);
			this.btnGenerateFile.Name = "btnGenerateFile";
			this.btnGenerateFile.Size = new global::System.Drawing.Size(75, 23);
			this.btnGenerateFile.TabIndex = 11;
			this.btnGenerateFile.Text = "生成类文件";
			this.btnGenerateFile.UseVisualStyleBackColor = true;
			this.btnGenerateFile.Click += new global::System.EventHandler(this.btnGenerateFile_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(13, 291);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(65, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "生成路径：";
			this.txtGeneratePath.Location = new global::System.Drawing.Point(14, 306);
			this.txtGeneratePath.Name = "txtGeneratePath";
			this.txtGeneratePath.Size = new global::System.Drawing.Size(180, 21);
			this.txtGeneratePath.TabIndex = 13;
			this.txtGeneratePath.Text = "D:\\";
            this.txtGeneratePath.Visible = false;
            this.label2.Visible = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(711, 445);
			base.Controls.Add(this.txtGeneratePath);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btnGenerateFile);
			base.Controls.Add(this.txtContent);
			base.Controls.Add(this.txtNamespace);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lbTables);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.cmbDatabase);
			base.Controls.Add(this.label4);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Main";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "实体类生成工具";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.Windows.Forms.Button btnGenerateFile;

		private global::System.Windows.Forms.ComboBox cmbDatabase;

		private global::System.ComponentModel.IContainer components = null;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Label label2;

		private global::System.Windows.Forms.Label label4;

		private global::System.Windows.Forms.Label label5;

		private global::System.Windows.Forms.ListBox lbTables;

		private global::System.Windows.Forms.RichTextBox txtContent;

		private global::System.Windows.Forms.TextBox txtGeneratePath;

		private global::System.Windows.Forms.TextBox txtNamespace;
	}
}

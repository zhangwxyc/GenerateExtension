using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using GenerateExtension;
using GenerateExtension.EntityGenerator;

namespace EntityGenerator
{
    public partial class Login : Form
    {
        public SlnInfo @SlnInfo { get; set; }

        //public Login()
        //{
        //    this.connString = string.Empty;
        //    this.components = null;
        //    this.InitializeComponent();
        //}
        public Login(Main main, SlnInfo slnInfo)
        {
            this.FormClosing += Login_FormClosing;
            this.connString = string.Empty;
            this.components = null;
            this.main = main;
            this.InitializeComponent();
            @SlnInfo = slnInfo;
            //ConfigHelper ch = ConfigHelper.Load(slnInfo.SlnPath);
            //if (ch != null)
            //{
            //    this.connString = ch.ConnectionString;
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //    return;
            //    //this.txtServer.Text = ch.DateBaseService;
            //    //this.txtDatabase.Text = ch.DateBase;
            //    //this.txtUser.Text = ch.UserName;
            //    //this.txtPassword.Text = ch.PassWord;
            //}
            //else
            //{
                this.txtServer.Text = "192.168.10.158";
                this.txtDatabase.Text = "HMCCRM";
                this.txtUser.Text = "sa";
                this.txtPassword.Text = "1qaz@WSX";
            //}
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtUser.Clear();
            this.txtPassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool flag = string.IsNullOrEmpty(this.txtDatabase.Text);
            if (flag)
            {
                this.txtDatabase.Text = "master";
            }
            bool @checked = this.ckbSystemUser.Checked;
            if (@checked)
            {
                this.connString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=true;", this.txtServer.Text, this.txtDatabase.Text);
            }
            else
            {
                this.connString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=false;User={2};Password={3};", new object[]
                {
                    this.txtServer.Text,
                    this.txtDatabase.Text,
                    this.txtUser.Text,
                    this.txtPassword.Text
                });
            }
            try
            {
                SqlConnection sqlConnection = new SqlConnection(this.connString);
                sqlConnection.Open();
                sqlConnection.Close();
                this.main.connString = this.connString;
                this.main.dateBase = this.txtDatabase.Text.Trim();
                ConfigHelper ch = new ConfigHelper()
                {
                    DateBaseService = this.txtServer.Text.Trim(),
                    DateBase = this.txtDatabase.Text.Trim(),
                    UserName = this.txtUser.Text.Trim(),
                    PassWord = this.txtPassword.Text.Trim(),
                    ConnectionString = connString
                };
                ch.Init(@SlnInfo.SlnPath);

                this.DialogResult = DialogResult.OK;
                base.Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show("连接失败！错误信息：" + ex.Message);
            }
        }

        private void ckbSystemUser_CheckedChanged(object sender, EventArgs e)
        {
            bool @checked = this.ckbSystemUser.Checked;
            if (@checked)
            {
                this.txtUser.Enabled = false;
                this.txtPassword.Enabled = false;
            }
            else
            {
                this.txtUser.Enabled = true;
                this.txtPassword.Enabled = true;
            }
        }

        protected override void WndProc(ref Message msg)
        {
            bool flag = msg.Msg == 274 && (int)msg.WParam == 61536;
            if (flag)
            {
                Application.Exit();
            }
            base.WndProc(ref msg);
        }

        private string connString;

        private Main main;
    }
}

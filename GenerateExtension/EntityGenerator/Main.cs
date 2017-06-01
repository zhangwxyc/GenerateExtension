using GenerateExtension;
using GenerateExtension.EntityGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EntityGenerator
{
    public partial class Main : Form
    {
        public Main(SlnInfo slnInfo)
        {
            this.InitializeComponent();
            
            ConfigHelper ch = ConfigHelper.Load(slnInfo.SlnPath);
            if (ch != null && !string.IsNullOrWhiteSpace(ch.ConnectionString))
            {
                connString = ch.ConnectionString;
                dateBase = ch.DateBase;
            }
            else
            {
                var digRet = new Login(this, slnInfo).ShowDialog();
                if (digRet != DialogResult.OK)
                {
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                    return;
                }
            }
            this.LoadDatabase();
            this.txtNamespace.Text = slnInfo.NameSpace;
        }
        public string Content { get; set; }
        public string FileName { get; set; }
        private void btnGenerateFile_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = string.IsNullOrEmpty(this.txtContent.Text.Trim());
                if (flag)
                {
                    MessageBox.Show("生成内容不能为空！");
                }
                else
                {
                    Content = this.txtContent.Text.Trim();
                    FileName = this.lbTables.Text.Trim() + "Entity.cs";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件生成失败！错误信息：" + ex.Message);
            }
        }

        private void ChangeColor()
        {
            this.txtContent.SelectionStart = 0;
            this.txtContent.SelectionLength = this.txtContent.Text.Length;
            this.txtContent.SelectionColor = Color.Black;
            bool flag = this.listDescription.Count > 0;
            if (flag)
            {
                this.ChangeKeyColor(this.listDescription, Color.Green);
            }
            this.ChangeKeyColor("namespace", Color.Blue);
            this.ChangeKeyColor("public", Color.Blue);
            this.ChangeKeyColor("class", Color.Blue);
            this.ChangeKeyColor("/// <summary>", Color.Gray);
            this.ChangeKeyColor("///", Color.Gray);
            this.ChangeKeyColor("/// </summary>", Color.Gray);
            this.ChangeKeyColor("int", Color.Blue);
            this.ChangeKeyColor("double", Color.Blue);
            this.ChangeKeyColor("float", Color.Blue);
            this.ChangeKeyColor("char", Color.Blue);
            this.ChangeKeyColor("string", Color.Blue);
            this.ChangeKeyColor("bool", Color.Blue);
            this.ChangeKeyColor("decimal", Color.Blue);
            this.ChangeKeyColor("enum", Color.Blue);
            this.ChangeKeyColor("const", Color.Blue);
            this.ChangeKeyColor("struct", Color.Blue);
            this.ChangeKeyColor("DateTime", Color.CadetBlue);
            this.ChangeKeyColor("get", Color.Blue);
            this.ChangeKeyColor("set", Color.Blue);
        }

        public void ChangeKeyColor(List<string> list, Color color)
        {
            foreach (string current in list)
            {
                this.ChangeKeyColor(current, color);
            }
        }

        public void ChangeKeyColor(string key, Color color)
        {
            MatchCollection matchCollection = new Regex(key).Matches(this.txtContent.Text);
            foreach (Match match in matchCollection)
            {
                this.txtContent.SelectionStart = match.Index;
                this.txtContent.SelectionLength = key.Length;
                this.txtContent.SelectionColor = color;
            }
        }

        private Dictionary<string, string> _sqlTypeMap = new Dictionary<string, string>()
        {
            {"nvarchar", "string" },
            {"varchar", "string" },
            {"nchar", "string" },
            {"ntext", "string" },
            {"text", "string" },
            {"char", "string" },
            {"xml", "string" },
            {"tinyint", "int" },
            {"smallint", "int" },
            {"bigint", "int" },
            {"bit", "bool" },
            {"real", "float" },
            {"float", "double" },
            {"smallmoney", "decimal" },
            {"money", "decimal" },
            {"decimal", "decimal" },
            {"numeric", "decimal" },
            {"smalldatetime", "DateTime" },
            {"datetime", "DateTime" },
            {"datetime2", "DateTime" },
            {"date", "DateTime" },
            {"time", "TimeSpan" },
            {"timestamp", "byte[]" },
            {"uniqueidentifier", "Guid" },
            {"binary", "byte[]" },
            {"varbinary", "byte[]" },
            {"image", "byte[]" },
        };

        private string ChangeWords(string content)
        {
            foreach (var sqlType in _sqlTypeMap)
            {
                content = Regex.Replace(content, sqlType.Key, sqlType.Value);
            }

            return content;

        }

        private void cmbDatabase_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbDatabase.Text;
            this.connString = Regex.Replace(this.connString, "Catalog=[0-9a-zA-Z_]+;", "Catalog=" + text + ";");
            this.LoadTables(text);
        }

        private void GenerateEntity()
        {
            try
            {
                int num = 0;
                StringBuilder stringBuilder = new StringBuilder();
                string databaseName = this.cmbDatabase.Text;
                var tableName = this.lbTables.Text;
                string cmdText = String.Format(
                    @"SELECT  syscolumns.name AS ColName
       ,systypes.name AS TypeName
       ,sys.extended_properties.value AS Description
       ,sysobjects.name AS TableName
       ,p2.value AS TableDescription
       ,p2.*
FROM    syscolumns
        INNER JOIN sysobjects ON syscolumns.id = sysobjects.id
        INNER JOIN systypes ON syscolumns.xtype = systypes.xtype
        LEFT JOIN sys.extended_properties ON sys.extended_properties.major_id = syscolumns.id
                                             AND sys.extended_properties.minor_id = syscolumns.colorder
        LEFT JOIN sys.extended_properties p2 ON p2.major_id = syscolumns.id
                                                AND p2.minor_id = 0 AND p2.name = 'MS_Description'
WHERE   sysobjects.name = '{0}'
        AND systypes.name <> 'sysname'
ORDER BY sys.extended_properties.minor_id ASC;",
                    tableName);
                this.con = new SqlConnection(this.connString);
                this.con.Open();
                this.cmd = new SqlCommand(cmdText, this.con);
                this.adapter = new SqlDataAdapter(this.cmd);
                this.ds = new DataSet();
                this.adapter.Fill(this.ds, "Entity");
                if (this.ds.Tables[0].Rows.Count == 0)
                {
                    this.txtContent.Text = "没有查询结果......";
                }
                else
                {
                    stringBuilder.AppendLine("using System;\r\n");
                    if (!string.IsNullOrEmpty(this.txtNamespace.Text.Trim()))
                    {
                        stringBuilder.AppendLine("namespace " + this.txtNamespace.Text + "\r\n{");
                        num += 4;
                    }
                    stringBuilder.Append(new string(' ', num));
                    stringBuilder.AppendLine("/// <summary>");
                    var tableDescription = this.ds.Tables[0].Rows[0][4].ToString().Trim().Replace(Environment.NewLine, "");
                    if (!string.IsNullOrEmpty(tableDescription))
                    {
                        stringBuilder.Append(new string(' ', num));
                        stringBuilder.AppendLine("/// " + tableDescription + "实体类");
                        this.listDescription.Add(tableDescription + "实体类");
                    }
                    stringBuilder.Append(new string(' ', num));
                    stringBuilder.AppendLine("/// " + string.Format("{0}.dbo.{1}", databaseName, tableName));
                    this.listDescription.Add(string.Format("{0}.dbo.{1}", databaseName, tableName));
                    stringBuilder.Append(new string(' ', num));
                    stringBuilder.AppendLine("/// </summary>");
                    stringBuilder.Append(new string(' ', num));
                    stringBuilder.AppendLine("public class " + this.ds.Tables[0].Rows[0][3].ToString() + "Entity");
                    stringBuilder.Append(new string(' ', num));
                    stringBuilder.AppendLine("{");
                    for (int i = 0; i < this.ds.Tables[0].Rows.Count; i++)
                    {
                        var description = this.ds.Tables[0].Rows[i][2].ToString().Trim().Replace(Environment.NewLine, "");
                        if (!string.IsNullOrEmpty(description))
                        {
                            stringBuilder.Append(new string(' ', num + 4));
                            stringBuilder.AppendLine("/// <summary>");
                            stringBuilder.Append(new string(' ', num + 4));
                            stringBuilder.AppendLine("/// " + description);
                            this.listDescription.Add(description);
                            stringBuilder.Append(new string(' ', num + 4));
                            stringBuilder.AppendLine("/// </summary>");
                        }
                        stringBuilder.Append(new string(' ', num + 4));
                        stringBuilder.AppendLine(string.Concat(new string[]
                        {
                            "public ",
                            this.ChangeWords(this.ds.Tables[0].Rows[i][1].ToString()),
                            " ",
                            this.ds.Tables[0].Rows[i][0].ToString(),
                            " { get; set; }"
                        }));
                    }
                    stringBuilder.Append(new string(' ', num));
                    stringBuilder.AppendLine("}");
                    if (!string.IsNullOrEmpty(this.txtNamespace.Text.Trim()))
                    {
                        stringBuilder.AppendLine("}");
                    }
                    string text = stringBuilder.ToString();
                    this.txtContent.Text = text;
                    this.ChangeColor();
                    this.adapter.Dispose();
                    this.cmd.Dispose();
                    this.con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("类生成失败！错误信息：" + ex.Message);
            }
        }

        private void lbTables_Click(object sender, EventArgs e)
        {
            this.GenerateEntity();
        }

        private void LoadDatabase()
        {
            try
            {
                this.con = new SqlConnection(this.connString);
                this.con.Open();
                string cmdText = string.Format("use master;select * from sysdatabases  ORDER BY name ASC ", new object[0]);
                this.cmd = new SqlCommand(cmdText, this.con);
                this.adapter = new SqlDataAdapter(this.cmd);
                this.ds = new DataSet();
                this.adapter.Fill(this.ds);
                this.cmbDatabase.DisplayMember = "name";
                this.cmbDatabase.ValueMember = "name";
                this.cmbDatabase.DataSource = this.ds.Tables[0];
                this.adapter.Dispose();
                this.cmd.Dispose();
                this.con.Close();
                bool flag = !string.IsNullOrEmpty(this.dateBase);
                if (flag)
                {
                    this.cmbDatabase.Text = this.dateBase;
                    this.LoadTables(this.dateBase);
                    this.connString = Regex.Replace(this.connString, "Catalog=[0-9a-zA-Z_]+;", "Catalog=" + this.dateBase + ";");
                }
                else
                {
                    this.LoadTables(this.cmbDatabase.Text);
                    this.connString = Regex.Replace(this.connString, "Catalog=[0-9a-zA-Z_]+;", "Catalog=" + this.cmbDatabase.Text + ";");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库加载错误！错误信息：" + ex.Message);
            }
        }

        private void LoadTables(string database)
        {
            try
            {
                if (this.con.State != ConnectionState.Open)
                {
                    this.con.Open();
                }
                StringBuilder stringBuilder = new StringBuilder().AppendFormat("use {0};select * from sysobjects where xtype in ('U','V') AND category <> 2 ORDER BY name ASC", database);
                this.cmd = new SqlCommand(stringBuilder.ToString(), this.con);
                this.adapter = new SqlDataAdapter(this.cmd);
                this.ds = new DataSet();
                this.adapter.Fill(this.ds);
                this.lbTables.DataSource = this.ds.Tables[0];
                this.lbTables.DisplayMember = "name";
                this.lbTables.ValueMember = "name";
                this.lbTables.SelectedItems.Clear();
                this.adapter.Dispose();
                this.cmd.Dispose();
                this.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据表加载错误！错误信息：" + ex.Message);
            }
        }

        private SqlDataAdapter adapter;

        private SqlCommand cmd;

        private SqlConnection con;

        public string connString = string.Empty;

        public string dateBase = "";

        private DataSet ds;

        private List<string> listDescription = new List<string>();
    }
}

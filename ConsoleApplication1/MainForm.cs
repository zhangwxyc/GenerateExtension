using GenerateExtension.AgentClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    public partial class MainForm : Form
    {
        private static string _mainPage = "http://test2.op.maiche.biz/tmcop/ApiClient/Index";

        public string ClassPath { get; set; }
        public string CfgPath { get; set; }

        public MainForm(string cfgPath)
        {
            CfgPath = cfgPath;
            if (string.IsNullOrWhiteSpace(CfgPath))
            {
                CfgPath = Environment.CurrentDirectory;
            }
        }
        public MainForm()
        {
            InitForm();
            CfgPath = Environment.CurrentDirectory;
        }

        private void InitForm()
        {
            InitializeComponent();
            _cfg = ConfigHelper.Load(CfgPath);
            if (_cfg == null && !string.IsNullOrWhiteSpace(_cfg.MainPage))
            {
                _mainPage = _cfg.MainPage;
            }

            BrowserForm.Url = new Uri(_mainPage);
            BrowserForm.AllowWebBrowserDrop = false;
            BrowserForm.WebBrowserShortcutsEnabled = false;
            BrowserForm.IsWebBrowserContextMenuEnabled = false;
            BrowserForm.DocumentCompleted += BrowserForm_DocumentCompleted;
            BrowserForm.NewWindow += BrowserForm_NewWindow;
            BrowserForm.Navigated += BrowserForm_Navigated;
            BrowserForm.Navigating += BrowserForm_Navigating;
            BrowserForm.FileDownload += BrowserForm_FileDownload;
        }

        private void BrowserForm_FileDownload(object sender, EventArgs e)
        {

        }

        private void BrowserForm_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

            if (BrowserForm.Url.AbsoluteUri.ToLower().Contains("op/login.aspx"))
            {
                HtmlDocument htmlDoc = BrowserForm.Document;
                //设置帐号
                //HtmlElement id = htmlDoc.GetElementById("username");
                string id = htmlDoc.GetElementById("txtUserName").GetAttribute("value");
                string pwd = htmlDoc.GetElementById("txtUserPwd").GetAttribute("value");

                new ConfigHelper()
                {
                    UserName = id,
                    PassWord = pwd
                }.Init(CfgPath);
                //btnLogin
                IsLogining = true;
            }
            else if (e.Url.AbsoluteUri.ToLower().Contains("/apiclient/generate"))
            {
                e.Cancel = true;
                string appId = GetAppId(e.Url.AbsoluteUri);
                if (!string.IsNullOrWhiteSpace(appId))
                {
                    string savePath = GetSavePath(appId);
                    FileDownload(e.Url.AbsoluteUri, savePath);
                    //this.DialogResult = DialogResult.OK;
                    //this.Close();
                    MessageBox.Show(string.Format("已创建：{0}", appId));
                }
            }
        }

        private void FileDownload(string absoluteUri, string savePath)
        {
            //WebClient wb = new WebClient();
            //wb.Encoding = Encoding.UTF8;
            //wb.DownloadFile(absoluteUri, savePath);
            string domain = new Uri(absoluteUri).Host;

            CookieContainer myCookieContainer = new CookieContainer();
            if (BrowserForm.Document.Cookie != null)
            {
                string cookieStr = BrowserForm.Document.Cookie;
                string[] cookstr = cookieStr.Split(';');
                foreach (string str in cookstr)
                {
                    string[] cookieNameValue = str.Split('=');
                    Cookie ck = new Cookie(cookieNameValue[0].Trim().ToString(), cookieNameValue[1].Trim().ToString());
                    ck.Domain = domain;
                    myCookieContainer.Add(ck);
                }
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(absoluteUri);
            request.CookieContainer = myCookieContainer;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();
            File.WriteAllText(savePath, html);
        }

        private string GetSavePath(string appId)
        {
            return Path.Combine(Environment.CurrentDirectory, appId + ".cs");
        }

        private string GetAppId(string url)
        {
            var match = Regex.Match(url, "(\\?|^)appid=(?<appId>.*)($|&)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups["appId"].Value;
            }
            return "";
        }

        private void BrowserForm_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.AbsoluteUri.ToLower().Contains("op/login.aspx"))
            {

            }
            else if (IsLogining)
            {
                //IsLogining = false;
                //BrowserForm.Navigate(_mainPage);
            }
        }

        private void BrowserForm_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        public bool IsLogining { get; set; }
        private ConfigHelper _cfg { get; set; }
        private void BrowserForm_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //将所有的链接的目标，指向本窗体
            foreach (HtmlElement archor in this.BrowserForm.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }
            //将所有的FORM的提交目标，指向本窗体
            foreach (HtmlElement form in this.BrowserForm.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }


            if (e.Url.AbsoluteUri.ToLower().Contains("op/login.aspx"))
            {
                var cfg = _cfg;
                if (cfg != null)
                {
                    HtmlDocument htmlDoc = BrowserForm.Document;
                    //设置帐号
                    //HtmlElement id = htmlDoc.GetElementById("username");
                    htmlDoc.GetElementById("txtUserName").SetAttribute("value", cfg.UserName);
                    htmlDoc.GetElementById("txtUserPwd").SetAttribute("value", cfg.PassWord);

                    //btnLogin
                    HtmlElement btn = htmlDoc.GetElementById("btnLogin");
                    if (btn != null)
                    {
                        btn.InvokeMember("click");
                    }
                }
            }
            else if (e.Url.AbsoluteUri.ToLower().Contains("op/main.aspx"))
            {
                BrowserForm.Navigate("http://op.maiche.biz/op/loginhelper.aspx?moduleid=4659d194-e764-4934-a943-e2caec0c6113", false);
                IsLogining = true;
            }
            else if (IsLogining)
            {
                IsLogining = false;
                BrowserForm.Navigate(_mainPage);
            }
        }
    }
}

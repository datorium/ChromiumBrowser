using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace ChromiumBrowser
{
    public partial class Browser : Form
    {

        public ChromiumWebBrowser browser;

        public Browser()
        {
            InitializeComponent();
            InitializeBrowser();
            InitializeTabs();
        }
        
        private void InitializeTabs()
        {
            Tabs.Dock = DockStyle.Bottom;
            Tabs.Height = ClientRectangle.Height - 50;
            Tabs.TabPages.Clear();
            AddBrowserTab();
        }

        public void InitializeBrowser()
        {
            Cef.Initialize(new CefSettings());
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            browser.Load(AddressBar.Text);
        }

        private void Browser_Resize(object sender, EventArgs e)
        {
            Tabs.Height = ClientRectangle.Height - 50;
        }

        private void buttonAddTab_Click(object sender, EventArgs e)
        {
            AddBrowserTab();
        }

        private void AddBrowserTab()
        {
            TabPage newTab = new TabPage();
            newTab.Text = "tab";
            Tabs.Controls.Add(newTab);
            browser = new ChromiumWebBrowser("www.google.com");
            newTab.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser br;
            br = (ChromiumWebBrowser)Tabs.SelectedTab.Controls[0];
            br.Reload();
        }
    }
}

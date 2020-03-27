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
        private ChromiumWebBrowser browser;

        public Browser()
        {
            InitializeComponent();
            InitializeBrowser();
            InitializeTabs();
        }
        private void InitializeBrowser()
        {
            Cef.Initialize(new CefSettings());
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            browser.Load(AddressBar.Text);
        }

        private void InitializeTabs()
        {
            Tabs.Dock = DockStyle.Bottom;
            Tabs.Height = ClientRectangle.Height - 50;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TabPage newTab = new TabPage();
            browser = new ChromiumWebBrowser("www.google.com");
            newTab.Text = "tab" + (Tabs.TabCount + 1).ToString();
            newTab.Controls.Add(browser);
            Tabs.Controls.Add(newTab);
            browser.Dock = DockStyle.Fill;
        }

        private void Browser_Resize(object sender, EventArgs e)
        {
            Tabs.Height = ClientRectangle.Height - 50;
        }
    }
}

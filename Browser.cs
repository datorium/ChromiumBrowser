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

        private void InitializeTabs()
        {
            Tabs.Dock = DockStyle.Bottom;
            Tabs.Height = ClientRectangle.Height - 50;
            Tabs.TabPages.Clear();
            AddBrowserTab();
        }

        
        
        private void AddBrowserTab()
        {
            TabPage newTab = new TabPage();
            browser = new ChromiumWebBrowser("www.google.com");
            newTab.Text = "tab" + (Tabs.TabCount + 1).ToString();
            newTab.Controls.Add(browser);
            Tabs.Controls.Add(newTab);
            browser.Dock = DockStyle.Fill;
            Tabs.SelectedTab = newTab;
        }


        private void Browser_Resize(object sender, EventArgs e)
        {
            Tabs.Height = ClientRectangle.Height - 50;
        }

        private void Tabs_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                MessageBox.Show("Right click!");
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            LoadIntoActiveBrowser();
        }

        private void LoadIntoActiveBrowser()
        {
            ChromiumWebBrowser activeBrowser;
            activeBrowser = (ChromiumWebBrowser)Tabs.SelectedTab.Controls[0];
            activeBrowser.Load(addressBar.Text);
        }

        private void addTab_Click(object sender, EventArgs e)
        {
            AddBrowserTab();
        }

        private void addressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                LoadIntoActiveBrowser();
            }
        }

        private void addressBar_MouseDown(object sender, MouseEventArgs e)
        {
            addressBar.SelectAll();
        }
    }
}

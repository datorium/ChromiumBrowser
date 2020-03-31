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
            AddBrowserTab("www.google.com");
        }

        public void InitializeBrowser()
        {
            Cef.Initialize(new CefSettings());
        }

        private void Browser_Resize(object sender, EventArgs e)
        {
            Tabs.Height = ClientRectangle.Height - 50;
        }

        private void buttonAddTab_Click(object sender, EventArgs e)
        {
            AddBrowserTab("www.google.com");
        }

        private void AddBrowserTab(string defaultURL)
        {
            TabPage newTab = new TabPage();
            newTab.Text = "tab";
            Tabs.Controls.Add(newTab);
            browser = new ChromiumWebBrowser(defaultURL);
            newTab.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            Tabs.SelectedTab = newTab;
            browser.AddressChanged += Browser_AddressChanged;
            browser.TitleChanged += Browser_TitleChanged;
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Tabs.SelectedTab.Text = e.Title;
            }));
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                AddressBar.Text = e.Address;
            }));
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser br;
            br = (ChromiumWebBrowser)Tabs.SelectedTab.Controls[0];
            br.Reload();
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            LoadToSelectedBrowser();
        }

        private void LoadToSelectedBrowser()
        {
            try
            {
                var selectedBrowser = (ChromiumWebBrowser)Tabs.SelectedTab.Controls[0];
                selectedBrowser.Load(AddressBar.Text);
            }
            catch
            {
                AddBrowserTab(AddressBar.Text);               
            }
        }

        private void AddTabButton_Click(object sender, EventArgs e)
        {
            AddBrowserTab("www.google.com");
        }

        private void RemoveTabButton_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedTabIndex = Tabs.SelectedIndex;
                Tabs.SelectedTab.Dispose();
                Tabs.SelectedIndex = selectedTabIndex - 1;
            }
            catch
            {
                
            }
        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                LoadToSelectedBrowser();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedBrowser = (ChromiumWebBrowser)Tabs.SelectedTab.Controls[0];
                selectedBrowser.Back();
            }
            catch
            {
                
            }
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedBrowser = (ChromiumWebBrowser)Tabs.SelectedTab.Controls[0];
                selectedBrowser.Forward();
            }
            catch
            {

            }
        }
    }
}

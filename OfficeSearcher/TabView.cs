using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfficeSearcher
{
    public partial class TabView : TabPage
    {
        public TabView()
        {
            InitializeComponent();
        }
        public TabView(OfficeData officeData)
        {
            InitializeComponent();
            webBrowser1.DocumentText = MyExt.ToHtml(officeData);
        }
        private bool bCancel = false;
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            int i;
            for (i = 0; i < webBrowser1.Document.Links.Count; i++)
            {
                webBrowser1.Document.Links[i].Click += new HtmlElementEventHandler(LinkClick);
            }
        }
        private void LinkClick(object sender, System.EventArgs e)
        {
            bCancel = true;
            HtmlElement element = (HtmlElement)sender;

            string id = element.GetAttribute("id");
            string href = element.GetAttribute("href");
            if (id == "0")
            {
                try
                {
                    System.Diagnostics.Process.Start(href);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + href, "Thong bao loi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", href));
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + href, "Thong bao loi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (bCancel)
            {
                e.Cancel = true;
                bCancel = false;
            }
        }
    }
}

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
    public partial class TabFind : TabPage
    {
        private List<OfficeData> lst = new List<OfficeData>();
        public TabFind()
        {
            InitializeComponent();
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtWhere.Text)) return;
            if (string.IsNullOrEmpty(TxtWhere.Text.Trim())) { TxtWhere.Clear(); return; }
            LuceneSearch search = new LuceneSearch();
            if ("cmd:thongke" == TxtWhere.Text)
            {
                webBrowser1.DocumentText = search.GetAllDoc();
                toolStripStatusLabel1.Text = "   All file names";
            }
            else
            {
                Lucene.Net.Search.Query query = search.GetTextQuery(TxtWhere.Text.Trim());
                if (query != null)
                    { lst = search.Search(query, MyConfig.GetMaxFindResult()); }
                toolStripStatusLabel1.Text = string.Format("   Found {0} files", lst.Count);
                webBrowser1.DocumentText = MyExt.ToShortHtml(lst);
            }
            search.LuceneDispose();
            
        }

        private void TxtWhere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnFind.PerformClick();
            }
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
            if (id == "-1")
            {
                string href = element.GetAttribute("href");
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
                //FrmView frmView = new FrmView(lst[Convert.ToInt32(id)]);
                //frmView.Show();
                TabView tabView = new TabView(lst[Convert.ToInt32(id)]);
                tabView.Text = "View " + id;
                var tabCtrl = (TabControl)this.Parent;
                tabCtrl.TabPages.Add(tabView);
                tabCtrl.SelectedIndex = tabCtrl.TabCount - 1;
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

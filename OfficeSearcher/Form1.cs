using Microsoft.Office.Interop.Excel;
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
    public partial class Form1 : Form
    {
        public bool IsIndexing;
        public Form1()
        {
            InitializeComponent();
            this.Text = MyConfig.GetAppName();
        }

        private void TsBtnCloseAll_Click(object sender, EventArgs e)
        {
            for (int i = tabControl1.TabPages.Count - 1; i >= 0; i--)
            {
                //Khi dang Indexing thi khong dong tab Index
                Type tabType = tabControl1.TabPages[i].GetType();
                if (IsIndexing)
                {
                   if (tabType != typeof(TabIndex) && tabType != typeof(TabFind))
                    {
                        tabControl1.TabPages.RemoveAt(i);
                    }
                }
                else
                {
                    if (tabType != typeof(TabFind))
                    {
                        tabControl1.TabPages.RemoveAt(i);
                    }
                }
            }
        }

        private void TsClose_Click(object sender, EventArgs e)
        {
            int i = tabControl1.SelectedIndex;
            //Khi dang Indexing thi khong dong tab Index
            Type tabType = tabControl1.TabPages[i].GetType();
            if (IsIndexing)
            {
                if (tabType != typeof(TabIndex) && tabType != typeof(TabFind))
                {
                    tabControl1.TabPages.RemoveAt(i);
                }
            }
            else
            {
                if (tabType != typeof(TabFind))
                {
                    tabControl1.TabPages.RemoveAt(i);
                }
            }
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }

        private void TsBtnIndex_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].GetType() == typeof(TabIndex))
                {
                    tabControl1.SelectedIndex = i;
                    return;
                }
            }
            TabIndex indexTab = new TabIndex();
            indexTab.Text = "Index";
            tabControl1.TabPages.Add(indexTab);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }

        private void TsBtnAbout_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].GetType() == typeof(TabAbout))
                {
                    tabControl1.SelectedIndex = i;
                    return;
                }
            }
            TabAbout tab = new TabAbout();
            tab.Text = "About";
            tabControl1.TabPages.Add(tab);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }
    }
}

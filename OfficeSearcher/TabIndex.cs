using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OfficeSearcher
{
    public partial class TabIndex : TabPage
    {
        public TabIndex()
        {
            InitializeComponent();
            TxtIndexFolder.Text = MyConfig.GetIndexFolder();
        }
        private StringBuilder sbErr = new StringBuilder();
        private readonly object _sbLock = new object();
        private void BgWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string sPath = MyConfig.GetIndexFolder();
            string[] sFiles = System.IO.Directory.GetFiles(sPath, "*.*", SearchOption.AllDirectories);
            if (sFiles.Length == 0)
            {
                BgWorker1.ReportProgress(0, "No file found");
            }
            else
            {
                DateTime DFrom = (DateTime)e.Argument;

                List<FileMeta> docFiles = new List<FileMeta>();
                List<FileMeta> xlsFiles = new List<FileMeta>();
                List<FileMeta> pptFiles = new List<FileMeta>();
                List<FileMeta> otherFiles = new List<FileMeta>();

                int totalFile = 0;
                foreach (string sFile in sFiles)
                {
                    FileMeta fileMeta = new FileMeta();
                    fileMeta.FileName = sFile;
                    fileMeta.DLastWriteTime = File.GetLastWriteTime(sFile);

                    if (fileMeta.DLastWriteTime > DFrom)
                    {
                        totalFile++;
                        string fileExt = Path.GetExtension(sFile).ToUpper();
                        if (fileExt == ".DOC" || fileExt == ".DOCX")
                        {
                            docFiles.Add(fileMeta);
                        }
                        else if (fileExt == ".XLS" || fileExt == ".XLSX")
                        {
                            xlsFiles.Add(fileMeta);
                        }
                        else if (fileExt == ".PPT" || fileExt == ".PPTX")
                        {
                            pptFiles.Add(fileMeta);
                        }
                        else
                        {
                            otherFiles.Add(fileMeta);
                        }
                    }
                }
                BgWorker1.ReportProgress(1, "Total files: " + totalFile.ToString());
                BgWorker1.ReportProgress(1, "It is time-consuming task, please wait ....");
                LuceneIndex luceneIndex = new LuceneIndex();

                //Xoa du lieu cu
                if (DFrom == DateTime.MinValue) //Re-Index All
                {
                    luceneIndex.DeleteAllDocuments();
                }
                else //Index From
                {
                    luceneIndex.DeleteDocumentsFrom(DFrom);
                }

                if (otherFiles.Count != 0)
                {
                    int currFile = 0;
                    foreach (var fileMeta in otherFiles)
                    {
                        currFile++;
                        BgWorker1.ReportProgress(1, string.Format("Indexing FILE_NAME ({0}/{1}): {2}", currFile, otherFiles.Count, fileMeta.FileName));
                        luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                    }

                    //ParallelLoopResult otherResult = Parallel.ForEach(otherFiles, (fileMeta) =>
                    //{
                    //    luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                    //    BgWorker1.ReportProgress(1, string.Format("Indexing file name: {0}", fileMeta.FileName));
                    //});
                    //if (otherResult.IsCompleted)
                    //{
                    //    luceneIndex.Save();
                    //}
                }
                luceneIndex.Save();

                if (docFiles.Count != 0)
                {
                    Word2Txt word2Txt = new Word2Txt();
                    int currDoc = 0;
                    foreach (FileMeta fileMeta in docFiles)
                    {
                        currDoc++;
                        BgWorker1.ReportProgress(1, string.Format("Indexing DOC file ({0}/{1}): {2}", currDoc, docFiles.Count, fileMeta.FileName));
                        string[] sBody = word2Txt.GetText(fileMeta.FileName);
                        if (string.IsNullOrEmpty(sBody[1]))
                        {
                            luceneIndex.BuildIndex(sBody[0], fileMeta.FileName, fileMeta.LastWriteTime);
                            
                        }
                        else
                        {
                            BgWorker1.ReportProgress(1, string.Format("Error: {0}", sBody[1]));
                            luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                        }
                    }
                    word2Txt.QuitApp();
                    luceneIndex.Save();
                    //ParallelLoopResult docResult = Parallel.ForEach(docFiles, (fileMeta) =>
                    //{
                    //    string[] sBody = word2Txt.GetText(fileMeta.FileName);
                    //    if (string.IsNullOrEmpty(sBody[1]))
                    //    {
                    //        luceneIndex.BuildIndex(sBody[0], fileMeta.FileName, fileMeta.LastWriteTime);
                    //        BgWorker1.ReportProgress(1, string.Format("Indexing file: {0}", fileMeta.FileName));
                    //    }
                    //    else
                    //    {
                    //        luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                    //        BgWorker1.ReportProgress(1, string.Format("Error: {0}", sBody[1]));
                    //    }
                    //});
                    //if (docResult.IsCompleted)
                    //{
                    //    luceneIndex.Save();
                    //    word2Txt.QuitApp();
                    //}
                }

                if (pptFiles.Count != 0)
                {
                    int currPpt = 0;
                    Ppt2Txt ppt2Txt = new Ppt2Txt();
                    foreach (FileMeta fileMeta in pptFiles)
                    {
                        currPpt++;
                        BgWorker1.ReportProgress(1, string.Format("Indexing PPT file ({0}/{1}): {2}", currPpt, pptFiles.Count, fileMeta.FileName));
                        string[] sBody = ppt2Txt.GetText(fileMeta.FileName);
                        if (string.IsNullOrEmpty(sBody[1]))
                        {
                            luceneIndex.BuildIndex(sBody[0], fileMeta.FileName, fileMeta.LastWriteTime);
                        }
                        else
                        {
                            BgWorker1.ReportProgress(1, string.Format("Error: {0}", sBody[1]));
                            luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                        }
                    }
                    ppt2Txt.QuitApp();
                    luceneIndex.Save();
                    //ParallelLoopResult pptResult = Parallel.ForEach(xlsFiles, (fileMeta) =>
                    //{
                    //    string[] sBody = ppt2Txt.GetText(fileMeta.FileName);
                    //    if (string.IsNullOrEmpty(sBody[1]))
                    //    {
                    //        luceneIndex.BuildIndex(sBody[0], fileMeta.FileName, fileMeta.LastWriteTime);
                    //        BgWorker1.ReportProgress(1, string.Format("Indexing file: {0}", fileMeta.FileName));
                    //    }
                    //    else
                    //    {
                    //        luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                    //        BgWorker1.ReportProgress(1, string.Format("Error: {0}", sBody[1]));
                    //    }
                    //});
                    //if (pptResult.IsCompleted)
                    //{
                    //    luceneIndex.Save();
                    //    ppt2Txt.QuitApp();
                    //}
                }

                if (xlsFiles.Count != 0)
                {
                    int currXls = 0;
                    Excel2Txt excel2Txt = new Excel2Txt();
                    foreach (FileMeta fileMeta in xlsFiles)
                    {
                        currXls++;
                        BgWorker1.ReportProgress(1, string.Format("Indexing XLS file ({0}/{1}): {2}", currXls, xlsFiles.Count, fileMeta.FileName)); ;
                        string[] sBody = excel2Txt.GetText(fileMeta.FileName);
                        if (string.IsNullOrEmpty(sBody[1]))
                        {
                            luceneIndex.BuildIndex(sBody[0], fileMeta.FileName, fileMeta.LastWriteTime);
                        }
                        else
                        {
                            BgWorker1.ReportProgress(1, string.Format("Error: {0}", sBody[1]));
                            luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                        }
                    }
                    excel2Txt.QuitApp();
                    luceneIndex.Save();
                    //ParallelLoopResult xlsResult = Parallel.ForEach(xlsFiles, (fileMeta) =>
                    //{
                    //    string[] sBody = excel2Txt.GetText(fileMeta.FileName);
                    //    if (string.IsNullOrEmpty(sBody[1]))
                    //    {
                    //        luceneIndex.BuildIndex(sBody[0], fileMeta.FileName, fileMeta.LastWriteTime);
                    //        BgWorker1.ReportProgress(1, string.Format("Indexing file: {0}", fileMeta.FileName));
                    //    }
                    //    else
                    //    {
                    //        luceneIndex.BuildIndex(string.Empty, fileMeta.FileName, fileMeta.LastWriteTime);
                    //        BgWorker1.ReportProgress(1, string.Format("Error: {0}", sBody[1]));
                    //    }
                    //});
                    //if (xlsResult.IsCompleted)
                    //{
                    //    luceneIndex.Save();
                    //    excel2Txt.QuitApp();
                    //}
                }
                luceneIndex.SaveAndDispose();
            }
        }

        private void BgWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string sStatus = e.UserState as string;
            TxtStatus.AppendText(sStatus);
            TxtStatus.AppendText(Environment.NewLine);
        }

        private void BgWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TxtStatus.AppendText("Complete!");
            BtnIndexFrom.Enabled = true;
            BtnIndexAll.Enabled = true;
            (this.Parent.Parent as Form1).IsIndexing = false;
        }

        private void BtnIndexFrom_Click(object sender, EventArgs e)
        {
            TxtStatus.Clear();
            BgWorker1.RunWorkerAsync(DPickFrom.Value);
            BtnIndexFrom.Enabled = false;
            BtnIndexAll.Enabled = false;
        }

        private void BtnIndexAll_Click(object sender, EventArgs e)
        {
            (this.Parent.Parent as Form1).IsIndexing = true;
            TxtStatus.Clear();
            BgWorker1.RunWorkerAsync(DateTime.MinValue);
            BtnIndexFrom.Enabled = false;
            BtnIndexAll.Enabled = false;
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            string sPath = null;
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    sPath = dialog.SelectedPath;
            }
            if (!string.IsNullOrEmpty(sPath))
            {
                DialogResult result = MessageBox.Show("All indexed data will be deleted. Do you want to change?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    LuceneIndex luceneIndex = new LuceneIndex();
                    luceneIndex.DeleteAllDocuments();
                    luceneIndex.SaveAndDispose();
                    MyConfig.SetIndexFolder(sPath);
                    TxtIndexFolder.Text = MyConfig.GetIndexFolder();
                }
            }
        }
    }
}

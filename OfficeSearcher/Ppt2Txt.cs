using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using OfficeCore = Microsoft.Office.Core;

namespace OfficeSearcher
{
    public class Ppt2Txt
    {
        private PowerPoint.Application pptApp = null;
        Microsoft.Office.Interop.PowerPoint.Presentation pp = null;
        public Ppt2Txt()
        {
            pptApp = new PowerPoint.Application();
            //pptApp.Visible = OfficeCore.MsoTriState.msoTrue;
        }

        public void QuitApp()
        {
            if (pptApp != null)
            {
                pptApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(pptApp);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public string[] GetText(string sFileName)
        {
            StringBuilder sb = new StringBuilder();
            string err = string.Empty;
            try
            {
                pp = pptApp.Presentations.Open(sFileName, OfficeCore.MsoTriState.msoTrue, OfficeCore.MsoTriState.msoFalse, OfficeCore.MsoTriState.msoFalse);
                foreach (Microsoft.Office.Interop.PowerPoint.Slide slide in pp.Slides)
                {
                    foreach (Microsoft.Office.Interop.PowerPoint.Shape shape in slide.Shapes)
                    {
                        if (shape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                        {
                            var textFrame = shape.TextFrame;
                            if (textFrame.HasText == Microsoft.Office.Core.MsoTriState.msoTrue)
                            {
                                var textRange = textFrame.TextRange;
                                string item = Convert.ToString(textRange.Text);
                                sb.Append(item);
                                sb.Append(Environment.NewLine);
                            }
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                sb.Append(sFileName);
                err = sFileName + " - " + ex.Message;
            }
            finally
            {
                if (pp != null)
                {
                    pp.Close();
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(pp);
                    pp = null;
                }
            }
            return new string[] { sb.ToString(), err };
        }
    }

    public class PptWorker
    {
        public PowerPoint.Application PptApp { get; set; }
        public LuceneIndex luceneIndex { get; set; }
        public FileMeta fileMeta { get; set; }
        public StringBuilder sbErr { get; set; }

        private readonly object oLockSbErr = new object();

        public PptWorker() { sbErr = new StringBuilder(); }
        public void Add2Index()
        {
            Microsoft.Office.Interop.PowerPoint.Presentation pp = null;

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Empty);
            string err = string.Empty;
            try
            {
                pp = PptApp.Presentations.Open(fileMeta.FileName, OfficeCore.MsoTriState.msoTrue, OfficeCore.MsoTriState.msoFalse, OfficeCore.MsoTriState.msoFalse);
                foreach (Microsoft.Office.Interop.PowerPoint.Slide slide in pp.Slides)
                {
                    foreach (Microsoft.Office.Interop.PowerPoint.Shape shape in slide.Shapes)
                    {
                        if (shape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                        {
                            var textFrame = shape.TextFrame;
                            if (textFrame.HasText == Microsoft.Office.Core.MsoTriState.msoTrue)
                            {
                                var textRange = textFrame.TextRange;
                                string item = Convert.ToString(textRange.Text);
                                sb.Append(item);
                                sb.Append(Environment.NewLine);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lock (oLockSbErr)
                {
                    sbErr.Append(ex.Message);
                    sbErr.Append(" ");
                    sbErr.Append(fileMeta.FileName);
                    sbErr.Append(Environment.NewLine);
                }
            }
            finally
            {
                if (pp != null)
                {
                    pp.Close();
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(pp);
                    pp = null;
                }
            }
            luceneIndex.BuildIndex(sb.ToString(), fileMeta.FileName, fileMeta.LastWriteTime);
        }

        public void QuitApp()
        {
            if (PptApp != null)
            {
                PptApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(PptApp);
            }
        }
    }

    public class PptAsync
    {
        public string sError = string.Empty;
        public PptAsync() { }
        public async void IndexAsync(List<FileMeta> lst, LuceneIndex luceneIndex)
        {
            PptWorker worker = new PptWorker();
            worker.PptApp = new Microsoft.Office.Interop.PowerPoint.Application();
            //worker.PptApp.Visible = false;
            worker.luceneIndex = luceneIndex;

            System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                worker.fileMeta = lst[i];
                tasks[i] = System.Threading.Tasks.Task.Run(() => DoAsync(worker));
            }
            await System.Threading.Tasks.Task.WhenAll(tasks);

            sError = worker.sbErr.ToString();
            worker.QuitApp();
        }
        public static void DoAsync(PptWorker worker)
        {
            worker.Add2Index();
        }
    }
}
